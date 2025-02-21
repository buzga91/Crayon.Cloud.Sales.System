using Crayon.Cloud.Sales.Integration.ContextDB;
using Crayon.Cloud.Sales.Integration.Contracts;
using Crayon.Cloud.Sales.Integration.Entities;

namespace Crayon.Cloud.Sales.Integration.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CustomerDB> GetCustomerById(int customerId)
        {
            var customer = new CustomerDB();
            using (_context)
            {
                customer = (from cu in _context.Customers
                         join ac in _context.Accounts on cu.Id equals ac.CustomerId
                         where cu.Id == customerId
                         select new CustomerDB
                         {
                             Accounts = cu.Accounts,
                             CustomerCcpId = cu.CustomerCcpId,
                             Id = cu.Id,
                             Name = cu.Name
                         }).FirstOrDefault();

                if (customer is null) Console.WriteLine($"There is no customer for specifi id:{customerId}");
            }
            return customer;
        }
    }
}
