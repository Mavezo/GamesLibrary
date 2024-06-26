﻿using System.ComponentModel.DataAnnotations;

namespace GamesLibrary.Data
{
	public class Game
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; } = default!;
		public string Description { get; set; } = default!;
		public int Price { get; set; }
		[Display(Name = "Link to the Poster")]
		public string PosterImagePath { get; set; } = default!;
		public List<VideoLink>? VideoLinks { get; set; } = default!;
		public List<ScreenshotLink>? ScreenshotLinks { get; set; } = default!;
		public List<Developer>? Developers { get; set; } = default!;
		[Display(Name = "Release Date")]
		public DateTime? ReleaseDate { get; set; }
		public decimal? AverageRate { get; set; }
		public int RateCount { get; set; } = 0;
		public List<Genre>? Genres { get; set; } = default!;
		public List<UserGamesRecomendation>? Recomendation { get; set; } = default!;
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
