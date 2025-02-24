using Crayon.Cloud.Sales.Domain.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Crayon.Cloud.Sales.Shared.DTO
{
   public class AccountDTO
    {
        [Required(ErrorMessage = "Account Id is required")]
        [ValidateIntValue]
        public int Id { get; set; }
        [Required(ErrorMessage = "AccountCsp Id is required")]
        [ValidateIntValue]
        public int AccountCspId { get; set; }

        [Required(ErrorMessage = "Account Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Customer Id is required")]
        [ValidateIntValue]
        public int CustomerId { get; set; }
    }
}
