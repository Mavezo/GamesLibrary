namespace GamesLibrary.Data
{
	public class Developer
	{
		public int Id { get; set; }
		public string Name { get; set; } = default!;
		public List<Game> Games { get; set; } = default!;
	}
}