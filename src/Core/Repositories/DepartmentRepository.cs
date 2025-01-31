using Core.Extensions;
using Core.Interfaces.Repositories;
using Core.Models.EmployeeDomain;
using Microsoft.EntityFrameworkCore;
using PrimeNG.TableFilter;
using PrimeNG.TableFilter.Models;

namespace Core.Repositories
{
	internal class DepartmentRepository : IDepartmentRepository
    {
        private readonly WadaDbContext _context;

        public DepartmentRepository(WadaDbContext context)
        {
            _context = context;
        }              

		public async Task<DepartmentEntity?> GetDepartmentByIdAsync(long id)
		{
			return await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);
		}

		public async Task<DepartmentEntity[]> GetDepartmentsAsync()
		{
            return await _context.Departments.ToArrayAsync();
		}

		public async Task<long> InsertDepartmentAsync(DepartmentEntity entity)
		{
			await _context.Departments.AddAsync(entity);
			await _context.SaveChangesAsync();
			return entity.Id;
		}

		public async Task DeleteDepartmentAsync(long id)
		{
			var department= await GetDepartmentByIdAsync(id);
			if (department != null)
			{
				_context.Departments.Delete(department);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<DepartmentEntity> UpdateDepartmentAsync(DepartmentEntity entity)
		{
			var department = _context.Update(entity);
			await _context.SaveChangesAsync();
			return department.Entity;
		}
	}
}
