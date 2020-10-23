namespace GameShop.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using GameShop.Services.Data;
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

        public IActionResult BuyGame(string id)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                this.gameService.BuyGame(id, userId);
                this.TempData["InfoMessage"] = "Добавихте успешно тази игра ";
            }
            catch (Exception e)
            {
                this.TempData["ErrorMessage"] = e.Message;
                this.TempData["InfoMessage"] = null;
            }

            return this.Redirect("/Home/Index");

        }
    }
}
