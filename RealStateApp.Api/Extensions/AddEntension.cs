using RealStateApp.Core.Application.Middleweares;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace RealStateApp.Api.Extensions
{
    public static class AddEntension
    {
        public static void UserSwaggerExtension(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Restaurante");
                options.DefaultModelRendering(ModelRendering.Model);
            });
        }
        public static void UseErrorHandlingMidleweare(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandleMiddlewear>();
        }
    }
}
