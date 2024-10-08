using LinkDev.Talabat.Core.Application.Abstraction;
using System.Security.Claims;

namespace LinkDev.Talabat.APIs.Services
{
    public class LoggedInUserService : ILoggedInUserService
    {
        private readonly IHttpContextAccessor? _contextAccessor;
        public string? UserId { get; }
        public LoggedInUserService(IHttpContextAccessor? httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
            UserId = _contextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.PrimarySid);
        }
    }
}
