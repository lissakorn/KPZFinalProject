using System.ComponentModel.DataAnnotations;

namespace PersonalOrganizer.Models
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
            
                if (date.Date < DateTime.Now.Date)
                {
                    return new ValidationResult(ErrorMessage ?? "Дата не може бути в минулому.");
                }
            }

            return ValidationResult.Success;
        }
    }
}