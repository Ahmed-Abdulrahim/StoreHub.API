using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic.FileIO;
using StoreHub.Application.Dtos.AuthDto;
using StoreHub.Application.Dtos.OrderDto;
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
    public class AuthService(IMapper mapper, UserManager<AppUser> user, IOptions<JwtOptions> jwtOptions) : IAuthService
    {
        public async Task<bool> CheckEmailExist(string email)
        {
            var isEmailExist = await user.FindByEmailAsync(email);
            return isEmailExist != null;
        }

        public async Task<UserResultDto> GetCurrentUser(string email)
        {
            var userExist = await user.FindByEmailAsync(email);
            if (userExist is null) throw new Exception("User not found");

            var result = new UserResultDto()
            {
                DisplayName = userExist.DisplayName,
                Email = userExist.Email,
                Token = GenerateJwtToken(userExist),
            };
            return result;
        }

        public async Task<ShippingAddressDto> GetCurrentUserAddress(string email)
        {
            var userAddress = await user.Users.Include(u => u.Address).FirstOrDefaultAsync(s => s.Email == email);
            if (userAddress is null) throw new Exception("User not found");
            var result = mapper.Map<ShippingAddressDto>(userAddress.Address);

            return result;
        }

        public async Task<ShippingAddressDto> UpdateCurrentUserAddress(ShippingAddressDto address, string email)
        {
            var userExist = await user.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == email);
            if (userExist is null) throw new Exception("User not found");
            if (userExist.Address is null)
            {
                userExist.Address = mapper.Map<Address>(address);
                userExist.Address.AppUserId = userExist.Id;
            }
            else
            {
                userExist.Address.FirstName = address.FirstName;
                userExist.Address.LastName = address.LastName;
                userExist.Address.Street = address.Street;
                userExist.Address.City = address.City;
                userExist.Address.Country = address.Country;
            }
            await user.UpdateAsync(userExist);
            return address;
        }

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
            if (await CheckEmailExist(register.Email))
            {
                throw new Exception("Email is already in use");
            }
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
