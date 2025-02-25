using Crayon.Cloud.Sales.Application.Contracts;
using Crayon.Cloud.Sales.Domain.Attributes;
using Crayon.Cloud.Sales.Integration.Extensions;
using Crayon.Cloud.Sales.Integration.SwaggerRequestExamples;
using Crayon.Cloud.Sales.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

using logger = Crayon.Cloud.Sales.Shared.Logger;
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

        /// <summary>
        /// Get available softwares from thir-party CCP.
        /// </summary>
        /// <returns></returns>
        [HttpGet("softwares")]
        public async Task<IActionResult> GetAvailableSoftwares()
        {
            try
            {
                logger.LogInfo("Getting available softwares from CCP");
                var result = await _SubscriptionService.GetAvailableSoftwaresFromCCP();
                logger.LogInfo("Proccess succeed");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Cancel existing subscription
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <returns></returns>
        [HttpPatch("cancel/{subscriptionId}")]
        [RoutParameterValidation]
        public async Task<IActionResult> CancelSubscription(int subscriptionId)
        {
            try
            {
                logger.LogInfo($"Canceling subscription with the id:{subscriptionId}.");
                var result = await _SubscriptionService.CancelSubscription(subscriptionId);
                logger.LogInfo("Proccess succeed");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Extend subscription valid to date time.
        /// </summary>
        /// <param name="extendSubscriptionValidDate"></param>
        /// <returns></returns>
        [HttpPatch("extend")]
        [SwaggerRequestExample(typeof(ExtendSubscriptionValidDateDTO), typeof(ExtendSubscriptionValidDateDTOExample))]
        public async Task<IActionResult> ExtendSubscriptionValidDate([FromBody] ExtendSubscriptionValidDateDTO extendSubscriptionValidDate)
        {
            try
            {
                logger.LogInfo($"Extending subscription (id:{extendSubscriptionValidDate.SubscriptionId}) valid date to");
                var result = await _SubscriptionService.ExtendSubscriptionValidDate(extendSubscriptionValidDate.SubscriptionId, extendSubscriptionValidDate.NewValidTo);
                logger.LogInfo("Proccess succeed");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Change subscription license quantity.
        /// </summary>
        /// <param name="changeSubscriptionQuantityDTO"></param>
        /// <returns></returns>
        [HttpPatch("changeLicense")]
        [SwaggerRequestExample(typeof(ChangeSubscriptionQuantityDTO), typeof(ChangeSubscriptionQuantityDTOExample))]
        public async Task<IActionResult> ChangeSubscriptionQuantity([FromBody] ChangeSubscriptionQuantityDTO changeSubscriptionQuantityDTO)
        {
            try
            {
                logger.LogInfo($"Changing subscription (id:{changeSubscriptionQuantityDTO.SubscriptionId})  license quantity");
                var result = await _SubscriptionService.ChangeSubscriptionQuantity(changeSubscriptionQuantityDTO.SubscriptionId, changeSubscriptionQuantityDTO.NewQuantity);
                logger.LogInfo("Proccess succeed");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Provision new subscription. Provisioning is done on third-party CCP, and in the Crayon system.
        /// </summary>
        /// <param name="provisionSubscription"></param>
        /// <returns></returns>
        [HttpPost("provision")]
        [SwaggerRequestExample(typeof(ProvisionSubscriptionDTO), typeof(ProvisionSubscriptionDTOExample))]
        public async Task<IActionResult> ProvisionSubscription([FromBody] ProvisionSubscriptionDTO provisionSubscription)
        {
            try
            {
                logger.LogInfo($"Provisioning new subscription");
                var domainLicense = SubscriptionExtensions.ToDomain(provisionSubscription);
                var result = await _SubscriptionService.ProvisionSubscription(domainLicense);
                logger.LogInfo("Proccess succeed");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
