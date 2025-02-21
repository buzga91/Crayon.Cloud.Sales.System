using Crayon.Cloud.Sales.Integration.Entities;

namespace Crayon.Cloud.Sales.Integration.Contracts
{
    public interface ISubscriptionRepository
    {
        Task Add(SubscriptionDB license);
        Task<IEnumerable<SubscriptionDB>> GetSubscriptionsByAccountId(int accountId);
        Task<SubscriptionDB> GetSubscriptionById(int licenseId);
        Task ChangeSubscriptionState(int subscriptionId, string state);
        Task ExtendSubscriptionValidationTime(int subscriptionId, DateTime validTo);
        Task ChangeSubscriptionQuantity(int subscriptionId, int newQuantity);
    }
}
