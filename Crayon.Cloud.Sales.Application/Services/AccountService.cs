using Crayon.Cloud.Sales.Application.Contracts;
using Crayon.Cloud.Sales.Domain.Models;
using Crayon.Cloud.Sales.Integration.Contracts;
using Crayon.Cloud.Sales.Integration.Extensions;
using Crayon.Cloud.Sales.Shared;

namespace Crayon.Cloud.Sales.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Result<Account>> GetAccountById(int accountId)
        {
            var accountEntitie = await _accountRepository.GetAccountById(accountId);
            var account = AccountExtensions.ToDomain(accountEntitie);
            return Result<Account>.Success(account);
        }

        public async Task<Result<IEnumerable<Account>>> GetAccounts(int customerId)
        {
            var accountEntities = await _accountRepository.GetAccounts(customerId);
            var accounts = AccountExtensions.ToDomainCollection(accountEntities);
            return Result<IEnumerable<Account>>.Success(accounts);
        }
    }
}
