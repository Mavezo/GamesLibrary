using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace GamesLibrary.Data
{
    public class GamesLibraryContext : IdentityDbContext<User>
    {
        public DbSet<Game> Games { get; set; } = default!;
        public DbSet<Genre> Genres { get; set; } = default!;
        public DbSet<UserGamesLibrary> UserGamesLiked { get; set; } = default!;

        public GamesLibraryContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureCreated();
        }
    }
}
