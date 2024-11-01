﻿using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    }
}
