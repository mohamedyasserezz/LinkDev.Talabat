using LinkDev.Talabat.APIs.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Buggy
{
    public class BuggyController : BaseApiController
    {
        [HttpGet("notfound")]
        public IActionResult GetNotFound()
        {
            return NotFound(new {StatusCode = 404, Message = "Not Found"}); // 404
        }
        [HttpGet("servererror")]
        public IActionResult GetServerError()
        {
            throw new Exception(); // 500
        }
        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new { StatusCode = 400, Message = "Bad Request" }); // 400
        }
        [HttpGet("badrequest/{id}")]
        public IActionResult GetValdiationError(int id) // => 400
        {
            return ValidationProblem();
        }


        [HttpGet("UnAuthorized")]
        public IActionResult GetUnAuthorized()
        {
            return Unauthorized(new { StatusCode = 401, Message = "UnAuthorized" }); // 401
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
