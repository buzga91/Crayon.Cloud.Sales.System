using Crayon.Cloud.Sales.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crayon.Cloud.Sales.Integration.Contracts
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAccounts(int customerId);
    }
}
