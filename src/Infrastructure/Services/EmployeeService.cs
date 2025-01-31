using AutoMapper;
using Infrastructure.Dto.EmployeeDomain;
using Infrastructure.Interfaces.Services;
using Core.Interfaces.Repositories;
using Core.Models.EmployeeDomain;
using Infrastructure.Models.Pagination;

namespace Infrastructure.Services
{
	public class EmployeeService : IEmployeeService
    {        
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IMapper mapper, IEmployeeRepository employeeRepository)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(long id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDto[]> GetEmployeesAsync()
        {
            var employees = await _employeeRepository.GetEmployeesAsync();
            return _mapper.Map<EmployeeDto[]>(employees);
        }

        public async Task<Page<EmployeeDto>> GetPaginatedEmployeesAsync(string employeeRequestJson)
        {
            var employees = await _employeeRepository.GetEmployeesAsync();
            var employeeDtos = _mapper.Map<EmployeeDto[]>(employees);
            return new Page<EmployeeDto>{ Content = employeeDtos };
        }

        public async Task<long> CreateEmployeeAsync(EmployeeDto employee)
        {
			// You can add validation and other business logic here
			var mappingOptions = new Action<IMappingOperationOptions>(opt => opt.Items["IsNew"] = true);
			var employeeEntity = _mapper.Map<EmployeeEntity>(employee, mappingOptions);
			return await _employeeRepository.InsertEmployeeAsync(employeeEntity);
        }

        public async Task<EmployeeDto> UpdateEmployeeAsync(EmployeeDto employee)
        {
			// You can add validation and other business logic here
			var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(employee.Id);
            _mapper.Map(employee, existingEmployee, opt => opt.Items["IsNew"] = false);
            var employeeEntity = await _employeeRepository.UpdateEmployeeAsync(existingEmployee!);
            
            return _mapper.Map<EmployeeDto>(employeeEntity);
        }

        public async Task DeleteEmployeeAsync(long id)
        {
            await _employeeRepository.DeleteEmployeeAsync(id);
        }

        public async Task<(EmployeeDto[] employees, int total)> GetEmployeesAsync(Dictionary<string, object> filters, int first, int rows, string sortField, int sortOrder, List<(string field, int order)> multiSortMeta)
        {
            var employees = await _employeeRepository.GetEmployeesAsync(filters, first, rows, sortField, sortOrder, multiSortMeta);

            var dtos = _mapper.Map<EmployeeDto[]>(employees.employees);
            return (dtos, employees.total);
        }		
	}
}
