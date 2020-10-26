namespace GameShop.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GameShop.Data.Models;
    using GameShop.Services.Data;
    using GameShop.Web.Helpers;
    using GameShop.Web.ViewModels.Game;
    using Microsoft.AspNetCore.Mvc;

    public class CartController : BaseController
    {
        private readonly IGameService gameService;

        public CartController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(this.HttpContext.Session, "cart");
            this.ViewBag.cart = cart;
            this.ViewBag.total = cart.Sum(item => item.Game.Price * item.Quantity);
            return this.View();
        }

        [Route("buy/{id}")]
        public IActionResult Buy(string id)
        {
            var game = this.gameService.GetGameById(id);
            var gameModel = new GameViewModel();
            if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") == null)
            {
                List<CartItem> cart = new List<CartItem>();
                cart.Add(new CartItem { Game = game, Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new CartItem { Game = game, Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }

        [Route("remove/{id}")]
        public IActionResult Remove(string id)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        private int isExist(string id)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Game.Id.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }

    }
}
