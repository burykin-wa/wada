using Core.Models.EmployeeDomain;

namespace Core.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        Task<EmployeeEntity[]> GetEmployeesAsync();
        Task<(EmployeeEntity[] employees, int total)> GetEmployeesAsync(Dictionary<string, object> filters, int first, int rows, string sortField, int sortOrder, List<(string field, int order)> multiSortMeta);
        Task<EmployeeEntity?> GetEmployeeByIdAsync(long id);
        Task<long> InsertEmployeeAsync(EmployeeEntity entity);
        Task<EmployeeEntity> UpdateEmployeeAsync(EmployeeEntity entity);        
        Task DeleteEmployeeAsync(long id);
	}
}
