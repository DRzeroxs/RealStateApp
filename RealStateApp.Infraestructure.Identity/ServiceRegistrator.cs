using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RealStateApp.Core.Application.Dto.Account;
using RealStateApp.Core.Application.Interfaces.IAccount;
using RealStateApp.Core.Application.Wrappers;
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
        public static void AddIdentityLayerForApi(this IServiceCollection services, IConfiguration configuration)
        {
            ContextConfiguration(services, configuration);


            services.AddIdentity<ApplicationUser, IdentityRole>()
             .AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

            services.Configure<JwtSettings>(configuration.GetSection("JWTSettings"));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration[
                        "JWTSettings:Issuer"
                        ],
                    ValidAudience = configuration[
                        "JWTSettings:Audience"
                        ],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration[
                        "JWTSettings:Key"
                        ])),
                };
                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";
                        return c.Response.WriteAsync(c.Exception.ToString());
                    },

                    OnChallenge = c =>
                    {
                        c.HandleResponse();
                        c.Response.StatusCode = 401;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new Response<string> ("Your are not Authorized" ));
                        return c.Response.WriteAsync(result);
                    },
                    OnForbidden = c =>
                    {

                        c.Response.StatusCode = 403;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new Response<string> ("Your are not Authorized to acces this resourced"));
                        return c.Response.WriteAsync(result);
                    }
                };

               
            });
                ServiceConfiguration(services);
        }

        public static void AddIdentityLayerForWeb(this IServiceCollection services, IConfiguration configuration)
        {
            ContextConfiguration(services, configuration);


            services.AddIdentity<ApplicationUser, IdentityRole>()
             .AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

            services.AddAuthentication();

            ServiceConfiguration(services);
        }
        #region"Private Methods"
        private static void ContextConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                options.UseSqlServer(configuration.GetConnectionString("Identityconexion"),
                m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
            });
        }

        private static void ServiceConfiguration(this IServiceCollection services)
        {
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IAccountServiceApi, AccountServiceApi>();
        }
       
        #endregion
    }
}
