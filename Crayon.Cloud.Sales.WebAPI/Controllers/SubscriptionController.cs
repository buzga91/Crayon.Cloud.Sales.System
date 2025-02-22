using Crayon.Cloud.Sales.Application.Contracts;
using Crayon.Cloud.Sales.Domain.Attributes;
using Crayon.Cloud.Sales.Domain.Extensions;
using Crayon.Cloud.Sales.Integration.SwaggerRequestExamples;
using Crayon.Cloud.Sales.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
namespace Crayon.Cloud.Sales.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _SubscriptionService;

        public SubscriptionController(ISubscriptionService SubscriptionService)
        {
            _SubscriptionService = SubscriptionService;
        }

        [HttpGet("get-available-softwares")]
        public async Task<IActionResult> GetAvailableSoftwares()
        {
            try
            {
                var result = await _SubscriptionService.GetAvailableSoftwaresFromCCP();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("get-subscriptions-by-{accountId}")]
        [RoutParameterValidation]
        public async Task<IActionResult> GetSubscriptionsByAccountId(int accountId)
        {
            try
            {
                var result = await _SubscriptionService.GetSubscriptionsForSpecificAccount(accountId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPatch("cancel-subscription-{subscriptionId}")]
        [RoutParameterValidation]
        public async Task<IActionResult> CancelSubscription(int subscriptionId)
        {
            try
            {
                var result = await _SubscriptionService.CancelSubscription(subscriptionId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPatch("extend-valid-date")]
        [SwaggerRequestExample(typeof(ExtendSubscriptionValidDateDTO), typeof(ExtendSubscriptionValidDateDTOExample))]
        public async Task<IActionResult> ExtendSubscriptionValidDate([FromBody] ExtendSubscriptionValidDateDTO extendSubscriptionValidDate)
        {
            try
            {
                var result = await _SubscriptionService.ExtendSubscriptionValidDate(extendSubscriptionValidDate.SubscriptionId, extendSubscriptionValidDate.NewValidTo);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPatch("change-license-quantity")]
        [SwaggerRequestExample(typeof(ChangeSubscriptionQuantityDTO), typeof(ChangeSubscriptionQuantityDTOExample))]
        public async Task<IActionResult> ChangeSubscriptionQuantity([FromBody] ChangeSubscriptionQuantityDTO changeSubscriptionQuantityDTO)
        {
            try
            {
                var result = await _SubscriptionService.ChangeSubscriptionQuantity(changeSubscriptionQuantityDTO.SubscriptionId, changeSubscriptionQuantityDTO.NewQuantity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("provision-subscription")]
        [SwaggerRequestExample(typeof(ProvisionSubscriptionDTO), typeof(ProvisionSubscriptionDTOExample))]
        public async Task<IActionResult> ProvisionSubscription([FromBody] ProvisionSubscriptionDTO provisionSubscription)
        {
            try
            {
                var domainLicense = SubscriptionExtensions.ToDomain(provisionSubscription);
                var result = await _SubscriptionService.ProvisionSubscription(domainLicense);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
