using Infrastructure.Data.Entities;
using Infrastructure.Manager;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Services
{
   
    public interface IAuthService
    {
        Task<object?> SignInAsync(SignInForm form);
        Task<IdentityResult> SignUpAsync(SignUpForm form);
    }

    public class AuthService(UserManager<UserEntity> userManager, SignInManager<UserEntity> signManager, RoleManager<IdentityRole> roleManager, IMemoryCache cache, ITokenManager tokenManager) : IAuthService
    {
        private readonly UserManager<UserEntity> _userManager = userManager;
        private readonly SignInManager<UserEntity> _signManager = signManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly ITokenManager _tokenManager = tokenManager;
        private readonly IMemoryCache _cache = cache;
        private const string _cacheKey_All = "User_all";

        public async Task<IdentityResult> SignUpAsync(SignUpForm model)
        {
            var user = new UserEntity
            {
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email
            };

            var identityResult = await _userManager.CreateAsync(user, model.Password);
            if (identityResult.Succeeded)
            {
                var userRole = model.Role ?? "User";

                if (!await _roleManager.RoleExistsAsync(userRole))
                {
                    await _roleManager.CreateAsync(new IdentityRole(userRole));
                }

                await _userManager.AddToRoleAsync(user, userRole);

                _cache.Remove(_cacheKey_All);

            }
            return identityResult;
        }

        public async Task<object?> SignInAsync(SignInForm form)
        {
            var user = await _userManager.FindByEmailAsync(form.Email);
            if (user == null)
                return null;

            var signInResult = await _signManager.CheckPasswordSignInAsync(user, form.Password, false);
            if (!signInResult.Succeeded)
                return null;

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "User";

            var token = _tokenManager.GenerateJwtToken(user, role);
            // skickar med en user object också då hanteringen av rolecheck sker via den i frontenden
            return new
            {
                token,
                user = new
                {
                    id = user.Id,
                    name = user.UserName,
                    email = user.Email,
                    role = roles
                }
            };
        }

    }
 }
