using Crayon.Cloud.Sales.Integration.Entities;

namespace Crayon.Cloud.Sales.Integration.Contracts
{
    public interface ICustomerRepository
    {
        Task<CustomerDB> GetCustomerById(int customerId);
    }
}
