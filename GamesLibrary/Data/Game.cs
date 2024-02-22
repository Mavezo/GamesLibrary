using System.ComponentModel.DataAnnotations;

namespace GamesLibrary.Data
{
	public class Game
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; } = default!;
		public string Description { get; set; } = default!;
		public int Price { get; set; }
		public string PosterImagePath { get; set; } = default!;
		public List<VideoLink> VideoLinks { get; set; } = default!;
		public List<ScreenshotLink> ScreenshotLinks { get; set; } = default!;
		public List<Developer> Developers { get; set; } = default!;
		public DataType ReleaseDate { get; set; } = default!;
		public decimal? AverageRate { get; set; }
		public int RateCount { get; set; } = 0;
		public List<Genre> Genres { get; set; } = default!;
		public List<User> Recomendation { get; set; } = default!;
	}
	public class VideoLink
	{
		public int Id { get; set; }
		public string Link { get; set; } = default!;	
	}
	public class ScreenshotLink
	{
		public int Id { get; set; }
		public string Link { get; set; } = default!;	
	}


}
