using System;
using DDDMedical.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace DDDMedical.API.Extensions
{
    public static class HttpExtensions
    {
        public static IServiceCollection AddCustomizedHttp(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddHttpClient("Foo", c =>
                {
                    c.BaseAddress = new Uri(configuration.GetValue<string>("HttpClients:Foo"));
                })
                .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)))
                .AddTypedClient(c => Refit.RestService.For<IFooClient>(c));

            return services;
        }
    }
}