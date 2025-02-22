using Crayon.Cloud.Sales.Application.Contracts;
using Crayon.Cloud.Sales.Integration.Contracts;
using Crayon.Cloud.Sales.Integration.Extensions;
using Crayon.Cloud.Sales.Shared;
using Crayon.Cloud.Sales.Shared.DTO;

namespace Crayon.Cloud.Sales.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Result<AccountDTO>> GetAccountById(int accountId)
        {
            var accountEntitie = await _accountRepository.GetAccountById(accountId);
            if (!accountEntitie.IsSuccess) return Result<AccountDTO>.Failure(accountEntitie.Error);
            var account = AccountExtensions.ToDto(accountEntitie.Value);
            return Result<AccountDTO>.Success(account);
        }

        public async Task<Result<IEnumerable<AccountDTO>>> GetAccounts(int customerId)
        {
            var accountEntities = await _accountRepository.GetAccounts(customerId);
            if (!accountEntities.IsSuccess) return Result<IEnumerable<AccountDTO>>.Failure(accountEntities.Error);
            var accounts = AccountExtensions.ToDtoCollection(accountEntities.Value);
            return Result<IEnumerable<AccountDTO>>.Success(accounts);
        }

        public async Task<Result<IEnumerable<AccountWithPurchasedSubscriptionsDTO>>> GetAccountsWithSubscriptions()
        {
            var accountEntities = await _accountRepository.GetAccountsWithSubscriptions();
            if (!accountEntities.IsSuccess) return Result<IEnumerable<AccountWithPurchasedSubscriptionsDTO>>.Failure(accountEntities.Error);
            var accounts = AccountExtensions.ToDtoCollectionWithPurchasedSubscriptions(accountEntities.Value);
            return Result<IEnumerable<AccountWithPurchasedSubscriptionsDTO>>.Success(accounts);
        }
    }
}
