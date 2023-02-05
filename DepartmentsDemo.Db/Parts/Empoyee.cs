using System.ComponentModel.DataAnnotations.Schema;

namespace DepartmentsDemo.Db.Entities;

public partial class Empoyee
{
	[NotMapped]
	public int Age => (int) ((DateTime.Now - DateOfBirth).TotalDays/365.242199); 
	
	
	[NotMapped]
	public string FullName => $"{SurName} {FirstName} {Patronymic}";
	
}