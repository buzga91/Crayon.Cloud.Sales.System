using Crayon.Cloud.Sales.Shared.DTO;
using Swashbuckle.AspNetCore.Filters;

namespace Crayon.Cloud.Sales.Integration.SwaggerRequestExamples
{
    public class ProvisionSubscriptionDTOExample : IExamplesProvider<ProvisionSubscriptionDTO>
    {
        public ProvisionSubscriptionDTO GetExamples()
        {
            return new ProvisionSubscriptionDTO
            {
                AccountId = 3,
                Quantity = 1,
                SoftwareId = 5,
                SoftwareName = "Microsoft Teams",
                State = "Active",
                ValidTo = DateTime.Now.AddYears(1)
            };
        }
    }
}
