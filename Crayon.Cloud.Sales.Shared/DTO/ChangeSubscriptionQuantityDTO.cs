using Crayon.Cloud.Sales.Domain.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Crayon.Cloud.Sales.Shared.DTO
{
   public class ChangeSubscriptionQuantityDTO
    {
        [Required(ErrorMessage = "Subscription Id is required")]
        [ValidateIntValue]
        public int SubscriptionId { get; set; }
        [Required(ErrorMessage = "New quantity value is required")]
        [ValidateIntValue]
        public int NewQuantity { get; set; }
    }
}
