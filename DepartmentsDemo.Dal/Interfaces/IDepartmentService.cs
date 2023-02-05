using DepartmentsDemo.Db.Entities;

namespace DepartmentsDemo.Dal.Interfaces;

public interface IDepartmentService
{
	Task<IEnumerable<Department>> GetSubDepartmentsAsync(Department department);

	Task<IEnumerable<Department>> GetAsync();
	ValueTask<Department?> GetByIdAsync(Guid id);
	
	Task<Department> CreateOrUpdateAsync(Department department);
	Task DeleteAsync(Department department);
}