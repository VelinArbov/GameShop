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
            try
            {

                var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(this.HttpContext.Session, "cart");
                this.ViewBag.cart = cart;
                this.ViewBag.Count = cart.Count();
                this.ViewBag.quantity = cart.Select(item => item.Quantity);
                this.ViewBag.total = cart.Sum(item => item.Game.Price * item.Quantity);
                this.ViewBag.TotalWithoutVAT = cart.Sum(item => item.Game.Price * item.Quantity) * 0.8M;               
                return this.View();
            }
            catch
            {
                this.TempData["ErrorMessage"] = "Shopping cart is empty";
                this.TempData["InfoMessage"] = null;
                return this.Redirect("/Home/Index");

            }
        }

        [Route("buy/{id}")]
        public IActionResult Buy(string id)
        {
            var game = this.gameService.GetGameById(id);
            var gameModel = new GameViewModel();
            Microsoft.AspNetCore.Http.HttpContext httpContext = this.HttpContext;
            if (SessionHelper.GetObjectFromJson<List<CartItem>>(httpContext.Session, "cart") == null)
            {
                List<CartItem> cart = new List<CartItem>();
                cart.Add(new CartItem { Game = game, Quantity = 1 });
                SessionHelper.SetObjectAsJson(this.HttpContext.Session, "cart", cart);
            }
            else
            {
                List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(httpContext.Session, "cart");
                int index = this.isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new CartItem { Game = game, Quantity = 1 });
                }

                SessionHelper.SetObjectAsJson(httpContext.Session, "cart", cart);
            }

            return this.RedirectToAction("Index");
        }

        [Route("remove/{id}")]
        public IActionResult Remove(string id)
        {
            Microsoft.AspNetCore.Http.HttpContext httpContext = this.HttpContext;
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(httpContext.Session, "cart");
            int index = this.isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(this.HttpContext.Session, "cart", cart);
            return this.RedirectToAction("Index");
        }

        private int isExist(string id)
        {
            Microsoft.AspNetCore.Http.HttpContext httpContext = this.HttpContext;
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(httpContext.Session, "cart");
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
