using Crayon.Cloud.Sales.Domain.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Crayon.Cloud.Sales.Shared.DTO
{
    public class AvailableSoftwareDTO
    {
        [Required(ErrorMessage = "Software Id is required")]
        [ValidateIntValue]
        public int Id { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "MinQuantity is required")]
        [ValidateIntValue]
        public int MinQuantity { get; set; }

        [Required(ErrorMessage = "MaxQuantity is required")]
        [ValidateIntValue]
        public int MaxQuantity { get; set; }
    }
}
