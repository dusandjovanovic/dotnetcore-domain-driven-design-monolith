using System;
using DDDMedical.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace DDDMedical.API.Configurations
{
    public static class AddMapper
    {
        public static void AddMapperSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(AutoMapperConfig.RegisterMappings());
        }
    }
}