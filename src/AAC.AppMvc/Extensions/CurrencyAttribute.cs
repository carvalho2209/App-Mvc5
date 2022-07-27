using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace AAC.AppMvc.Extensions
{
    public class CurrencyAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var currency = Convert.ToDecimal(value, new CultureInfo("en-US"));
            }
            catch (Exception)
            {
                return new ValidationResult("Currency in invalid format.");
            }

            return ValidationResult.Success;
        }
    }
}