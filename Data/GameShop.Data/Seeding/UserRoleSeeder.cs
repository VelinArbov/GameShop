namespace GameShop.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using GameShop.Common;
    using GameShop.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class UserRoleSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetService<RoleManager<ApplicationRole>>();
            var user = await userManager.FindByNameAsync("arbov.v@gmail.com");
            var role = await roleManager.FindByNameAsync(GlobalConstants.AdministratorRoleName);

            var exists = dbContext.UserRoles.Any(x => x.UserId == user.Id && x.RoleId == role.Id);

            if (exists)
            {
                return;
            }

            user.IsAdmin = true;

            dbContext.UserRoles.Add(new IdentityUserRole<string>
            {
                RoleId = role.Id,
                UserId = user.Id
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
