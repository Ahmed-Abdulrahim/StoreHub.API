using StoreHub.Application.Dtos.AuthDto;
using StoreHub.Application.Dtos.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreHub.Application.Services.Contracts
{
    public interface IAuthService
    {
        Task<UserResultDto> LoginAsync(LoginDto login);
        Task<UserResultDto> RegisterAsync(RegisterDto register);

        Task<bool> CheckEmailExist(string email);
        Task<UserResultDto> GetCurrentUser(string email);
        Task<ShippingAddressDto> GetCurrentUserAddress(string email);
        Task<ShippingAddressDto> UpdateCurrentUserAddress(ShippingAddressDto address, string email);
    }
}
