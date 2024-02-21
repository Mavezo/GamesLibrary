using Microsoft.AspNetCore.Identity;
using System.Net;

namespace GamesLibrary.Data
{
    public class Users : IdentityUser
    {
        public List<UserGamesRecomendation> Recomendations { get; set; } = default!;
        public List<UserGamesLibrary> GamesLibrary { get; set; } = default!;
    }
}
