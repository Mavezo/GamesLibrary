using GamesLibrary.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GamesLibrary.Areas.Admin.Models.Games
{
    public class CreateViewModel
    {
        public Game Game { get; set; }
        public int[] GenreIds { get; set; }
        public List<SelectListItem> AllGenres { get; set; }
        public List<string> ScreenshotLinks { get; set; }
        public List<string> VideoLinks { get; set; }
    }

}
