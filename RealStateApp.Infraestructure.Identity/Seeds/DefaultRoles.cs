using Microsoft.AspNetCore.Identity;
using RealStateApp.Core.Application.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Infraestructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Cliente.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Agente.ToString()));
        }
    }
}
