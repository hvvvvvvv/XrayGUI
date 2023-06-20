using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProxyController.Modle
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CompareOtherPropertyAttribute : CompareAttribute
    {
        private object _compareValue;
        private ValidationAttribute _validationObj;
        public CompareOtherPropertyAttribute(string propertyName, object value, ValidationAttribute validationObj) : base(propertyName)
        {
            _compareValue = value;
            _validationObj = validationObj;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var res = ValidationResult.Success;
            var baseRes = base.IsValid(_compareValue, validationContext);
            if(baseRes is null ||  baseRes == ValidationResult.Success)
            {
                res = _validationObj.GetValidationResult(value, validationContext);
            }
            return res;
        }

    }
}
