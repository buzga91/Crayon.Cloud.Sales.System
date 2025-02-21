using Crayon.Cloud.Sales.Integration.Entities;
using Crayon.Cloud.Sales.Shared;

namespace Crayon.Cloud.Sales.Integration.Contracts
{
    public interface ISubscriptionRepository
    {
        Task<Result> Add(SubscriptionDB license);
        Task<Result<IEnumerable<SubscriptionDB>>> GetSubscriptionsByAccountId(int accountId);
        Task<Result<SubscriptionDB>> GetSubscriptionById(int licenseId);
        Task<Result> ChangeSubscriptionState(int subscriptionId, string state);
        Task<Result> ExtendSubscriptionValidationTime(int subscriptionId, DateTime validTo);
        Task<Result> ChangeSubscriptionQuantity(int subscriptionId, int newQuantity);
    }
}
