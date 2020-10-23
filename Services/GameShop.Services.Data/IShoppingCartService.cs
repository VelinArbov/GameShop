namespace GameShop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.AspNetCore.Http;

    public interface IShoppingCartService
    {
        void AddToCart(string id);

       
    }
}
