using LinkDev.Talabat.Core.Application.Abstraction.Models.Auth;
using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.Dashboard.Controllers
{
    public class AdminController(SignInManager<ApplicationUser> _signInManager, UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager) : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null)
                throw new UnAuthorizedException("There is no user with this email.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (result.IsNotAllowed)
                throw new NotFoundException("user", loginDto.Email); // Check if IsNotAllowed is true
            if (result.IsLockedOut)
                throw new BadRequestException("User is locked out.");
            if (!result.Succeeded || !await _userManager.IsInRoleAsync(user, "Admin"))
            {
                throw new UnAuthorizedException("User does not have the necessary permissions.");
            }

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
