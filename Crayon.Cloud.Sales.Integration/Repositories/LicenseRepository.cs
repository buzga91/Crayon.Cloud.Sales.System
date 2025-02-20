using Crayon.Cloud.Sales.Integration.Context;
using Crayon.Cloud.Sales.Integration.Contracts;
using Crayon.Cloud.Sales.Integration.Entities;

namespace Crayon.Cloud.Sales.Integration.Repositories
{
    class LicenseRepository : ILicenseRepository
    {
        private readonly ApplicationDbContext context;

        public LicenseRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Task<LicenseDB> Add(LicenseDB license)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LicenseDB>> GetLicensesByAccountId(int accountId)
        {
            throw new NotImplementedException();
        }

        public Task<LicenseDB> GetLicensesById(int licenseId)
        {
            throw new NotImplementedException();
        }

        public Task Update(LicenseDB license)
        {
            throw new NotImplementedException();
        }
    }
}
