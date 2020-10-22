namespace GameShop.Web.ViewModels.Game
{
    using System;

    using GameShop.Data.Models;
    using GameShop.Services.Mapping;

    public class GameViewModel : IMapFrom<Game>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public string ImageURL { get; set; }

        public string ShortDesc => this.Description.Length > 20 ? this.Description.Substring(0, 15) + "..." : this.Description;

        public string Description { get; set; }

        public DateTime RealaseDate { get; set; }

        public string ShortDate => this.RealaseDate.ToString("d");
    }
}