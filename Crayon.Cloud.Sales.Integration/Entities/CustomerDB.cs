using Crayon.Cloud.Sales.Domain.Models;

namespace Crayon.Cloud.Sales.Integration.Entities
{
    public class CustomerDB
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<AccountDB> Accounts { get; set; } = new List<AccountDB>();
    }
}
