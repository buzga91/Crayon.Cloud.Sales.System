using Crayon.Cloud.Sales.Integration.Contracts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Crayon.Cloud.Sales.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoftwareServicesController : ControllerBase
    {
        private readonly ICCPClient _ccpClient;

        public SoftwareServicesController(ICCPClient ccpClient)
        {
            _ccpClient = ccpClient;
        }
        // GET: api/<SoftwareServicesController>
        [HttpGet]
        public async Task<IActionResult> GetAvailableSoftwares()
        {
            var result = await _ccpClient.GetAvailableSoftwareServices();
            return Ok(result);
        }
    }
}
