using GamesLibrary.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamesLibrary.Areas.Admin.Controllers
{
    [Area("Admin")]   
    public class GamesController : Controller
    {
		private readonly GamesLibraryContext context;

		public GamesController(GamesLibraryContext context)
        {
			this.context = context;
		}

        public async Task<IActionResult> Index()
        {
            return View(await context.Games.Include(t=>t.Genres).ToListAsync());
            
        }
    }
}
