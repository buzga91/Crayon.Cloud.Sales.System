namespace Crayon.Cloud.Sales.Domain.Models
{

    public enum CustomerType
    {
        None = -1,
        Direct,
        Reseller
    }
    public class Customer
    {
        public int CustomerId { get; set; }
        public int CustomerCcpId { get; set; }
        public CustomerType CustomerType { get; set; }
        public string Name { get; set; }
        public IEnumerable<Account> Accounts { get; set; }
    }
}
