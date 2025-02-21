using Crayon.Cloud.Sales.Domain.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Crayon.Cloud.Sales.Shared.DTO
{
   public class ExtendSubscriptionValidDateDTO
    {
        [Required(ErrorMessage = "Subscription Id is required")]
        [ValidateIntValue]
        public int SubscriptionId { get; set; }
        [Required(ErrorMessage = "New valid subscription date time is required")]
        [DataType(DataType.Date)]
        [FutureDate(ErrorMessage = "Date must be in the future")]
        public DateTime NewValidTo { get; set; }
    }
}
