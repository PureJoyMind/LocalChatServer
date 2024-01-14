using System.ComponentModel.DataAnnotations;
using LocalChatServerWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LocalChatServerWeb.Controllers
{
    public class OperationsController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private RoleManager<ApplicationRole> roleManager;

        public OperationsController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public ViewResult Create() => View();

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = new ApplicationUser
                {
                    UserName = user.Name
                };

                IdentityResult result = await userManager.CreateAsync(appUser, user.Password);
                if (result.Succeeded)
                {
                    //Adding User to Admin Role
                    await userManager.AddToRoleAsync(appUser, "Admin");
                    ViewBag.Message = "User Created Successfully";
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View(user);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CreateRole() => View();

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRole([Required] string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await roleManager.CreateAsync(new ApplicationRole() { Name = name });
                if (result.Succeeded)
                    ViewBag.Message = "Role Created Successfully";
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }
    }
}
