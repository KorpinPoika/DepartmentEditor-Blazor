using System.ComponentModel.DataAnnotations;
using DepartmentsDemo.Assets;

namespace DepartmentsDemo.Models;

public class EmployeeModel
{
	[Required]
	[StringLength(50, MinimumLength = 2, ErrorMessage = "Length of string should be between 2 and 50 symbols")]
	public string FirstName { get; set; } = null!;

	[Required]
	[StringLength(50, MinimumLength = 2, ErrorMessage = "Length of string should be between 2 and 50 symbols")]
	public string SurName { get; set; } = null!;

	[StringLength(50, MinimumLength = 2, ErrorMessage = "Length of string should be between 2 and 50 symbols")]
	public string? Patronymic { get; set; }

	[Required]
	[Age]
	public DateTime DateOfBirth { get; set; }

	[StringLength(4, ErrorMessage = "Length of string should be 4 symbols")]
	public string? DocSeries { get; set; }

	[StringLength(6, ErrorMessage = "Length of string should be 6 symbols")]
	public string? DocNumber { get; set; }

	[Required]
	[StringLength(50, MinimumLength = 2, ErrorMessage = "Length of string should be between 2 and 50 symbols")]
	public string Position { get; set; } = null!;
	
}