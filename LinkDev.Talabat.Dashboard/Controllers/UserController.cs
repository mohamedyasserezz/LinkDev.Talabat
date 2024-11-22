using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Dashboard.Models.Roles;
using LinkDev.Talabat.Dashboard.Models.UserRoles;
using LinkDev.Talabat.Dashboard.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.Talabat.Dashboard.Controllers
{
    public class UserController(UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync(); // Fetch users first
            var usersVM = new List<UserViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user); // Await GetRolesAsync
                usersVM.Add(new UserViewModel
                {
                    Id = user.Id,
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    UserName = user.UserName,
                    Roles = roles // Roles populated asynchronously
                });
            }

            return View(usersVM);

        }
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var allRoles = await _roleManager.Roles.ToListAsync();
            var userRoleVM = new UserRolesViewModel()
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = allRoles.Select(r => new RoleViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    IsSelected = _userManager.IsInRoleAsync(user, r.Id).Result
                }).ToList()
            };
            return View(userRoleVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id, UserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(id);

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in model.Roles)
            {
                if (userRoles.Any(r => r == role.Name) && !role.IsSelected)
                {
                    await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                    await _userManager.AddToRoleAsync(user, role.Name);

            }
            return RedirectToAction("Index");
        }
    }
}
