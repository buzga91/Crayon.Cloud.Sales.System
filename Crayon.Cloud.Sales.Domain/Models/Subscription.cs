namespace Crayon.Cloud.Sales.Domain.Models
{
    public enum licenseState
    {
        None = -1,
        Active,
        Canceled
    }
    public class Subscription
    {
        public int Id { get; set; }
        public int SoftwareId { get; set; }
        public string SoftwareName { get; set; }
        public int AccountId { get; set; }
        public int Quantity { get; set; }
        public int MinQuantity { get; set; }
        public int MaxQuantity { get; set; }
        public DateTime ValidTo { get; set; }
        public licenseState State { get; set; }
    }
}
