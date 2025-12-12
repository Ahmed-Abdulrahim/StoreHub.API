using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic.FileIO;
using StoreHub.Application.Dtos.AuthDto;
using StoreHub.Application.Services.Contracts;
using StoreHub.Application.Shared;
using StoreHub.Core.Models.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StoreHub.Application.Services
{
    public class AuthService(UserManager<AppUser> user, IOptions<JwtOptions> jwtOptions) : IAuthService
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
                Token = GenerateJwtToken(findUser),
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
                Token = GenerateJwtToken(AddUser),
            };
            return Data;
        }
        private string GenerateJwtToken(AppUser appUser)
        {
            var jwt = jwtOptions.Value;
            var autClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email , appUser.Email),
                new Claim(ClaimTypes.MobilePhone , appUser.PhoneNumber),
            };

            var role = user.GetRolesAsync(appUser);
            foreach (var item in role.Result)
            {
                autClaims.Add(new Claim(ClaimTypes.Role, item));
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.secretKey));
            var token = new JwtSecurityToken
                (
                issuer: jwt.issuer,
                audience: jwt.audience,
                claims: autClaims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: new SigningCredentials(secretKey, algorithm: SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
