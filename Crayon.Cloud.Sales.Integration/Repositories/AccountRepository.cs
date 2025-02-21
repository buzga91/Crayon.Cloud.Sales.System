using Crayon.Cloud.Sales.Integration.ContextDB;
using Crayon.Cloud.Sales.Integration.Contracts;
using Crayon.Cloud.Sales.Integration.Entities;

namespace Crayon.Cloud.Sales.Integration.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AccountDB> GetAccountById(int accountId)
        {
            var account = new AccountDB();
            using (_context)
            {

                account = (from ac in _context.Accounts 
                         join s in _context.Subscriptions on ac.Id equals s.AccountId
                         where ac.Id == accountId

                         select new AccountDB
                         {
                             Customer = ac.Customer,
                             CustomerId = ac.CustomerId,
                             Id = ac.Id,
                             Name = ac.Name,
                             Subscriptions = ac.Subscriptions
                         }).FirstOrDefault();

                if (account is null) Console.WriteLine($"There is no account for specifi id:{accountId}");
            }
            return account;
        }

        public async Task<IEnumerable<AccountDB>> GetAccounts(int customerId)
        {
            var accounts = new List<AccountDB>();
            using (_context)
            {
                accounts = (from ac in _context.Accounts
                            join s in _context.Subscriptions on ac.Id equals s.AccountId
                            where ac.CustomerId == customerId

                            select new AccountDB
                            {
                                Customer = ac.Customer,
                                CustomerId = ac.CustomerId,
                                Id = ac.Id,
                                Name = ac.Name,
                                Subscriptions = ac.Subscriptions
                            }).ToList();
            }
            return accounts;
        }
    }
}
