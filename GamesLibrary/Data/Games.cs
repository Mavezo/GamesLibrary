using System.ComponentModel.DataAnnotations;

namespace GamesLibrary.Data
{
	public class Games
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; } = default!;
		public string Description { get; set; } = default!;
		public int Price { get; set; }
		public string PosterImagePath { get; set; } = default!;
		public List<string> VideoLinks { get; set; } = default!;
		public List<string> ScreenshotLinks { get; set; } = default!;
		public List<string> Developers { get; set; } = default!;
		public DataType ReleaseDate { get; set; } = default!;
		public decimal? AverageRate { get; set; }
		public int RateCount { get; set; } = 0;
		public List<Genres> Genres { get; set; } = default!;
	}
}
