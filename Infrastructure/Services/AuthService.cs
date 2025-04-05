using Infrastructure.Data.Entities;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Services
{
   
    public interface IAuthService
    {
        Task<SignInResult> SignInAsync(SignInForm form);
        Task<IdentityResult> SignUpAsync(SignUpForm form);
    }

    public class AuthService(UserManager<UserEntity> userManager, SignInManager<UserEntity> signManager, RoleManager<IdentityRole> roleManager, IMemoryCache cache) : IAuthService
    {
        private readonly UserManager<UserEntity> _userManager = userManager;
        private readonly SignInManager<UserEntity> _signManager = signManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
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

        public async Task<SignInResult> SignInAsync(SignInForm form)
        {
            var result = await _signManager.PasswordSignInAsync(form.Email, form.Password, false, false);
            return result;
        }

    }
 }
