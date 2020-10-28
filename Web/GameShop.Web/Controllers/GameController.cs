namespace GameShop.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using GameShop.Data.Models;
    using GameShop.Services.Data;
    using GameShop.Web.Helpers;
    using GameShop.Web.ViewModels.Game;
    using Microsoft.AspNetCore.Mvc;

    public class GameController : BaseController
    {
        private readonly IGameService gameService;

        public GameController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        public IActionResult Details(string id)
        {



            var viewModel = this.gameService.GetJobById<GameViewModel>(id);

            return this.View(viewModel);
        }

        //public IActionResult BuyGame(string id)
        //{
        //    try
        //    {
        //        var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        //        this.gameService.BuyGame(id, userId);
        //        this.TempData["InfoMessage"] = "Добавихте успешно тази игра ";
        //    }
        //    catch (Exception e)
        //    {
        //        this.TempData["ErrorMessage"] = e.Message;
        //        this.TempData["InfoMessage"] = null;
        //    }

        //    return this.Redirect("/Home/Index");

        //}

        public IActionResult Buy(string id)
        {
            var game = this.gameService.GetGameById(id);
            var gameModel = new GameViewModel();
            if (SessionHelper.GetObjectFromJson<List<CartItem>>(this.HttpContext.Session, "cart") == null)
            {
                List<CartItem> cart = new List<CartItem>();
                cart.Add(new CartItem { Game = game, Quantity = 1 });
                SessionHelper.SetObjectAsJson(this.HttpContext.Session, "cart", cart);
            }
            else
            {
                List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(this.HttpContext.Session, "cart");
                int index = this.isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new CartItem { Game = game, Quantity = 1 });
                }

                SessionHelper.SetObjectAsJson(this.HttpContext.Session, "cart", cart);
            }

            this.TempData["InfoMessage"] = "Добавихте успешно тази игра ";
            return this.Redirect("/Home/Index");
        }

        private int isExist(string id)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(this.HttpContext.Session, "cart");
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
