namespace Crayon.Cloud.Sales.Shared.DTO
{
    public class AvailableSoftwareDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int MinQuantity { get; set; }
        public int MaxQuantity { get; set; }
    }
}
