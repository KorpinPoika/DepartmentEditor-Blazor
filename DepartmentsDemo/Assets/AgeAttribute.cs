using System.ComponentModel.DataAnnotations;

namespace DepartmentsDemo.Assets;

public class AgeAttribute: ValidationAttribute
{
	protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
	{
		if (value == null) {
			return base.IsValid(value, validationContext);
		}
		
		return CheckAge((DateTime)value);
	}

	private ValidationResult? CheckAge(DateTime value)
	{
		var today = DateTime.Today;

		if (value >= today) {
			return new ValidationResult("Date must be in the past");
		}

		var age = today.Year - value.Year;
		if (age > 150) {
			return new ValidationResult($"The age {age} years is unreal");
		}
		
		return null;
	}
}