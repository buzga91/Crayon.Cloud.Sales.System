namespace Crayon.Cloud.Sales.Shared.DTO
{
    public class PurchasedSoftwareDTO
    {
        public int Quantity { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
