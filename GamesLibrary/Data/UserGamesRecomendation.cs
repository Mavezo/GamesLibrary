namespace GamesLibrary.Data
{
		public class UserGamesRecomendation
		{
			public int Id { get; set; }
			public Game RecomendedGame { get; set; } = default!;
			public User User { get; set; } = default!;
		}
}
