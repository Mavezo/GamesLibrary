using Microsoft.AspNetCore.Identity;
using System.Net;

namespace GamesLibrary.Data
{
    public class User : IdentityUser
    {
        public List<Games> Recomendations { get; set; } = default!;
        public List<UserGamesLibrary> GamesLibrary { get; set; } = default!;
    }
}
