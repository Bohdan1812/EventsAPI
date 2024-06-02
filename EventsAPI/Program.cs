using Api;
using Api.Hubs;
using Application;
using Domain.Common.Models;
using Infrastructure;
using Microsoft.AspNetCore.Identity;


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
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    //app.UseExceptionHandler("/error");
    app.MapIdentityApi<ApplicationUser>();
    app.UseAuthorization();
    app.UseHttpsRedirection();
    app.MapControllers();
    app.MapHub<ChatHub>("/Chat");
    app.Run();
}
