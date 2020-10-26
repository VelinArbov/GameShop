namespace GameShop.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;

    using GameShop.Services.Data;
    using GameShop.Web.ViewModels;
    using GameShop.Web.ViewModels.Game;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    public class HomeController : BaseController
    {
        private readonly IGameService gameService;
      

        public HomeController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        public IActionResult Index(int page = 1, string searchString = null)
        {
            
            this.ViewData["CurrentFilter"] = searchString;
            if (!string.IsNullOrEmpty(searchString))
            {
                var viewModel = new AllGamesViewModel
                {
                    Games = this.gameService.GetAll<GameViewModel>().Where(x
                        => x.Title.ToLower().Contains(searchString.ToLower()) || x.Title.ToLower().Contains(searchString.ToLower())),
                };
                return this.View(viewModel);
            }

            this.ViewBag.Id = this.HttpContext.Session.Id;
            var view = new AllGamesViewModel
            {
                Games = this.gameService.GetAll<GameViewModel>(),
            };

            return this.View(view);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }




    }
}
