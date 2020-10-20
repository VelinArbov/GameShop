namespace GameShop.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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


    }
}
