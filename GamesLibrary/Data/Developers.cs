namespace GamesLibrary.Data
{
	public class Developers
	{
		public int Id { get; set; }
		public string Name { get; set; } = default!;
		public List<Games> Games { get; set; } = default!;
	}
}