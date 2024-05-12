using GamesLibrary.Data;
using Microsoft.AspNetCore.Identity;

namespace GamesLibrary.Areas.Admin.Models.User
{
    public class IndexViewModel
    {
        public UserManager<Data.User> UserManager { get; set; }
        public GamesLibraryContext Context { get; set; }
    }
}
