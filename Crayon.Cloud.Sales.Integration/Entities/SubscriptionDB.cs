namespace Crayon.Cloud.Sales.Integration.Entities
{
    public class SubscriptionDB
    {
        public int Id { get; set; }
        public int SoftwareId { get; set; }
        public string SoftwareName { get; set; } 
        public int Quantity { get; set; }
        public int MinQuantity { get; set; }
        public int MaxQuantity { get; set; }
        public string State { get; set; } 
        public DateTime ValidTo { get; set; } 
        public int AccountId { get; set; }
        public AccountDB Account { get; set; }
    }
}
