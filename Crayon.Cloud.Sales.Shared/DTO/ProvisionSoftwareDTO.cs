namespace Crayon.Cloud.Sales.Shared.DTO
{
    public class ProvisionSoftwareDTO
    {
        public int Id { get; set; }
        public int CustomerCcpId { get; set; }
        public int Quantity { get; set; }
        public string State { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
