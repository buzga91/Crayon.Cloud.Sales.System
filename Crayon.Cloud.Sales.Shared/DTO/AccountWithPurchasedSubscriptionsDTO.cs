namespace Crayon.Cloud.Sales.Shared.DTO
{
   public class AccountWithPurchasedSubscriptionsDTO
    {
        public int Id { get; set; }
        public int AccountCCpId { get; set; }
        public string Name { get; set; }
        public int CustomerId { get; set; }
        public IEnumerable<SubscriptionDTO> PurchasedSoftwareLicenses { get; set; }
    }
}
