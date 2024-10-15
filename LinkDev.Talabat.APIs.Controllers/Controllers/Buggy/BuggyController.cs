using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.APIs.Controllers.Controllers.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Buggy
{
    public class BuggyController : BaseApiController
    {
        [HttpGet("notfound")]
        public IActionResult GetNotFound()
        {
            return NotFound(new ApiResponse(404)); // 404
        }
        [HttpGet("servererror")]
        public IActionResult GetServerError()
        {
            throw new Exception(); // 500
        }
        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400)); // 400
        }
        [HttpGet("badrequest/{id}")]
        public IActionResult GetValdiationError(int id) // => 400
        {
            return ValidationProblem();
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
