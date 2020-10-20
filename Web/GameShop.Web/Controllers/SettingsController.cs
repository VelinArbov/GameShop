namespace GameShop.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using GameShop.Data.Common.Repositories;
    using GameShop.Data.Models;
    using GameShop.Services.Data;
    using GameShop.Web.ViewModels.Settings;

    using Microsoft.AspNetCore.Mvc;

    public class SettingsController : BaseController
    {
        private readonly IDeletableEntityRepository<Setting> repository;

        public SettingsController(IDeletableEntityRepository<Setting> repository)
        {
            
            this.repository = repository;
        }

        public IActionResult Index()
        {
            
          
            return this.View();
        }

        public async Task<IActionResult> InsertSetting()
        {
            var random = new Random();
            var setting = new Setting { Name = $"Name_{random.Next()}", Value = $"Value_{random.Next()}" };

            await this.repository.AddAsync(setting);
            await this.repository.SaveChangesAsync();

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
