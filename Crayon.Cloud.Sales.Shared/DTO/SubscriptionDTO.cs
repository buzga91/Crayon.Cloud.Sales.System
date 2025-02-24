namespace Crayon.Cloud.Sales.Shared.DTO
{
    public class SubscriptionDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int SoftwareId { get; set; }
        public string SoftwareName { get; set; }
        public int AccountId { get; set; }
        public int Quantity { get; set; }
        public int MinQuantity { get; set; }
        public int MaxQuantity { get; set; }
        public DateTime ValidTo { get; set; }
        public string State { get; set; }
    }
}
