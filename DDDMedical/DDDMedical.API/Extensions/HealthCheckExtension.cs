using DDDMedical.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HealthChecks.UI.Core;

namespace DDDMedical.API.Extensions
{
    public static class HealthCheckExtension
    {
        public static IServiceCollection AddCustomizedHealthCheck(this IServiceCollection services,
            IConfiguration configuration, IWebHostEnvironment env)
        {
            if (!env.IsProduction() && !env.IsStaging()) return services;
            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("DefaultConnection"))
                .AddDbContextCheck<ApplicationDbContext>();

            services.AddHealthChecksUI(setupSettings: setup =>
            {
                setup.AddHealthCheckEndpoint("endpoint1", "/health");
            });

            return services;
        }

        public static void UseCustomizedHealthCheck(IEndpointRouteBuilder endpoints, IWebHostEnvironment env)
        {
            if (env.IsProduction() || env.IsStaging())
            {
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                });

                endpoints.MapHealthChecksUI();
            }
        }
    }
}