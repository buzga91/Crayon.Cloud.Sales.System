namespace Crayon.Cloud.Sales.Shared.DTO
{
    public class ProvisionSubscriptionDTO
    {
        public int SoftwareId { get; set; }
        public string SoftwareName { get; set; }
        public int AccountId { get; set; }
        public int Quantity { get; set; }
        public DateTime ValidTo { get; set; }
        public string State { get; set; }
    }
}
