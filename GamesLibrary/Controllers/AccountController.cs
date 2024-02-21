using GamesLibrary.Data;
using GamesLibrary.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.DependencyResolver;
using System.Security.Policy;

namespace GamesLibrary.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<Users> userManager;
		private readonly SignInManager<Users> signInManager;
		private readonly GamesLibraryContext libraryContext;

		public AccountController(UserManager<Users> userManager, SignInManager<Users> signInManager, GamesLibraryContext libraryContext)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.libraryContext = libraryContext;
		}

		[HttpGet]
		public async Task<IActionResult> Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegistrationViewModel vM)
		{
			if (ModelState.IsValid)
			{
				Users user = new Users() { UserName = vM.Nickname, Email = vM.Email };
				var result = await userManager.CreateAsync(user, vM.Password);
				if (result.Succeeded)
				{
					await signInManager.SignInAsync(user, false);
					return RedirectToAction("Index", "Home");
				}
				else
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
			}
			return View(vM);
		}

		[HttpGet]
		public async Task<IActionResult> Login(string returnUrl = null)
		{
			return View(new LoginViewModel { ReturnURL = returnUrl });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel vM)
		{
			if (ModelState.IsValid)
			{
				var result = await signInManager.PasswordSignInAsync(vM.Nickname, vM.Password, vM.RememberMe, false);
				if (result.Succeeded)
				{
					if (!string.IsNullOrEmpty(vM.ReturnURL) && Url.IsLocalUrl(vM.ReturnURL))
					{
						return Redirect(vM.ReturnURL);
					}
					else
					{
						return RedirectToAction("Index", "Home");
					}
				}
				else
					ModelState.AddModelError("", "Incorrect login or password");
			}
			return View(vM);
		}

		[HttpGet]
		public async Task<IActionResult> AccessDenied()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			if (signInManager.IsSignedIn(User))
			{
				await signInManager.SignOutAsync();
			}
			else
			{
				ModelState.AddModelError("", "Logout error");
			}
			return RedirectToAction("Index", "Home");
		}

	}
}
