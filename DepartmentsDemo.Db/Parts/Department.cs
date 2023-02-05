using System.ComponentModel.DataAnnotations.Schema;

namespace DepartmentsDemo.Db.Entities;

public partial class Department
{
	[NotMapped] 
	public int NumberOfEmployees => Empoyees.Count;
	
	[NotMapped]
	public string FullName => $"{Name} [{Code ?? "No code"}]";
}