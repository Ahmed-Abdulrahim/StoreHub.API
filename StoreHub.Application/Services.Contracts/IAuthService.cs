using StoreHub.Application.Dtos.AuthDto;
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
    }
}
