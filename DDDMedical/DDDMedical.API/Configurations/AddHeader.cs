using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DDDMedical.API.Configurations
{
    public class AddHeader : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "CustomHeader",
                In = ParameterLocation.Header,
                Required = true
            });
        }
    }
}