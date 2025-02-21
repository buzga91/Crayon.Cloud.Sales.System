using Crayon.Cloud.Sales.Shared.DTO;
using Swashbuckle.AspNetCore.Filters;

namespace Crayon.Cloud.Sales.Integration.SwaggerRequestExamples
{
    public class ExtendSubscriptionValidDateDTOExample : IExamplesProvider<ExtendSubscriptionValidDateDTO>
    {
        public ExtendSubscriptionValidDateDTO GetExamples()
        {
            return new ExtendSubscriptionValidDateDTO
            {
                SubscriptionId = 3,
                NewValidTo = DateTime.Now.AddYears(1).AddMonths(1)
            };
        }
    }
}