using Crayon.Cloud.Sales.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Crayon.Cloud.Sales.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private int _customerId;
        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        private void MockAuthentication()
        {
            var customerIdClaim = User.FindFirst("CustomerId");
            if (customerIdClaim == null)
            {
                throw new UnauthorizedAccessException("CustomerId claim not found.");
            }

            if(!int.TryParse(customerIdClaim.Value, out int customerId))
            {
                throw new ArithmeticException("Customer id is not valid value");
            }
            _customerId = customerId;
        }

        [HttpGet("subscriptions")]
        public async Task<IActionResult> GetAccountsWithPurchasedSubscriptions()
        {
            try
            {
                var result = await _accountService.GetAccountsWithSubscriptions();
                return Accepted(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Return All accounts for speficif customer.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAccountsByCustomerId()
        {
            MockAuthentication();
            try
            {
                var result = await _accountService.GetAccounts(_customerId);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
