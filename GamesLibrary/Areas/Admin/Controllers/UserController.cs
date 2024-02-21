using GamesLibrary.Areas.Admin.Models.User;
using GamesLibrary.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NuGet.Protocol;
using System.Composition.Convention;

namespace GamesLibrary.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
	[Area("Admin")]
	public class UserController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<Users> signInManager;
        private readonly UserManager<Users> userManager;
        private readonly GamesLibraryContext context;

        public UserController(RoleManager<IdentityRole> roleManager, SignInManager<Users> signInManager, UserManager<Users> userManager, GamesLibraryContext libraryContext)
        {
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.userManager = userManager;
            context = libraryContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var vM = new IndexViewModel() { Context = context, UserManager = userManager };
            return View(vM);
        }

        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                var result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    ModelState.AddModelError("", "Enter role name again");
            }
            return View(role);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound();
            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(string id, IdentityRole role)
        {
            if (id != role.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var oldRole = await roleManager.FindByIdAsync(role.Id);
                oldRole.Name = role.Name;
                var result = await roleManager.UpdateAsync(oldRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
            }
            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = await roleManager.FindByIdAsync(id);
                if (role is not null)
                {
                    await roleManager.DeleteAsync(role);
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError("", "Error in delete");
            return await EditRole(id);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (ModelState.IsValid)
            {
                Users user = await userManager.FindByIdAsync(id);
                if (user is not null)
                {
                    context.Users.Remove(user);
                    await context.SaveChangesAsync();
                }
            }
            ModelState.AddModelError("", "Error with deleting user");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> AddUser()
        {
            AddUserViewModel vM = new AddUserViewModel() { AllRoles = context.Roles.ToList() };
            return View(vM);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserViewModel vM)
        {
            if (ModelState.IsValid)
            {
                Users user = new Users() { UserName = vM.UserName, Email = vM.Email };
                var result = await userManager.CreateAsync(user, vM.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRolesAsync(user, vM.UserRoles);
                    return RedirectToAction("Index");
                }
                else
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
            }
            vM.AllRoles = context.Roles.ToList();
            return View(vM);
        }
    }
}
