using Microsoft.EntityFrameworkCore;

namespace Crayon.Cloud.Sales.Integration.Entities
{
    [Index(nameof(CustomerId), Name = "IDX_CustomerId")]
    public class AccountDB
    {
        public int Id { get; set; }
        public int AccountCcpId { get; set; }
        public string Name { get; set; }
        public int CustomerId { get; set; }
        public CustomerDB Customer { get; set; }
        public ICollection<SubscriptionDB> Subscriptions { get; set; } = new List<SubscriptionDB>();
    }
}