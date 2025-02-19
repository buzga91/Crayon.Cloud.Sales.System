using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crayon.Cloud.Sales.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public License License { get; set; }
        public SoftwareService SoftwareService { get; set; }
        public DateTime PurchasedDateTime { get; set; }
        public Account Account { get; set; }
        public Customer Customer { get; set; }
    }
}
