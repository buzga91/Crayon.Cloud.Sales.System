using Crayon.Cloud.Sales.Integration.ContextDB;
using Crayon.Cloud.Sales.Integration.Contracts;
using Crayon.Cloud.Sales.Integration.Entities;
using Crayon.Cloud.Sales.Shared;

namespace Crayon.Cloud.Sales.Integration.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Add(SubscriptionDB license)
        {
            await _context.Subscriptions.AddAsync(license);
            await _context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> ChangeSubscriptionState(int subscriptionId, string state)
        {
            var subscription = _context.Subscriptions.Where(x => x.Id == subscriptionId)
                .FirstOrDefault();
            if (subscription == null)
            {
                string message = $"Subscription with id:{subscriptionId}  doesn't exist";
                Logger.LogError(message);
                return Result.Failure(message);
            }
            if(subscription.State == state)
            {
                string message = $"Subscription with id:{subscriptionId} is already in provided state. Subscriptin state:{subscription.State}, new provided state:{state}";
                Logger.LogError(message);
                return Result.Failure(message);
            }
            subscription.State = state;
            await _context.SaveChangesAsync();
            return Result.Success();
        }
        public async Task<Result> ExtendSubscriptionValidationTime(int subscriptionId, DateTime validTo)
        {
            var subscription = _context.Subscriptions.Where(x => x.Id == subscriptionId)
                .FirstOrDefault();
            if (subscription == null)
            {
                string message = $"Subscription with id:{subscriptionId}  doesn't exist";
                Logger.LogError(message);
                return Result.Failure(message);
            }
            else if (subscription.State == "Canceled")
            {
                string message = $"You can't extend validation time on canceled subscription.";
                Logger.LogError(message);
                return Result.Failure(message);
            }
            else if (subscription.ValidTo >= validTo)
            {
                string message = $"New valid to date {validTo} must be greater then current valid date time {subscription.ValidTo}";
                Logger.LogError(message);
                return Result.Failure(message);
            }
                subscription.ValidTo = validTo;
            await _context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> ChangeSubscriptionQuantity(int subscriptionId, int newQuantity)
        {
            var subscription = _context.Subscriptions.Where(x => x.Id == subscriptionId)
               .FirstOrDefault();
            if (subscription == null)
            {
                string message = $"Subscription with id:{subscriptionId} doesn't exist";
                Console.WriteLine(message);
                return Result.Failure(message);
            }
            else if (subscription.State == "Canceled")
            {
                string message = $"You can't change license quantity on canceled subscription.";
                Logger.LogError(message);
                return Result.Failure(message);
            }
            else if (newQuantity > subscription.MaxQuantity || newQuantity < subscription.MinQuantity)
            {
                string message = $"New subscription quantity ({newQuantity}) can't be more then ({subscription.MaxQuantity}) and less then  ({subscription.MinQuantity}).";
                Console.WriteLine(message);
                return Result.Failure(message);
            }
            subscription.Quantity = newQuantity;
            await _context.SaveChangesAsync();
            return Result.Success();
        }
    }
}
