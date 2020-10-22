namespace GameShop.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using GameShop.Services.Data;
    using GameShop.Web.ViewModels.Administration.Dashboard;
    using GameShop.Web.ViewModels.Game;
    using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult> CreateGame(GameViewModel model)
        {
            await this.gameService.CreateAsync(model.Title, model.Description, model.ImageURL, model.Price, model.RealaseDate);
            return this.RedirectToAction("Index");
        }

        public IActionResult EditGame()
        {
            return this.View("Game/EditGame");
        }

        [HttpPost]
        public async Task<ActionResult> EditGame(GameViewModel model)
        {
            await this.gameService.EditAsync(model.Id, model.Title, model.Description, model.ImageURL, model.Price);
            return this.Redirect("/Administration/Dashboard");
        }

        public async Task<ActionResult> Delete(string id)
        {
            await this.gameService.DeleteAsync(id);
            return this.RedirectToAction("Index");
        }
    }
}
