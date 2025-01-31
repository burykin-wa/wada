using Core.Models.EmployeeDomain;

namespace Core.Interfaces.Repositories
{
    public interface IDepartmentRepository
    {
        Task<DepartmentEntity?> GetDepartmentByIdAsync(long id);
        Task<DepartmentEntity[]> GetDepartmentsAsync();
        Task<long> InsertDepartmentAsync(DepartmentEntity entity);
        Task DeleteDepartmentAsync(long id);
        Task<DepartmentEntity> UpdateDepartmentAsync(DepartmentEntity entity);
	}
}
