using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Crayon.Cloud.Sales.Integration.SwaggerRequestExamples
{
    public class SwaggerRouteParameterExampleFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var customerIdParam = operation.Parameters
                .FirstOrDefault(p => p.Name == "customerId" && p.In == ParameterLocation.Path);

            if (customerIdParam != null)
            {
                customerIdParam.Example = new OpenApiString("2");
            }

            var accountIdParam = operation.Parameters
               .FirstOrDefault(p => p.Name == "accountId" && p.In == ParameterLocation.Path);

            if (accountIdParam != null)
            {
                accountIdParam.Example = new OpenApiString("2");
            }


            var subscriptionIdParam = operation.Parameters
              .FirstOrDefault(p => p.Name == "subscriptionId" && p.In == ParameterLocation.Path);

            if (subscriptionIdParam != null)
            {
                subscriptionIdParam.Example = new OpenApiString("3");
            }
        }
    }
}