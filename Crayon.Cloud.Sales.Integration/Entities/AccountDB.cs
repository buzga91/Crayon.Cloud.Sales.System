namespace Crayon.Cloud.Sales.Integration.Entities
{
    public class AccountDB
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CustomerId { get; set; }
        public CustomerDB Customer { get; set; }
        public ICollection<LicenseDB> Licenses { get; set; } = new List<LicenseDB>();
    }
}