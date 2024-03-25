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
    public static class DefaultClienteUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser defaultUser = new();

            defaultUser.FirstName = "Manuel";

            defaultUser.LastName = "Gara";

            defaultUser.UserName = "Manuel";

            defaultUser.Email = "Manuel@gmail.com";

            defaultUser.EmailConfirmed = true;

            defaultUser.PhoneNumber = "8093334554";

            defaultUser.ImgUrl = string.Empty;

            defaultUser.TypeOfUser = "Cliente";

            defaultUser.PhoneNumberConfirmed = true;

            defaultUser.IsActive = true;

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = userManager.FindByEmailAsync(defaultUser.Email);

                if (user != null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$work");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Cliente.ToString());

                }
            }
        }
    }
}
