﻿namespace GamesLibrary.Data
{
    public class UserGamesLibrary
    {
        public int Id { get; set; }
        public int? Rate { get; set; }
        public User User { get; set; } = default!;
        public Game Game { get; set; } = default!;
         
    }
}
