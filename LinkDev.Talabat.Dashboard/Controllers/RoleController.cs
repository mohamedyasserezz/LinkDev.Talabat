using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Dashboard.Models.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.Talabat.Dashboard.Controllers
{
    public class RoleController(RoleManager<IdentityRole> _roleManager) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleFormViewModel model)
        {
            if (!ModelState.IsValid)
                throw new BadRequestException("Invalid create");

            var IsRoleExist = await _roleManager.RoleExistsAsync(model.Name);
            if (!IsRoleExist)
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = model.Name.Trim() });
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("Name", "Role is already exist");
                return View(nameof(Index), await _roleManager.Roles.ToListAsync());
            }
        }
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role is not null)
                await _roleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var roleVM = new RoleViewModel
            {
                Name = role.Name!
            };
            return View(roleVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id, RoleViewModel model)
        {
            if (!ModelState.IsValid)
                throw new BadRequestException("Invalid create");

            var IsRoleExist = await _roleManager.RoleExistsAsync(model.Name);
            if (!IsRoleExist)
            {
                var role = await _roleManager.FindByIdAsync(model.Id);
                role.Name = model.Name;
                await _roleManager.UpdateAsync(role);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("Name", "Role is already exist");
                return View(nameof(Index), await _roleManager.Roles.ToListAsync());
            }
        }
    }
}
