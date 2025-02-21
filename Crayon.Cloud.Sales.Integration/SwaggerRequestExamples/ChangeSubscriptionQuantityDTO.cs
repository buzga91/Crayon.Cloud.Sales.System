using Crayon.Cloud.Sales.Shared.DTO;
using Swashbuckle.AspNetCore.Filters;

namespace Crayon.Cloud.Sales.Integration.SwaggerRequestExamples
{
   public class ChangeSubscriptionQuantityDTOExample : IExamplesProvider<ChangeSubscriptionQuantityDTO>
    {
        public ChangeSubscriptionQuantityDTO GetExamples()
        {
            return new ChangeSubscriptionQuantityDTO
            {
                SubscriptionId = 3,
                NewQuantity = 10
            };
        }
    }
}