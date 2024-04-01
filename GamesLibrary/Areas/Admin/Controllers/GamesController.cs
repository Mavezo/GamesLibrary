using GamesLibrary.Areas.Admin.Models.Games;
using GamesLibrary.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GamesLibrary.Areas.Admin.Controllers
{
	[Authorize(Roles = "admin")]
	[Area("Admin")]
	public class GamesController : Controller
	{
		private readonly GamesLibraryContext context;
		private readonly RoleManager<IdentityRole> roleManager;
		private readonly SignInManager<User> signInManager;
		private readonly UserManager<User> userManager;

		public GamesController(GamesLibraryContext context, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager, UserManager<User> userManager)
		{
			this.context = context;
			this.roleManager = roleManager;
			this.signInManager = signInManager;
			this.userManager = userManager;
		}

		// GET: GamesController
		public async Task<IActionResult> Index()
		{
			return View(context.Games.Include(g => g.Genres).Include(g => g.Developers).ToList());
		}

		// GET: GamesController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: GamesController/Create
		public ActionResult Create()
		{
			CreateViewModel vM = new CreateViewModel()
			{
				AllGenres = context.Genres.Select(g=> new SelectListItem { Text = g.GenreName, Value = g.Id.ToString()})
				.ToList()
			};
			return View(vM);
		}

		[HttpPost]
		public IActionResult Create(CreateViewModel vM)
		{
			return null;
		}


		// GET: GamesController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: GamesController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: GamesController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: GamesController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
