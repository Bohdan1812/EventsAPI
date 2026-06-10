using Application.Persistence.Repositories;
using Application.Persistence.Services;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Application.Persistence.Services.Authentication;
using Infrastructure.Models;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services
                .AddPersistance(configuration);

            return services;
        }

        public static IServiceCollection AddPersistance(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
           var dbConnection = Environment.GetEnvironmentVariable("DB_CONNECTION") 
           ?? configuration.GetConnectionString("EventsSqlDb");
           
            services.AddDbContext<EventAppDbContext>(options =>
               options.UseNpgsql(dbConnection));
                
            services.AddAuthorization();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]!)),
                    ClockSkew = TimeSpan.Zero
                };
            });
            // services.AddIdentityApiEndpoints<ApplicationUser>()
            //     .AddEntityFrameworkStores<EventAppDbContext> ();

            services.AddSingleton(TimeProvider.System);

            services.AddDataProtection();

            services.AddIdentityCore<ApplicationUser>()
                .AddEntityFrameworkStores<EventAppDbContext>()
                .AddDefaultTokenProviders()
                .AddSignInManager<SignInManager<ApplicationUser>>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOrganizerRepository, OrganizerRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IJoinRequestRepository, JoinRequestRepository>();
            services.AddScoped<IInviteRepository, InviteRepository>();
            services.AddScoped<IParticipationRepository, ParticipationRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IUserPhotoService, UserPhotoService>();    
            services.AddScoped<IEventPhotoService, EventPhotoService>();
            services.AddScoped<JwtTokenGenerator>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            
            return services;
        }
    } 
}
