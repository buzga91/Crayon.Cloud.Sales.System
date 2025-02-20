using Crayon.Cloud.Sales.Integration.Entities;

namespace Crayon.Cloud.Sales.Integration.Contracts
{
    public interface IAccountRepository
    {
        Task<IEnumerable<AccountDB>> GetAccounts(int customerId);
        Task<AccountDB> GetAccountById(int accountId);
    }
}
