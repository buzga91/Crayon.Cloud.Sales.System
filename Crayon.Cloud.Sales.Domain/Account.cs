using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crayon.Cloud.Sales.Domain
{
    public class Account
    {
        [Required(ErrorMessage = "Account Id is required")]
        public int Id{ get; set; }
        [Required(ErrorMessage = "Account Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "CustomerId is required")]
        public int CustomerId { get; set; }
        public IEnumerable<License> PurchasedSoftwareLicenses { get; set; }
    }
}
