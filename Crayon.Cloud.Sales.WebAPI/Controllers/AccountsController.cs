using Crayon.Cloud.Sales.Application.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Crayon.Cloud.Sales.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountsController(AccountService accountService)
        {
            _accountService = accountService;
        }
        // GET: api/<Accounts>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<Accounts>/5
        [HttpGet("{customerId}")]
        public async Task<IActionResult>GetAccountsByCustomerId(int customerId)
        {
            var result = await _accountService.GetAccounts(customerId);
            return Ok(result);
        }
    }
}
