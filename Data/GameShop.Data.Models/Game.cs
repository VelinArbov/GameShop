namespace GameShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    using GameShop.Data.Common.Models;

    public class Game : IDeletableEntity
    {
        public Game()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Users = new HashSet<UserGames>();
        }

        public string Id { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public string ImageURL { get; set; }

        public string Description { get; set; }

        public virtual ICollection<UserGames> Users { get; set; }

        public DateTime RealaseDate { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
