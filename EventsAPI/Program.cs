using Api;
using Api.Hubs;
using Application;
using Domain.Common.Models;
using Infrastructure;
using Microsoft.Extensions.FileProviders;


var builder = WebApplication.CreateBuilder(args);
{
    /*Add services to the container.*/

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    

    builder.Services
        .AddInfrastructure(builder.Configuration)
        .AddApplication()
        .AddPresentation();
}



var app = builder.Build();
{
 
    var userPhotosPath = Path.Combine(app.Environment.ContentRootPath, "UserPhotos");
    var eventPhotosPath = Path.Combine(app.Environment.ContentRootPath, "EventPhotos");

    if (!Directory.Exists(userPhotosPath)) Directory.CreateDirectory(userPhotosPath);
    if (!Directory.Exists(eventPhotosPath)) Directory.CreateDirectory(eventPhotosPath);
 
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    //app.UseExceptionHandler("/error");
    app.MapIdentityApi<ApplicationUser>();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseHttpsRedirection();
    app.MapControllers();
    app.MapHub<ChatHub>("/Chat");
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(builder.Environment.ContentRootPath, "UserPhotos")),
        RequestPath = "/userPhotos"
    });//Need to confiure authorization
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(builder.Environment.ContentRootPath, "EventPhotos")),
        RequestPath = "/eventPhotos"
    });//Need to confiure authorization
    app.Run();
}
