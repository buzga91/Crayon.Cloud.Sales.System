using System.ComponentModel.DataAnnotations;

namespace Crayon.Cloud.Sales.Domain.Models
{
    public class Account
    {
        [Required(ErrorMessage = "Account Id is required")]
        public int Id{ get; set; }
        [Required(ErrorMessage = "Account Id is required")]
        public int AccountCcpId { get; set; }
        [Required(ErrorMessage = "Account Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "CustomerId is required")]
        public int CustomerId { get; set; }
        public IEnumerable<Subscription> PurchasedSoftwareLicenses { get; set; }
    }
}
