using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealStateApp.Core.Application.Interfaces.IAccount;
using RealStateApp.Core.Domain.Settings;
using RealStateApp.Infraestructure.Identity.Context;
using RealStateApp.Infraestructure.Identity.Entities;
using RealStateApp.Infraestructure.Identity.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Infraestructure.Identity
{
    public static class ServiceRegistrator
    {
        public static void AddIdentityLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                options.UseSqlServer(configuration.GetConnectionString("Identityconexion"),
                m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
            });


            services.AddIdentity<ApplicationUser, IdentityRole>()
             .AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

            services.Configure<JwtSettings>(configuration.GetSection("JWTSettings"));

            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IAccountServiceApi, AccountServiceApi>();

        }
    }
}
