using Core.Extensions;
using Core.Interfaces.Repositories;
using Core.Models.EmployeeDomain;
using Microsoft.EntityFrameworkCore;
using PrimeNG.TableFilter;
using PrimeNG.TableFilter.Models;

namespace Core.Repositories
{
	internal class EmployeeRepository : IEmployeeRepository
    {
        private readonly WadaDbContext _context;

        public EmployeeRepository(WadaDbContext context)
        {
            _context = context;
        }
       
        public async Task<EmployeeEntity?> GetEmployeeByIdAsync(long id)
        {
            return await _context.Employees.Include(_ => _.Supervisor).Include(e => e.Department).FirstOrDefaultAsync(_=>_.Id == id);
        }

		public async Task<EmployeeEntity[]> GetEmployeesAsync()
        {
            return await _context.Employees.Include(_=>_.Supervisor).Include(e => e.Department).ToArrayAsync();
        }

        public async Task<long> InsertEmployeeAsync(EmployeeEntity entity)
        {
            await _context.Employees.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<EmployeeEntity> UpdateEmployeeAsync(EmployeeEntity entity)
        {
            var employee = _context.Update(entity);
            await _context.SaveChangesAsync();
            return employee.Entity;
        }

        public async Task DeleteEmployeeAsync(long id)
        {
            var employee = await GetEmployeeByIdAsync(id);
            if (employee != null)
            {
                _context.Employees.Delete(employee);
                await _context.SaveChangesAsync();
            }
        }

		public async Task<(EmployeeEntity[] employees, int total)> GetEmployeesAsync(Dictionary<string, object> filters, int first, int rows, string sortField, int sortOrder, List<(string field, int order)> multiSortMeta)
		{
			TableFilterModel tableFilterModel = new TableFilterModel
			{
				Filters = filters,
				First = first,
				Rows = rows,
				SortField = sortField,
				SortOrder = sortOrder,
				MultiSortMeta = multiSortMeta?.Select(m => new TableFilterSortMeta { Field = m.field, Order = m.order }).ToList()
			};
			var query = _context.Employees.Include(e => e.Supervisor).Include(e => e.Department).AsQueryable();
			var employees = await query.PrimengTableFilter(tableFilterModel, out var totalRecord).ToArrayAsync();
            return (employees, totalRecord);
		}
	}
}
