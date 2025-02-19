using Crayon.Cloud.Sales.Domain;
using Crayon.Cloud.Sales.Integration.Contracts;
using Crayon.Cloud.Sales.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crayon.Cloud.Sales.Application.Services
{
   public class AccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Result<IEnumerable<Account>>> GetAccounts(int customerId)
        {
             var accounts = await _accountRepository.GetAccounts(customerId);
            return Result<IEnumerable<Account>>.Success(accounts);
        }
    }
}
