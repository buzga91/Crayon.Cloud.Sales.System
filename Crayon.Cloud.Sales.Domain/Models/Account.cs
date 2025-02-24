namespace Crayon.Cloud.Sales.Domain.Models
{
    public class Account
    {
        public int Id{ get; set; }
        public int AccountCcpId { get; set; }
        public string Name { get; set; }
        public int CustomerId { get; set; }
        public IEnumerable<Subscription> PurchasedSoftwareLicenses { get; set; }
    }
}
