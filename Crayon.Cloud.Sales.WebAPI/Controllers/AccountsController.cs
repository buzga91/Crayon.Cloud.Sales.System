using Crayon.Cloud.Sales.Application.Contracts;
using Crayon.Cloud.Sales.Domain.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Crayon.Cloud.Sales.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("get-acoutns-by-{customerId}")]
        public async Task<IActionResult> GetAccountsByCustomerId(int customerId)
        {
            try
            {
                var result = await _accountService.GetAccounts(customerId);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
