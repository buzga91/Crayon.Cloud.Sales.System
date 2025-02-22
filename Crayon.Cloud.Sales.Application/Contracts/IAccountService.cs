using Crayon.Cloud.Sales.Shared;
using Crayon.Cloud.Sales.Shared.DTO;

namespace Crayon.Cloud.Sales.Application.Contracts
{
    public interface IAccountService
    {
        Task<Result<IEnumerable<AccountDTO>>> GetAccounts(int customerId);
        Task<Result<AccountDTO>> GetAccountById(int accountId);
    }
}
