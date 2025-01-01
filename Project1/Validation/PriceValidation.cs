using System.ComponentModel.DataAnnotations;

namespace Project1.Validation
{
    public class PriceValidation :ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                double doubleValue;
                if (double.TryParse(value.ToString(), out doubleValue))
                {
                    if (doubleValue > 0)
                    {
                        return ValidationResult.Success;
                    }
                }
            }

            return new ValidationResult(ErrorMessage ?? "The value must be greater than zero.");
        }

    }
}
