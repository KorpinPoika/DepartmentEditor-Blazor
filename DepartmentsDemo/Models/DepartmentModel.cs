using System.ComponentModel.DataAnnotations;

namespace DepartmentsDemo.Models;

public class DepartmentModel
{
	[Required]
	[StringLength(50, MinimumLength = 2, ErrorMessage = "Length of string should be between 2 and 50 symbols")]
	public string Name { get; set; } = null!;

	[StringLength(10, ErrorMessage = "Length of string should be less or equals 10 symbols")]
	public string? Code { get; set; }
}