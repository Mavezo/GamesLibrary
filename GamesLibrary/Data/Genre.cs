namespace GamesLibrary.Data
{
    public class Genre
    {
        public int Id { get; set; }
        public string GenreName { get; set; } = default!;
        public List<Game> Games { get; set; } = default!;
    }
}
