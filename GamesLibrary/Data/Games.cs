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
		public List<VideoLinks> VideoLinks { get; set; } = default!;
		public List<ScreenshotLinks> ScreenshotLinks { get; set; } = default!;
		public List<Developers> Developers { get; set; } = default!;
		public DataType ReleaseDate { get; set; } = default!;
		public decimal? AverageRate { get; set; }
		public int RateCount { get; set; } = 0;
		public List<Genres> Genres { get; set; } = default!;
		public List<User> Recomendation { get; set; } = default!;
	}
	public class VideoLinks
	{
		public int Id { get; set; }
		public string Link { get; set; } = default!;	
	}
	public class ScreenshotLinks
	{
		public int Id { get; set; }
		public string Link { get; set; } = default!;	
	}


}
