using Crayon.Cloud.Sales.Domain.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Crayon.Cloud.Sales.Shared.DTO
{
    public class ProvisionSoftwareDTO
    {
        [Required(ErrorMessage = "Software Id is required")]
        [ValidateIntValue]
        public int Id { get; set; }

        [Required(ErrorMessage = "CustomerCcp Id is required")]
        [ValidateIntValue]
        public int CustomerCcpId { get; set; }

        
        [Required(ErrorMessage = "AccountCcp Id is required")]
        [ValidateIntValue]
        public int AccountCcpId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [ValidateIntValue]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }

        [Required(ErrorMessage = "Software validation date time is required")]
        [DataType(DataType.Date)]
        [FutureDate(ErrorMessage = "Date must be in the future")]
        public DateTime ValidTo { get; set; }
    }
}
