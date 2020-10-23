namespace GameShop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using GameShop.Data.Common.Repositories;
    using GameShop.Data.Models;
    using Microsoft.AspNetCore.Http;

    public class ShoppingCardService : IShoppingCartService
    {
        private readonly IRepository<CartItem> repository;
        private readonly IDeletableEntityRepository<Game> gameRepository;
        private readonly HttpContextAccessor httpContextAccessor;
        public const string CartSessionKey = "CartId";

        public ShoppingCardService(
            IRepository<CartItem> repository,
            IDeletableEntityRepository<Game> gameRepository,
            HttpContextAccessor httpContextAccessor)
        {
            this.repository = repository;
            this.gameRepository = gameRepository;
            this.httpContextAccessor = httpContextAccessor;
        }

        public string ShoppingCartId { get; set; }

        public void AddToCart(string id)
        {
            // Retrieve the product from the database.

            var cartItem = this.repository.All().SingleOrDefault(
                c => c.CartId == this.ShoppingCartId
                && c.GameId == id);
            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists.                 
                cartItem = new CartItem
                {
                    ItemId = Guid.NewGuid().ToString(),
                    GameId = id,
                    CartId = this.ShoppingCartId,
                    Game = this.gameRepository.All().SingleOrDefault(
                   p => p.Id == id),
                    Quantity = 1,
                    DateCreated = DateTime.Now,
                };

                this.repository.AddAsync(cartItem);
            }
            else
            {
                // If the item does exist in the cart,                  
                // then add one to the quantity.                 
                cartItem.Quantity++;
            }

            this.repository.SaveChangesAsync();
        }
    }
}
