using Crayon.Cloud.Sales.Domain;
using Crayon.Cloud.Sales.Integration.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crayon.Cloud.Sales.Integration.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        Task<IEnumerable<Account>> IAccountRepository.GetAccounts(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}
