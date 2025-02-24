using Crayon.Cloud.Sales.Domain.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Crayon.Cloud.Sales.Shared.DTO
{
   public class AccountWithPurchasedSubscriptionsDTO
    {
        [Required(ErrorMessage = "Account Id is required")]
        [ValidateIntValue]
        public int Id { get; set; }

        [Required(ErrorMessage = "AccountCcp Id is required")]
        [ValidateIntValue]
        public int AccountCCpId { get; set; }

        [Required(ErrorMessage = "Account name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Customer Id is required")]
        [ValidateIntValue]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Purchased software licenses are required")]
        public IEnumerable<SubscriptionDTO> PurchasedSoftwareLicenses { get; set; }
    }
}
