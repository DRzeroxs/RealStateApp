using RealStateApp.Infraestructure.Persistence;
using RealStateApp.Infraestructure.Identity;
using RealStateApp.Infraestructure.Identity.Seeds;
using RealStateApp.Infraestructure.Shared;
using RealStateApp.Core.Application;
using RealStateApp.Middlewares;

namespace RealStateApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddPersistenceLayer(builder.Configuration);
            builder.Services.AddIdentityLayerForWeb(builder.Configuration);
            builder.Services.AddSharedInfraestrucutre(builder.Configuration);
            builder.Services.AddApplicationLayer();
            builder.Services.AddTransient<ValidateUserSession, ValidateUserSession>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddSession();
            var app = builder.Build();

            await app.Services.AddIdentitySeedsConfiguration();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
