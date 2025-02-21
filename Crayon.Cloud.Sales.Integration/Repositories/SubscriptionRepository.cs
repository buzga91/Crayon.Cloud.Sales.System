using Crayon.Cloud.Sales.Integration.ContextDB;
using Crayon.Cloud.Sales.Integration.Contracts;
using Crayon.Cloud.Sales.Integration.Entities;

namespace Crayon.Cloud.Sales.Integration.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(SubscriptionDB license)
        {
            using (_context)
            {
                await _context.Subscriptions.AddAsync(license);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<SubscriptionDB>> GetSubscriptionsByAccountId(int accountId)
        {
            var subscriptions = new List<SubscriptionDB>();
            using (_context)
            {
                subscriptions = (from s in _context.Subscriptions
                         join ac in _context.Accounts on s.AccountId equals ac.Id
                         where s.AccountId == accountId
                         select new SubscriptionDB
                         {
                             Account = s.Account,
                             Id =s.Id,
                             AccountId = s.AccountId,
                             MaxQuantity = s.MaxQuantity,
                             MinQuantity = s.MinQuantity,
                             Quantity = s.Quantity,
                             SoftwareId = s.SoftwareId,
                             SoftwareName = s.SoftwareName,
                             State = s.State,
                             ValidTo = s.ValidTo
                         }).ToList();
                if (subscriptions is null) Console.WriteLine($"There is not subrscriptions for specific account Id:{accountId}");
            }
            return subscriptions;
        }

        public async Task<SubscriptionDB> GetSubscriptionById(int subscriptionId)
        {
            var subscription = new SubscriptionDB();
            using (_context)
            {
                subscription = (from s in _context.Subscriptions
                                 join ac in _context.Accounts on s.AccountId equals ac.Id
                                 where s.Id == subscriptionId
                                select new SubscriptionDB
                                 {
                                     Account = s.Account,
                                     Id = s.Id,
                                     AccountId = s.AccountId,
                                     MaxQuantity = s.MaxQuantity,
                                     MinQuantity = s.MinQuantity,
                                     Quantity = s.Quantity,
                                     SoftwareId = s.SoftwareId,
                                     SoftwareName = s.SoftwareName,
                                     State = s.State,
                                     ValidTo = s.ValidTo
                                 }).FirstOrDefault();

                if (subscription is null) Console.WriteLine($"There is not subscription for specific subscription Id:{subscriptionId}");
            }
            return subscription;
        }

        public async Task ChangeSubscriptionState(int subscriptionId, string state)
        {
            var subscription = new SubscriptionDB();
            using (_context)
            {
                subscription = _context.Subscriptions.Where(x => x.Id == subscriptionId)
                   .FirstOrDefault();
                if(subscription is null)
                {
                    Console.WriteLine($"Subscription with {subscriptionId} id doesn't exist");
                }
                subscription.State = state;
               await  _context.SaveChangesAsync();
            }
        }
        public async Task ExtendSubscriptionValidationTime(int subscriptionId, DateTime validTo)
        {
            var subscription = new SubscriptionDB();
            using (_context)
            {
                subscription = _context.Subscriptions.Where(x => x.Id == subscriptionId)
                   .FirstOrDefault();
                if (subscription is null)
                {
                    Console.WriteLine($"Subscription with {subscriptionId} id doesn't exist");
                }
                subscription.ValidTo = validTo;
               await _context.SaveChangesAsync();
            }
        }

        public async Task ChangeSubscriptionQuantity(int subscriptionId, int newQuantity)
        {
            var subscription = new SubscriptionDB();
            using (_context)
            {
                subscription = _context.Subscriptions.Where(x => x.Id == subscriptionId)
                   .FirstOrDefault();
                if (subscription is null)
                {
                    Console.WriteLine($"Subscription with {subscriptionId} id doesn't exist");
                }
                else if(newQuantity > subscription.MaxQuantity || newQuantity < subscription.MinQuantity)
                {
                    Console.WriteLine($"New subscription quantity ({newQuantity}) can't be more then ({subscription.MaxQuantity}) and less then  ({subscription.MinQuantity}).");
                }
                    subscription.Quantity = newQuantity;
                await _context.SaveChangesAsync();
            }
        }
    }
}
