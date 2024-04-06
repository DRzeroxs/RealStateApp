using Microsoft.AspNetCore.Identity;
using RealStateApp.Core.Application.Enum;
using RealStateApp.Infraestructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Infraestructure.Identity.Seeds
{
    public static class DefaultDeveloperUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultUser = new();


            defaultUser.FirstName = "Antonio";

            defaultUser.LastName = "Peralta";

            defaultUser.UserName = "Antonio";

            defaultUser.Email = "Developer@gmail.com";

            defaultUser.EmailConfirmed = true;

            defaultUser.PhoneNumber = "8093334554";

            defaultUser.ImgUrl = string.Empty;

            defaultUser.TypeOfUser = "Developer";

            defaultUser.PhoneNumberConfirmed = true;

            defaultUser.Cedula = "40226271827";

            defaultUser.IsActive = true;


            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var usuario = userManager.FindByEmailAsync(defaultUser.Email);
                if (usuario != null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$work");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Agente.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Cliente.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Developer.ToString());
                }
            }

        }
    }
}
