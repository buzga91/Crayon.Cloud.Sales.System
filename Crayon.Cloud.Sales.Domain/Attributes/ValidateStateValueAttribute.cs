using Crayon.Cloud.Sales.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Crayon.Cloud.Sales.Domain.Attributes
{
   public class ValidateStateValueAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is string state)
            {
                return Enum.TryParse<licenseState>(state, out licenseState stateOut);
            }
            return false;
        }
    }
}
