using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Common;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Account
{
    public class AccountController(IServiceManager serviceManager) : BaseApiController
    {
        [HttpPost("login")]
        public async Task<ActionResult<LoginDto>> Login(LoginDto model)
        {
            var result = await serviceManager.AuthService.LoginAsync(model);
            return Ok(result);
        }
        [HttpPost("Register")]
        public async Task<ActionResult<RegisterDto>> Register(RegisterDto model)
        {
            var result = await serviceManager.AuthService.RegisterAsync(model);
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var result = await serviceManager.AuthService.GetCurrentUserAsync(User);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var result = await serviceManager.AuthService.GetUserAddressAsync(User);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto addressDto)
        {
            var result = await serviceManager.AuthService.UpdateUserAddressAsync(User, addressDto);

            return Ok(result);
        }
        
        [HttpDelete("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            return Ok(await serviceManager.AuthService.EmailExists(email));
        }
    }
}
