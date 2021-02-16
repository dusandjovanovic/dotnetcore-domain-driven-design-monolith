using DDDMedical.API.Configurations;
using DDDMedical.API.Extensions;
using DDDMedical.Infrastructure.IoC;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DDDMedical.API
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomizedDatabase(Configuration, _env);
            services.AddCustomizedAuth(Configuration);
            services.AddCustomizedHttp(Configuration);
            services.AddMapperSetup();
            services.AddMediatR(typeof(Startup));
            services.AddCustomizedHash(Configuration);
            services.AddCustomizedSwagger(_env);
            services.AddCustomizedHealthCheck(Configuration, _env);
            
            RegisterServices(services);

            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.IgnoreNullValues = true);
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCustomizedErrorHandling(_env);
            app.UseRouting();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            app.UseCustomizedAuth();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                HealthCheckExtension.UseCustomizedHealthCheck(endpoints, _env);
            });
            app.UseCustomizedSwagger(_env);
        }

        private static void RegisterServices(IServiceCollection services)
        {
            Injector.RegisterServices(services);
        }
    }
}