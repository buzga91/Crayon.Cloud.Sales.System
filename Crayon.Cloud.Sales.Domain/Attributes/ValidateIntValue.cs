using System.ComponentModel.DataAnnotations;

namespace Crayon.Cloud.Sales.Domain.Attributes
{
    public class ValidateIntValue : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is int id)
            {
                if (id > Int32.MaxValue) return false;

                return id > 0;
            }
            return false;
        }
    }
}
