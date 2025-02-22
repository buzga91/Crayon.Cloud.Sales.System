using Crayon.Cloud.Sales.Domain.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Crayon.Cloud.Sales.Shared.DTO
{
    public class ProvisionSubscriptionDTO
    {
        [Required(ErrorMessage = "Software Id is required")]
        [ValidateIntValue]
        public int SoftwareId { get; set; }

        [Required(ErrorMessage = "Software name is required")]
        public string SoftwareName { get; set; }

        [Required(ErrorMessage = "Account id is required")]
        [ValidateIntValue]
        public int AccountId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [ValidateIntValue]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Subscription validation date time is required")]
        [DataType(DataType.Date)]
        [FutureDate(ErrorMessage = "Date must be in the future")]
        public DateTime ValidTo { get; set; }

        [Required(ErrorMessage = "State is required")]
        [ValidateStateValueAttribute]
        public string State { get; set; }
    }
}
