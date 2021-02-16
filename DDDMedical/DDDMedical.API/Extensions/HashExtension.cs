using DDDMedical.Domain.Services.Hash;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DDDMedical.API.Extensions
{
    public static class HashExtension
    {
        public static IServiceCollection AddCustomizedHash(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<HashingOptions>(configuration.GetSection(HashingOptions.Hashing));
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            return services;
        }
    }
}