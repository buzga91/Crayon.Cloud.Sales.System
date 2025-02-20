using Crayon.Cloud.Sales.Application.Contracts;
using Crayon.Cloud.Sales.Domain.Extensions;
using Crayon.Cloud.Sales.Domain.Models;
using Crayon.Cloud.Sales.Integration.Contracts;
using Crayon.Cloud.Sales.Shared;
using Crayon.Cloud.Sales.Shared.DTO;

namespace Crayon.Cloud.Sales.Application.Services
{
    public class LicenseService : ILicenseService
    {
        private readonly ILicenseRepository _licenseRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ISoftwareService _ccpClient;

        public LicenseService(ILicenseRepository licenseRepository, IAccountRepository accountRepository, ISoftwareService ccpClient)
        {
            _licenseRepository = licenseRepository;
            _accountRepository = accountRepository;
            _ccpClient = ccpClient;
        }
        public async Task<Result<IEnumerable<AvailableSoftwareDTO>>> GetAvailableSoftwaresFromCCP()
        {
            return await _ccpClient.GetAvailableSoftwareServices();
        }
        public async Task<Result> CancelLicense(int licenseId)
        {
            var license = await _licenseRepository.GetLicensesById(licenseId);
            if (license != null)
            {
                license.State = licenseState.Canceled.ToString();
                await _licenseRepository.Update(license);
            }
            return Result.Success();
        }

        public async Task<Result> ExtendLicense(int licenseId, DateTime newValidTo)
        {
            var license = await _licenseRepository.GetLicensesById(licenseId);
            if (license != null && license.State == licenseState.Active.ToString())
            {
                license.ValidTo = newValidTo;
                await _licenseRepository.Update(license);
            }
            return Result.Success();
        }

        public async Task<Result<IEnumerable<License>>> GetLicensesForSpecificAccount(int accountId)
        {
            var license = await _licenseRepository.GetLicensesByAccountId(accountId);
            var domainLicense = LicenseExtensions.ToDomainCollection(license);
            return Result<IEnumerable<License>>.Success(domainLicense);
        }

        public async Task<Result<License>> ProvisionLicense(License license)
        {
            var softwareDTO = new PurchasedSoftwareDTO
            {
                Name = license.SoftwareName,
                ValidTo = license.ValidTo,
                State = license.State.ToString(),
                Quantity = license.Quantity
            };
            var result = await _ccpClient.ProvisionNewLicense(softwareDTO);

            if(!result.IsSuccess) return Result<License>.Failure(result.Error);

            var accountEntity = await _accountRepository.GetAccountById(license.AccountId);
            var licenseEntity = LicenseExtensions.ToEntity(license, accountEntity);

            await _licenseRepository.Add(licenseEntity);
            return Result<License>.Success(license);
        }
    }
}
