using Crayon.Cloud.Sales.Integration.Entities;
using Crayon.Cloud.Sales.Shared;

namespace Crayon.Cloud.Sales.Integration.Contracts
{
    public interface ICustomerRepository
    {
        Task<Result<CustomerDB>> GetCustomerById(int customerId);
    }
}
