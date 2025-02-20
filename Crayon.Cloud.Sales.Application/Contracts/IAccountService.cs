using Crayon.Cloud.Sales.Domain.Models;
using Crayon.Cloud.Sales.Shared;

namespace Crayon.Cloud.Sales.Application.Contracts
{
    public interface IAccountService
    {
        Task<Result<IEnumerable<Account>>> GetAccounts(int customerId);
        Task<Result<Account>> GetAccountById(int accountId);
    }
}
