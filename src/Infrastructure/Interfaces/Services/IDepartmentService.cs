using Infrastructure.Dto.EmployeeDomain;

namespace Infrastructure.Interfaces.Services
{
    public interface IDepartmentService
    {
        Task<DepartmentDto> GetDepartmentByIdAsync(long id);
        Task<DepartmentDto[]> GetDepartmentsAsync();
		Task<long> CreateDepartmentAsync(DepartmentDto employee);
		Task<DepartmentDto> UpdateDepartmentAsync(DepartmentDto employee);
		Task DeleteDepartmentAsync(long id);
	}
}
