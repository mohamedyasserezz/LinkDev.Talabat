﻿using LinkDev.Talabat.APIs.Controllers.Errors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Common
{
    [ApiController]
    [Route("Errors/{Code}")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class ErrorsController : ControllerBase
    {
        [HttpGet]
        public ActionResult Errors(int Code)
        {
            if (Code == (int)HttpStatusCode.NotFound)
            {
                var response = new ApiResponse((int)HttpStatusCode.NotFound, $"the requested endpoint is not found");
                return NotFound(response);
            }

            return StatusCode(Code, new ApiResponse(Code));
        }
    }
}
