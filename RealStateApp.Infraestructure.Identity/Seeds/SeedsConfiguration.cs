using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RealStateApp.Infraestructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Infraestructure.Identity.Seeds
{
    public static class SeedsConfiguration
    {
        public static async Task AddIdentitySeedsConfiguration(this IServiceProvider service)
        {
            #region "Seeds"
            using (var scope = service.CreateScope())
            {
                var serviceScope = scope.ServiceProvider;
                try
                {
                    var userManager = serviceScope.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = serviceScope.GetRequiredService<RoleManager<IdentityRole>>();


                    await DefaultRoles.SeedAsync(roleManager);
                    await DefaultAdminUser.SeedAsync(userManager);
                    await DefaultAgenteUser.SeedAsync(userManager); 
                    await DefaultClienteUser.SeedAsync(userManager);    
                }
                catch (Exception ex)
                {

                }
            }
            #endregion
        }
    }
}
