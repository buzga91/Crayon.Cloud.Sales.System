using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Crayon.Cloud.Sales.Domain.Attributes
{
    public class RoutParameterValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.TryGetValue("customerId", out var customerIdObj) &&
                customerIdObj is int customerId && (customerId <= 0 || customerId > Int32.MaxValue))
            {
                context.Result = new BadRequestObjectResult($"Customer ID must be a positive integer, not greater than {Int32.MaxValue}");
            }

            else if (context.ActionArguments.TryGetValue("accountId", out var accountIdObj) &&
                accountIdObj is int accountId && (accountId <= 0 || accountId > Int32.MaxValue))
            {
                context.Result = new BadRequestObjectResult($"Account ID must be a positive integer, not greater than {Int32.MaxValue}");
            }

            else if (context.ActionArguments.TryGetValue("subscriptionId", out var subscriptionIdObj) &&
                subscriptionIdObj is int subscriptionId && (subscriptionId <= 0 || subscriptionId > Int32.MaxValue))
            {
                context.Result = new BadRequestObjectResult($"Subscription ID must be a positive integer, not greater than {Int32.MaxValue}");
            }
        }
    }
}
