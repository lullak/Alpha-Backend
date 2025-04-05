using Infrastructure.Data.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(string id);
        Task<IEnumerable<User>> GetUsersAsync();
        Task<(IdentityResult Result, bool Success)> CreateUserAsync(AddUserFormData formData, string defaultRole = "User");
        Task<bool> DeleteUserAsync(string id);
        Task<bool> UpdateUserAsync(EditUserFormData formData);
    }

    //Mycket osäker på rollhantering och usermanager blev mycket fram och tillbaka med AI här och mycket av koden är därifrån
    // speciellt för create och update hittade även andra resurser med liknande lösningar. Gäller ej cache.
    //Dock har jag börjat förstå hur man först sätter upp roller med rolemanager för att sedan använda
    //usermangers metoder för att ta bort och lägga till roller
    public class UserService(IUserRepository userRepository, UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager, IUserAddressRepository addressRepository, IMemoryCache cache) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly UserManager<UserEntity> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly IUserAddressRepository _addressRepository = addressRepository;
        private readonly IMemoryCache _cache = cache;
        private const string _cacheKey_All = "User_all";


        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            if (_cache.TryGetValue(_cacheKey_All, out IEnumerable<User>? cachedItems))
                return cachedItems;

            _cache.Remove(_cacheKey_All);
            var entities = await _userRepository.GetAllAsync(
                orderByDescending: false,
                sortBy: x => x.FirstName,
                filterBy: null,
                i => i.Address
            );

            var users = new List<User>();
            foreach (var entity in entities)
            {
                var roles = await _userManager.GetRolesAsync(entity);
                var role = roles.FirstOrDefault() ?? "User";
                var user = UserFactory.ToModel(entity, role);
                users.Add(user);
            }
            _cache.Set(_cacheKey_All, users, TimeSpan.FromMinutes(10));

            return users;

        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            if (_cache.TryGetValue(_cacheKey_All, out IEnumerable<User>? cachedItems))
            {
                var cachedProject = cachedItems?.FirstOrDefault(x => x.Id == id);
                if (cachedProject != null)
                    return cachedProject;
            }

            _cache.Remove(_cacheKey_All);
            var entity = await _userRepository.GetAsync(x => x.Id == id, i => i.Address);
            if (entity == null)
                return null!;
            var roles = await _userManager.GetRolesAsync(entity);
            var role = roles.FirstOrDefault() ?? "User";
            var users = UserFactory.ToModel(entity, role);

            _cache.Set(_cacheKey_All, users, TimeSpan.FromMinutes(10));
            return users;
        }

        public async Task<(IdentityResult Result, bool Success)> CreateUserAsync(AddUserFormData formData, string defaultRole = "User")
        {
            if (formData == null)
                return (IdentityResult.Failed(new IdentityError { Description = "Form data is null" }), false);

            var user = new UserEntity
            {
                UserName = formData.Email,
                Email = formData.Email,
                FirstName = formData.FirstName,
                LastName = formData.LastName,
                PhoneNumber = formData.PhoneNumber,
                JobTitle = formData.JobTitle,
                Image = formData.Image
            };

            string password = "BYTmig123!";
            var identityResult = await _userManager.CreateAsync(user, password);

            if (!identityResult.Succeeded)
                return (identityResult, false);

            if (!string.IsNullOrWhiteSpace(formData.StreetName) &&
                !string.IsNullOrWhiteSpace(formData.PostalCode) &&
                !string.IsNullOrWhiteSpace(formData.City))
            {
                var address = new UserAddressEntity
                {
                    StreetName = formData.StreetName,
                    PostalCode = formData.PostalCode,
                    City = formData.City,
                    UserId = user.Id
                };

                if (!await _addressRepository.AddAsync(address))
                {
                    await _userManager.DeleteAsync(user);
                    return (IdentityResult.Failed(new IdentityError { Description = "Failed to add address" }), false);
                }
            }

            var userRole = formData.Role ?? defaultRole;
            if (!await _roleManager.RoleExistsAsync(userRole))
            {
                await _roleManager.CreateAsync(new IdentityRole(userRole));
            }

            var roleResult = await _userManager.AddToRoleAsync(user, userRole);
            if (!roleResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                return (roleResult, false);
            }
            
            _cache.Remove(_cacheKey_All);


            return (IdentityResult.Success, true);
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                _cache.Remove(_cacheKey_All);
            }
            return result.Succeeded;
        }

        public async Task<bool> UpdateUserAsync(EditUserFormData formData)
        {
            if (formData == null)
                return false;

            var userEntity = await _userRepository.GetAsync(
                u => u.Id == formData.Id,
                u => u.Address);

            if (userEntity == null)
            {
                return false;
            }

            userEntity.FirstName = formData.FirstName;
            userEntity.LastName = formData.LastName;
            userEntity.JobTitle = formData.JobTitle;
            userEntity.PhoneNumber = formData.PhoneNumber;
            userEntity.Email = formData.Email;
            userEntity.Image = formData.Image;

            if (userEntity.Address == null)
            {
                userEntity.Address = new UserAddressEntity
                {
                    UserId = formData.Id,
                    City = formData.City,
                    PostalCode = formData.PostalCode,
                    StreetName = formData.StreetName
                };
            }
            else
            {
                userEntity.Address.City = formData.City;
                userEntity.Address.PostalCode = formData.PostalCode;
                userEntity.Address.StreetName = formData.StreetName;
            }
            var updateResult = await _userRepository.UpdateAsync(userEntity);
            if (!updateResult)
            {
                return false;
            }
            var currentRoles = await _userManager.GetRolesAsync(userEntity);
            if (!currentRoles.Contains(formData.Role))
            {
                var removeResult = await _userManager.RemoveFromRolesAsync(userEntity, currentRoles);
                if (!removeResult.Succeeded)
                {
                    return false;
                }

                var addResult = await _userManager.AddToRoleAsync(userEntity, formData.Role);
                if (!addResult.Succeeded)
                {
                    return false;
                }
                    
            }

            _cache.Remove(_cacheKey_All);

            return true;
        }

   
    }
}

