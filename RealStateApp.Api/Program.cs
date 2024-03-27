using RealStateApp.Api.Extensions;
using RealStateApp.Infraestructure.Persistence;
using RealStateApp.Infraestructure.Identity;
using RealStateApp.Infraestructure.Identity.Seeds;
using RealStateApp.Infraestructure.Shared;
using RealStateApp.Core.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerExtension();
builder.Services.AddApiVersioningExtension();
builder.Services.AddHealthChecks();
builder.Services.AddApiVersioningExtension();
builder.Services.AddApiVersioning();
builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.AddIdentityLayerForApi(builder.Configuration);
builder.Services.AddSharedInfraestrucutre(builder.Configuration);
builder.Services.AddApplicationLayer();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

await app.Services.AddIdentitySeedsConfiguration();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UserSwaggerExtension();
app.UseErrorHandlingMidleweare();
app.UseHealthChecks("/health");
app.UseSession();

app.MapControllers();

app.Run();
