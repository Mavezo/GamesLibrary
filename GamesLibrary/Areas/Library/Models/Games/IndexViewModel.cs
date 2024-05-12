using GamesLibrary.Data;
using GamesLibrary.Models;

namespace GamesLibrary.Areas.Library.Models.Games
{
    public class IndexViewModel
    {
        public IEnumerable<Game> Games { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
