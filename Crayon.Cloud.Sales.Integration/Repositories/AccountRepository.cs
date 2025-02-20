using Crayon.Cloud.Sales.Integration.Context;
using Crayon.Cloud.Sales.Integration.Contracts;
using Crayon.Cloud.Sales.Integration.Entities;

namespace Crayon.Cloud.Sales.Integration.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext context;

        public AccountRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Task<AccountDB> GetAccountById(int accountId)
        {
            throw new NotImplementedException();
        }

       public Task<IEnumerable<AccountDB>> GetAccounts(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}
