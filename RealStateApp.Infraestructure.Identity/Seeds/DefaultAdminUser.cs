using Microsoft.AspNetCore.Identity;
using RealStateApp.Core.Application.Enum;
using RealStateApp.Infraestructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Infraestructure.Identity.Seeds
{
    public static class DefaultAdminUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser defaultUser = new();

            defaultUser.FirstName = "Jose";

            defaultUser.LastName = "Encarnacion";

            defaultUser.UserName = "Jose";

            defaultUser.Email = "Jose@gmail.com";

            defaultUser.EmailConfirmed = true;

            defaultUser.ImgUrl = "asas";

            defaultUser.TypeOfUser = "Admin";
            
            defaultUser.IsActive = true;    

            defaultUser.PhoneNumberConfirmed = true;

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = userManager.FindByEmailAsync(defaultUser.Email);

                if (user != null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$work");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());

                }
            }
        }
    }
}
