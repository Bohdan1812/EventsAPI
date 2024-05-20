using Application.Persistence.Repositories;
using Domain.Common.Models;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
           var dbConnection = configuration["ConnectionStrings:Events:SqlDb"];

            services.AddDbContext<EventAppDbContext>(options =>
                //options.UseSqlServer(dbConnection));
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthorization();

            services.AddIdentityApiEndpoints<ApplicationUser>()
                .AddEntityFrameworkStores<EventAppDbContext> ();
        
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOrganizerRepository, OrganizerRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IJoinRequestRepository, JoinRequestRepository>();
            services.AddScoped<IInviteRepository, InviteRepository>();
            services.AddScoped<IParticipationRepository, ParticipationRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();  
            
            return services;
        }
    } 
}
