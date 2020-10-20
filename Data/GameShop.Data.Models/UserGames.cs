namespace GameShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UserGames
    {
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string GameId { get; set; }

        public Game Game { get; set; }
    }
}
