using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.APIs.Controllers.Controllers.Errors;
using LinkDev.Talabat.APIs.Controllers.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Buggy
{
    public class BuggyController : BaseApiController
    {
        [HttpGet("notfound")]
        public IActionResult GetNotFound()
        {
            throw new NotFoundException();
            // return NotFound(new ApiResponse(404)); // 404
        }
        [HttpGet("servererror")]
        public IActionResult GetServerError()
        {
            /// try
            /// {
            ///     throw new Exception();
            /// }
            /// catch(Exception ex)
            /// {
            ///     var response = new ApiResponse(500);
            ///     Response.WriteAsync(JsonSerializer.Serialize(response));
            ///     return StatusCode(response.StatusCode);
            /// }

            throw new Exception();
            
        }
        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400)); // 400
        }
        [HttpGet("badrequest/{id}")]
        public IActionResult GetValdiationError(int id) // => 400
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(P => P.Value.Errors.Count > 0)
                                           .SelectMany(P => P.Value.Errors)
                                           .Select(P => P.ErrorMessage);

                return BadRequest(new ApiValdiationErrorResponse() { Errors = errors }); 
            }
            return Ok();
        }


        [HttpGet("UnAuthorized")]
        public IActionResult GetUnAuthorized()
        {
            return Unauthorized(new ApiResponse(401)); // 401
        }
        [HttpGet("Forbidden")]
        public IActionResult GetForbidden()
        {
            return Forbid(); // 403
        }
        [Authorize]
        [HttpGet("authorized")]
        public IActionResult GetNotAuthorized()
        {
            return Ok();
        }
    }
}
