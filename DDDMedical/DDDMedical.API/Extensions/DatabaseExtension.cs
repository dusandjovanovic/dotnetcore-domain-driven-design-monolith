using DDDMedical.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DDDMedical.API.Extensions
{
    public static class DatabaseExtension
    {
        public static IServiceCollection AddCustomizedDatabase(this IServiceCollection services,
            IConfiguration configuration, IWebHostEnvironment env)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), 
                    x => x.MigrationsAssembly("DDDMedical.API"));

                if (env.IsProduction()) return;
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            });

            services.AddDbContext<EventStoreSqlContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), 
                    x => x.MigrationsAssembly("DDDMedical.API"));

                if (env.IsProduction()) return;
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            });

            return services;
        }
    }
}