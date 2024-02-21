namespace GamesLibrary.Data
{
    public class Genres
    {
        public int Id { get; set; }
        public string GenreName { get; set; } = default!;
        public List<Games> Games { get; set; } = default!;
    }
}
