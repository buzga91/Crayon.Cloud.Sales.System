using Crayon.Cloud.Sales.Application.Contracts;
using Crayon.Cloud.Sales.Domain.Extensions;
using Crayon.Cloud.Sales.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Crayon.Cloud.Sales.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LicenseController : ControllerBase
    {
        private readonly ILicenseService _licenseService;

        public LicenseController(ILicenseService licenseService)
        {
            _licenseService = licenseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableSoftwares()
        {
            var result = await _licenseService.GetAvailableSoftwaresFromCCP();
            return Ok(result);
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetLicenseByAccountId(int accountId)
        {
            var result = await _licenseService.GetLicensesForSpecificAccount(accountId);
            return Ok(result);
        }

       
        [HttpPatch("{licenseId}")]
        public async Task<IActionResult> CancelLicense(int licenseId)
        {
            var result = await _licenseService.CancelLicense(licenseId);
            return Ok(result);
        }

       
        [HttpPatch]
        public async Task<IActionResult> ExtendLicenseValidDate([FromBody] ExtendLicenseValidDateDTO extendLicenseValidDate)
        {
            var result = await _licenseService.ExtendLicense(extendLicenseValidDate.LicenseId, extendLicenseValidDate.NewValidTo);
            return Ok(result);
        }

        // PUT api/<License>/5
        [HttpPost]
        public async Task<IActionResult> ProvisionLicense([FromBody] ProvisionLicenseDTO provisionLicense)
        {
            var domainLicense = LicenseExtensions.ToDomain(provisionLicense);
            var result = await _licenseService.ProvisionLicense(domainLicense);
            return Ok(result);
        }

        // DELETE api/<License>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
