using Crayon.Cloud.Sales.Integration.ContextDB;
using Crayon.Cloud.Sales.Integration.Contracts;
using Crayon.Cloud.Sales.Integration.Entities;
using Crayon.Cloud.Sales.Shared;
using Crayon.Cloud.Sales.Shared.DTO;

namespace Crayon.Cloud.Sales.Integration.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<AccountDB>> GetAccountById(int accountId)
        {
            var account = _context.Accounts.FirstOrDefault(x => x.Id == accountId);

            if (account == null)
            {
                string message = $"There is no account for specifi id:{accountId}";
                Console.WriteLine(message);
                return Result<AccountDB>.Failure(message);
            }
            return Result<AccountDB>.Success(account);
        }

        public async Task<Result<IEnumerable<AccountDB>>> GetAccounts(int customerId)
        {
            var accounts = _context.Accounts.Where(x => x.CustomerId == customerId).ToList();

            if (accounts == null)
            {
                string message = $"There is no account for specifi id:{customerId}";
                Console.WriteLine(message);
                return Result<IEnumerable<AccountDB>>.Failure(message);
            }
            return Result<IEnumerable<AccountDB>>.Success(accounts);
        }

        public async Task<Result<IEnumerable<AccountDB>>> GetAccountsWithSubscriptions()
        {
            var accounts = (from ac in _context.Accounts
                            join c in _context.Customers on ac.CustomerId equals c.Id
                            where _context.Subscriptions.Any(s => s.AccountId == ac.Id)
                            select new AccountDB
                            {
                                Id = ac.Id,
                                Name = ac.Name,
                                CustomerId = ac.CustomerId,
                                Subscriptions = (from s in _context.Subscriptions
                                                 where s.AccountId == ac.Id
                                                 select new SubscriptionDB
                                                 {
                                                     Id = s.Id,
                                                     SoftwareName = s.SoftwareName,
                                                     Quantity = s.Quantity,
                                                     ValidTo = s.ValidTo,
                                                     State = s.State,
                                                     Account = s.Account,
                                                     AccountId = s.AccountId,
                                                     MaxQuantity = s.MaxQuantity,
                                                     MinQuantity = s.MinQuantity,
                                                     SoftwareId = s.SoftwareId,
                                                 }).ToList()
                            }).ToList();
            if (accounts == null)
            {
                string message = $"There is no accounts with purchased";
                Console.WriteLine(message);
                return Result<IEnumerable<AccountDB>>.Failure(message);
            }
            return Result<IEnumerable<AccountDB>>.Success(accounts);
        }
    }
}
