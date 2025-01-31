using Infrastructure.Dto.EmployeeDomain;
using Infrastructure.Models.Pagination;

namespace Infrastructure.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<Page<EmployeeDto>> GetPaginatedEmployeesAsync(string employeeRequestJson);
        Task<EmployeeDto[]> GetEmployeesAsync();
        Task<(EmployeeDto[] employees, int total)> GetEmployeesAsync(Dictionary<string, object> filters, int first, int rows, string sortField, int sortOrder, List<(string field, int order)> multiSortMeta);
		Task<EmployeeDto> GetEmployeeByIdAsync(long id);
        Task<long> CreateEmployeeAsync(EmployeeDto employee);
        Task<EmployeeDto> UpdateEmployeeAsync(EmployeeDto employee);
        Task DeleteEmployeeAsync(long id);
	}
}
