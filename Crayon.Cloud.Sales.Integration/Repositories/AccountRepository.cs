using Crayon.Cloud.Sales.Integration.ContextDB;
using Crayon.Cloud.Sales.Integration.Contracts;
using Crayon.Cloud.Sales.Integration.Entities;
using Crayon.Cloud.Sales.Shared;
using Microsoft.EntityFrameworkCore;

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
                Logger.LogError(message);
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
                Logger.LogError(message);
                return Result<IEnumerable<AccountDB>>.Failure(message);
            }
            return Result<IEnumerable<AccountDB>>.Success(accounts);
        }

        public async Task<Result<IEnumerable<AccountDB>>> GetAccountsWithSubscriptions()
        {
            var accounts = await _context.Accounts
      .Include(a => a.Customer)
      .Include(a => a.Subscriptions)
      .Where(a => a.Subscriptions.Any())
      .ToListAsync();

            if (accounts == null)
            {
                string message = $"There is no accounts with purchased";
                Logger.LogError(message);
                return Result<IEnumerable<AccountDB>>.Failure(message);
            }
            return Result<IEnumerable<AccountDB>>.Success(accounts);
        }
    }
}
