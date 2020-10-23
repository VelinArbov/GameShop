namespace GameShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class CartItem
    {
        [Key]
        public string ItemId { get; set; }

        public string CartId { get; set; }

        public int Quantity { get; set; }

        public DateTime DateCreated { get; set; }

        public string GameId { get; set; }

        public virtual Game Game { get; set; }
    }
}
