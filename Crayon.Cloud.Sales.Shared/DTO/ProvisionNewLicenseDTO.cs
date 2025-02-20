namespace Crayon.Cloud.Sales.Shared.DTO
{
    public class ProvisionLicenseDTO
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int Quantity { get; set; }
        public string SoftwareName { get; set; }
        public DateTime ValidTo { get; set; }
        public string State { get; set; }
    }
}
