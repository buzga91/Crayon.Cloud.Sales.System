using Crayon.Cloud.Sales.Domain.Models;

namespace Crayon.Cloud.Sales.Shared.DTO
{
   public class AccountDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CustomerId { get; set; }
        public IEnumerable<SubscriptionDTO> PurchasedSoftwareLicenses { get; set; }
    }
}
