using Crayon.Cloud.Sales.Integration.ContextDB;
using Crayon.Cloud.Sales.Integration.Contracts;
using Crayon.Cloud.Sales.Integration.Entities;
using Crayon.Cloud.Sales.Shared;

namespace Crayon.Cloud.Sales.Integration.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<CustomerDB>> GetCustomerById(int customerId)
        {
            var customer = _context.Customers.FirstOrDefault(x => x.Id == customerId);

            if (customer == null)
            {
                string message = $"There is no customer for specifi id:{customerId}";
                Logger.LogError(message);
                return Result<CustomerDB>.Failure(message);
            }
            return Result<CustomerDB>.Success(customer);
        }
    }
}
