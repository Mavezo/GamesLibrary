namespace GamesLibrary.Data
{
    public class UserGamesRecomendation
    {
        public int Id { get; set; }
        public Games Game { get; set; } = default!;
        public Users User { get; set; } = default!;
    }
}
