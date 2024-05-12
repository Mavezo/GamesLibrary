using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace GamesLibrary.Data
{
	public static class SeedData
	{
		public static async void Initialize(IServiceProvider serviceProvider)
		{
			using (var context = new GamesLibraryContext(
				serviceProvider.GetRequiredService<DbContextOptions<GamesLibraryContext>>()))
			{
				if (context.Games.Any() || context.Developers.Any() || context.Genres.Any())
				{
					return;
				}


				using (StreamReader file = new StreamReader("wwwroot/games.json"))
				{
					using (JsonTextReader reader = new JsonTextReader(file))
					{
						while (reader.Read() && reader.TokenType != JsonToken.StartObject) { }

						int objectsRead = 0;
						
						while ( objectsRead < 5000 && reader.Read()) 
						{
                            await Console.Out.WriteLineAsync($"{objectsRead} skipped");
							reader.Skip();
                            objectsRead++;
						}



						while (objectsRead < 30000 && reader.Read())
						{



							if (reader.TokenType == JsonToken.StartObject)
							{
								JObject obj = JObject.Load(reader);

								var gameGenres = obj["genres"].Value<JArray>().ToObject<HashSet<string>>();
								var gameDevelopers = obj["developers"].Value<JArray>().ToObject<HashSet<string>>();
								var gameScreenshots = obj["screenshots"].Value<JArray>().ToObject<HashSet<string>>();
								var gameVideos = obj["movies"].Value<JArray>().ToObject<HashSet<string>>();

								foreach (var genreName in gameGenres)
								{
									if (!context.Genres.Any(t => t.GenreName == genreName))
									{
										Genre genre = new Genre() { GenreName = genreName };
										await context.Genres.AddAsync(genre);
									}
								}

								foreach (var developersName in gameDevelopers)
								{
									if (!context.Developers.Any(d => d.Name == developersName))
									{
										Developer developer = new Developer() { Name = developersName };
										await context.Developers.AddAsync(developer);
									}
								}


								var positiveScoreCount = obj["positive"].Value<int>();
								var negativeScoreCount = obj["negative"].Value<int>();

								var totalScore = positiveScoreCount + negativeScoreCount;

								var totalAvgScore = 0;

								if (totalScore != 0)
									totalAvgScore = Convert.ToInt32((positiveScoreCount / ((double)totalScore / 100)));



								Game game = new Game()
								{
									Genres = await context.Genres.Where(t => gameGenres.Contains(t.GenreName)).ToListAsync(),
									Developers = await context.Developers.Where(t => gameDevelopers.Contains(t.Name)).ToListAsync(),

									Name = obj["name"].Value<string>(),
									Description = obj["short_description"].Value<string>(),
									Price = obj["price"].Value<int>(),
									PosterImagePath = obj["header_image"].Value<string>(),
									AverageRate = totalAvgScore,
									RateCount = totalScore,
									ReleaseDate = DateTime.Parse(obj["release_date"].Value<string>()),
									ScreenshotLinks = gameScreenshots.Select(s => new ScreenshotLink() { Link = s }).ToList(),
									VideoLinks = gameVideos.Select(v => new VideoLink() { Link = v }).ToList()

								};

								if (await context.Games.ContainsAsync(game))
								{
									objectsRead++;
									continue;
								}

								await context.Games.AddAsync(game);
								await context.SaveChangesAsync();

								await Console.Out.WriteLineAsync($"{game.Name} - added");

								objectsRead++;
							}
						}
						Console.WriteLine(context.Games.Count());




					}

				}
			}
		}
	}
}