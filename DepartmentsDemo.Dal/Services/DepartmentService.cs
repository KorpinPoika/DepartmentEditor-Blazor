using DepartmentsDemo.Dal.Interfaces;
using DepartmentsDemo.Db.Entities;

namespace DepartmentsDemo.Dal.Services;

public class DepartmentService : IDepartmentService
{
	private readonly IEfGenericRepository<Department> _departmentRepository;
	private readonly IEfGenericRepository<Empoyee> _employeeRepository;

	public DepartmentService(IEfGenericRepository<Department> departmentRepository, IEfGenericRepository<Empoyee> employeeRepository)
	{
		_departmentRepository = departmentRepository;
		_employeeRepository = employeeRepository;
	}

	
	public Task<IEnumerable<Department>> GetSubDepartmentsAsync(Department department)
	{
		return _departmentRepository.GetAsync(d => d.ParentDepartmentId == department.Id);
	}

	public async Task<IEnumerable<Department>> GetAsync()
	{
		var departments = await _departmentRepository.GetWithIncludeAsync(d => d.Empoyees);
		
		departments.Where(d => d.ParentDepartmentId != null).ToList().ForEach(
			d => d.ParentDepartment = departments.FirstOrDefault(x => x.Id == d.ParentDepartmentId)	
		);

		return departments;
	}

	public ValueTask<Department?> GetByIdAsync(Guid id) => _departmentRepository.GetByIdAsync(id);

	public async Task<Department> CreateOrUpdateAsync(Department department)
	{
		if (IsNew(department))
		{
			department.Id = Guid.NewGuid();
			_departmentRepository.Create(department);
		}
		else
		{
			_departmentRepository.Update(department);
		}

		await _departmentRepository.SaveChangesAsync().ConfigureAwait(false);

		return department;
	}

	public async Task DeleteAsync(Department department)
	{
		var subDepartments = await GetSubDepartmentsAsync(department);
		var delTasksDep = subDepartments.ToList().Select(DeleteAsync).ToArray();
		await Task.WhenAll(delTasksDep);

		var employees = await _employeeRepository.GetAsync(e => e.DepartmentId == department.Id);
		employees.ToList().ForEach(
			async e => {
				var employee = await _employeeRepository.GetByIdAsync(e.Id);
				if (employee != null) {
					_employeeRepository.Remove(employee);
				}
			}
		);
		await _employeeRepository.SaveChangesAsync();

		var dep = await _departmentRepository.GetByIdAsync(department.Id);
		if (dep != null)
		{
			_departmentRepository.Remove(dep);
			await _departmentRepository.SaveChangesAsync();
		}
	}

	private static bool IsNew(Department department) => department.Id == default;
}