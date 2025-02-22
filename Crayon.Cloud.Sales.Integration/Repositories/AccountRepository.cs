using Crayon.Cloud.Sales.Integration.ContextDB;
using Crayon.Cloud.Sales.Integration.Contracts;
using Crayon.Cloud.Sales.Integration.Entities;
using Crayon.Cloud.Sales.Shared;

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
            var accounts = (from ac in _context.Accounts
                        join c in _context.Customers on ac.CustomerId equals c.Id
                        where ac.CustomerId == customerId
                        select new AccountDB
                        {
                            Id = ac.Id,
                            Name = ac.Name,
                            CustomerId = ac.CustomerId,
                            Customer = c,
                            Subscriptions = _context.Subscriptions
                                .Where(s => s.AccountId == ac.Id)
                                .ToList()
                        }).ToList();

            if (accounts == null)
            {
                string message = $"There is no account for specifi id:{customerId}";
                Console.WriteLine(message);
                return Result<IEnumerable<AccountDB>>.Failure(message);
            }
            return Result<IEnumerable<AccountDB>>.Success(accounts);
        }
    }
}
