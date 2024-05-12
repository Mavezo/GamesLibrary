using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace GamesLibrary.Data
{
	public class GamesLibraryContext : IdentityDbContext<User>
	{
		public DbSet<Game> Games { get; set; } = default!;
		public DbSet<Genre> Genres { get; set; } = default!;
		public DbSet<Developer> Developers { get; set; } = default!;
		public DbSet<ScreenshotLink> Screenshots { get; set; } = default!;
		public DbSet<VideoLink> Videos { get; set; } = default!;
		public DbSet<UserGamesLibrary> UserGamesLiked { get; set; } = default!;


		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Developer>().HasData(
				new Developer { Id = 1, Name = "HoYoverse" },
				new Developer { Id = 2, Name = "Mihoyo" }
				
				);

			builder.Entity<Genre>().HasData(
					new Genre { Id = 1, GenreName = "Action"},
					new Genre { Id = 2, GenreName = "Adventure"},
					new Genre { Id = 3, GenreName = "RPG"},
					new Genre { Id = 4, GenreName = "MMORPG"}
				);
		}
		public GamesLibraryContext(DbContextOptions options) : base(options)
		{
			//Database.EnsureCreated();
		}
			

	}
}
