using GamesLibrary.Areas.Library.Models.Games;
using GamesLibrary.Data;
using GamesLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Security.Policy;

namespace GamesLibrary.Areas.Library.Controllers
{
    [Area("Library")]
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
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 20;
            var gamesPerPage = context.Games.OrderByDescending(g => g.RateCount).Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo()
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = await context.Games.CountAsync()
            };
            IndexViewModel vM = new IndexViewModel() { Games = gamesPerPage, PageInfo = pageInfo };
            return View(vM);

        }

        // GET: GamesController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var game = await context.Games
                .Include(g => g.Genres)
                .Include(g => g.Developers)
                .Include(g => g.ScreenshotLinks)
                .Include(g => g.VideoLinks)
                .FirstOrDefaultAsync(g => g.Id == id);
            return View(game);
        }

		// GET: GamesController/Create
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> Create()
        {
            CreateViewModel vM = new CreateViewModel()
            {
                AllGenres = await context.Genres
                .Select(g => new SelectListItem { Text = g.GenreName, Value = g.Id.ToString() })
                .ToListAsync(),
                AllDevelopers = await context.Developers
                .Select(d => new SelectListItem { Text = d.Name, Value = d.Id.ToString() })
                .ToListAsync()
            };
            return View(vM);
        }

		[Authorize(Roles = "admin")]
        [HttpPost]
		public IActionResult Create(CreateViewModel vM)
        {
            if (ModelState.IsValid)
            {

                List<Genre> genres = context.Genres.Where(g => vM.GenreIds.Contains(g.Id)).ToList();
                List<Developer> developers = context.Developers.Where(d => vM.DevelopersIds.Contains(d.Id)).ToList();

                List<string> screens = vM.ScreenshotLinks.Where(t => t != string.Empty).ToList();
                screens.ForEach(t => t.Trim());

                List<string> videos = vM.VideoLinks.Where(t => t != string.Empty).ToList();
                videos.ForEach(t => t.Trim());

                Game newGame = new Game()
                {
                    Name = vM.Game.Name,
                    Description = vM.Game.Description,
                    Price = vM.Game.Price,
                    PosterImagePath = vM.Game.PosterImagePath,
                    RateCount = vM.Game.RateCount,
                    AverageRate = vM.Game.AverageRate,
                    Genres = genres,
                    Developers = developers,
                    ReleaseDate = vM.Game.ReleaseDate,
                    ScreenshotLinks = screens.Select(t => new ScreenshotLink() { Link = t }).ToList(),
                    VideoLinks = videos.Select(t => new VideoLink() { Link = t }).ToList()
                };

                context.Games.Add(newGame);
                var result = context.SaveChanges();


                return RedirectToAction("Index", "Games");
            }
            else
            {
                vM.AllDevelopers = context.Developers
                .Select(d => new SelectListItem { Text = d.Name, Value = d.Id.ToString() })
                .ToList();
                vM.AllGenres = context.Genres
                .Select(g => new SelectListItem { Text = g.GenreName, Value = g.Id.ToString() })
                .ToList();

                return View(vM);
            }
        }

		// GET: GamesController/Edit/5
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> Edit(int id)
        {
            EditViewModel vM = new EditViewModel();

            Game? game = await context.Games
                .Include(t => t.Genres)
                .Include(t => t.Developers)
                .Include(t => t.VideoLinks)
                .Include(t => t.ScreenshotLinks)
                .FirstOrDefaultAsync(t => t.Id == id)!;
            if (game == null)
            {
                return NotFound();
            }

            vM.Game = game;
            vM.AllGenres = await context.Genres
                .Select(g => new SelectListItem
                {
                    Text = g.GenreName,
                    Value = g.Id.ToString(),
                    Selected = game.Genres!.Contains(g)
                }).ToListAsync();

            vM.AllDevelopers = await context.Developers.
                Select(d => new SelectListItem
                {
                    Text = d.Name,
                    Value = d.Id.ToString(),
                    Selected = game.Developers!.Contains(d)
                }).ToListAsync();

            return View(vM);
        }

        // POST: GamesController/Edit/5
		[Authorize(Roles = "admin")]
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

        // POST: GamesController/DeleteImage
		[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            if (ModelState.IsValid)
            {
                ScreenshotLink? link = await context.Screenshots.FirstOrDefaultAsync(s => s.Id == imageId);
                if (link != null)
                {
                    int gameId = (await context.Games.Where(g => g.ScreenshotLinks!.Contains(link)).FirstOrDefaultAsync())!.Id;
                    context.Screenshots.Remove(link);
                    await context.SaveChangesAsync();
                    return RedirectToAction($"Edit", new { id = gameId });
                }
            }
            ModelState.AddModelError("", "Error with deleting image");
            return RedirectToAction($"Index");
        }

        // POST: GamesController/AddImage
		[Authorize(Roles = "admin")]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddImage(string imageLink, int gameId)
        {
            if (ModelState.IsValid)
            {
                Uri? uriResult;
                bool result = Uri.TryCreate(imageLink, UriKind.Absolute, out uriResult)
                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                if (result)
                {
                    ScreenshotLink link = new ScreenshotLink() { Link = imageLink };
                    Game? game = await context.Games.Include(t => t.ScreenshotLinks).FirstOrDefaultAsync(g => g.Id == gameId);
                    if (game != null)
                    {
                        game.ScreenshotLinks!.Add(link);
                        await context.SaveChangesAsync();
                        return RedirectToAction($"Edit", new { id = gameId });
                    }
                }
            }
            ModelState.AddModelError("", "Error with adding image");
            return RedirectToAction($"Index");
        }

        // POST: GamesController/AddVideo
		[Authorize(Roles = "admin")]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVideo(string videoLink, int gameId)
        {
            if (ModelState.IsValid)
            {
                var link = new VideoLink() { Link = videoLink };
                Game? game = await context.Games.Include(t => t.VideoLinks).FirstOrDefaultAsync(g => g.Id == gameId);
                if (game != null)
                {
                    game.VideoLinks!.Add(link);
                    await context.SaveChangesAsync();
                    return RedirectToAction($"Edit", new { id = gameId });
                }
            }
            ModelState.AddModelError("", "Error with adding image");
            return RedirectToAction($"Index");
        }

        // POST: GamesController/DeleteVideo
		[Authorize(Roles = "admin")]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVideo(int videoId)
        {
            if (ModelState.IsValid)
            {
                VideoLink link = await context.Videos.FirstOrDefaultAsync(s => s.Id == videoId);
                if (link != null)
                {
                    int gameId = (await context.Games.Where(g => g.VideoLinks.Contains(link)).FirstOrDefaultAsync())!.Id;
                    context.Videos.Remove(link);
                    await context.SaveChangesAsync();
                    return RedirectToAction($"Edit", new { id = gameId });
                }
            }
            ModelState.AddModelError("", "Error with deleting image");
            return RedirectToAction($"Index");
        }

        // GET: GamesController/Delete/50
		[Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GamesController/Delete/5
		[Authorize(Roles = "admin")]
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
