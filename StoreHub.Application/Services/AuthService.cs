using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using StoreHub.Application.Dtos.AuthDto;
using StoreHub.Application.Services.Contracts;
using StoreHub.Core.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreHub.Application.Services
{
    public class AuthService(UserManager<AppUser> user) : IAuthService
    {
        public async Task<UserResultDto> LoginAsync(LoginDto login)
        {
            var findUser = await user.FindByEmailAsync(login.Email);
            if (findUser is null) throw new Exception("Invalid email or password");
            var findByPasseord = await user.CheckPasswordAsync(findUser, login.Password);
            if (findByPasseord == false) throw new Exception("Invalid email or password");
            var result = new UserResultDto()
            {
                DisplayName = findUser.DisplayName,
                Email = login.Email,
                Token = "Token",
            };
            return result;
        }

        public async Task<UserResultDto> RegisterAsync(RegisterDto register)
        {
            var AddUser = new AppUser()
            {
                UserName = register.UserName,
                DisplayName = register.DisplayName,
                Email = register.Email,
                PhoneNumber = register.PhoneNumber,
            };

            var result = await user.CreateAsync(AddUser, register.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"User registration failed: {errors}");
            }
            var Data = new UserResultDto()
            {
                DisplayName = AddUser.DisplayName,
                Email = register.Email,
                Token = "Token",
            };
            return Data;
        }
    }
}
