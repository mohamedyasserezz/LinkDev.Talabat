using LinkDev.Talabat.Core.Application.Abstraction.Models.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Common;
using System.Security.Claims;

namespace LinkDev.Talabat.Core.Application.Abstraction.Services.Auth
{
    public interface IAuthService
    {
        Task<UserDto> LoginAsync(LoginDto loginDto);
        Task<UserDto> RegisterAsync(RegisterDto registerDto);

        Task<UserDto> GetCurrentUserAsync(ClaimsPrincipal claimsPrincipal);
        Task<AddressDto?> GetUserAddressAsync(ClaimsPrincipal claimsPrincipal);

        Task<AddressDto?> UpdateUserAddressAsync(ClaimsPrincipal claim, AddressDto addressDto);
    }
}
