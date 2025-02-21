using Crayon.Cloud.Sales.Domain.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Crayon.Cloud.Sales.Shared.DTO
{
    public class PurchasedSoftwareDTO
    {
        [Required(ErrorMessage = "Quantity is required")]
        [ValidateIntValue]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Name  is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "State  is required")]
        public string State { get; set; }

        [Required(ErrorMessage = "Software validation date time is required")]
        [DataType(DataType.Date)]
        [FutureDate(ErrorMessage = "Date must be in the future")]
        public DateTime ValidTo { get; set; }
    }
}
