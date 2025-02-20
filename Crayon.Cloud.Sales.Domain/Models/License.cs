namespace Crayon.Cloud.Sales.Domain.Models
{
    public enum licenseState
    {
        None = -1,
        Active,
        Canceled
    }
    public class License
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int Quantity { get; set; }
        public string SoftwareName{ get; set; }
        public DateTime ValidTo { get; set; }
        public licenseState State { get; set; }
    }
}
