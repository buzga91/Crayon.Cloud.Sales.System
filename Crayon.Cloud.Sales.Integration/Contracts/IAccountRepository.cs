using Crayon.Cloud.Sales.Integration.Entities;
using Crayon.Cloud.Sales.Shared;

namespace Crayon.Cloud.Sales.Integration.Contracts
{
    public interface IAccountRepository
    {
        Task<Result<IEnumerable<AccountDB>>> GetAccounts(int customerId);
        Task<Result<AccountDB>> GetAccountById(int accountId);
    }
}
