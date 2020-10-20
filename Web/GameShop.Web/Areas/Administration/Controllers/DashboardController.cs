namespace GameShop.Web.Areas.Administration.Controllers
{
    using GameShop.Services.Data;
    using GameShop.Web.ViewModels.Administration.Dashboard;
    using GameShop.Web.ViewModels.Game;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class DashboardController : AdministrationController
    {
      
        private readonly IGameService gameService;

        public DashboardController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult CreateGame()
        {
            return this.View("Game/CreateGame");
        }

        [HttpPost]
        public async Task<ActionResult> CreateCategory(GameViewModel model)
        {
            await this.gameService.CreateAsync(model.Title, model.Description, model.ImageURL, model.Price);       
            return this.RedirectToAction("Index");
        }


    }
}
