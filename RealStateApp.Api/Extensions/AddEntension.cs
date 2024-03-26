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
            });
        }
    }
}
