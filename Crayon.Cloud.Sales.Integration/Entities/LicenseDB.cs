namespace Crayon.Cloud.Sales.Integration.Entities
{
    public class LicenseDB
    {
        public int Id { get; set; } 
        public string SoftwareName { get; set; } 
        public int Quantity { get; set; } 
        public string State { get; set; } 
        public DateTime ValidTo { get; set; } 
        public int AccountId { get; set; }
        public AccountDB Account { get; set; }
    }
}
