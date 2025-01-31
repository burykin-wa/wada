using AutoMapper;
using Core.Interfaces.Repositories;
using Core.Models.EmployeeDomain;
using Infrastructure.Dto.EmployeeDomain;
using Infrastructure.Interfaces.Services;

namespace Infrastructure.Services
{
    public class DepartmentService : IDepartmentService
    {        
        private readonly IMapper _mapper;        
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IMapper mapper, IDepartmentRepository departmentRepository)
        {
            _mapper = mapper;
            _departmentRepository = departmentRepository;
        }

		public async Task<long> CreateDepartmentAsync(DepartmentDto department)
		{
			return await _departmentRepository.InsertDepartmentAsync(_mapper.Map<DepartmentEntity>(department));
		}

		public async Task DeleteDepartmentAsync(long id)
		{
			await _departmentRepository.DeleteDepartmentAsync(id);
		}

		public async Task<DepartmentDto> GetDepartmentByIdAsync(long id)
		{
			var department = await _departmentRepository.GetDepartmentByIdAsync(id);
			return _mapper.Map<DepartmentDto>(department);
		}

		public async Task<DepartmentDto[]> GetDepartmentsAsync()
		{
			var departments = await _departmentRepository.GetDepartmentsAsync();
			return _mapper.Map<DepartmentDto[]>(departments);
		}

		public async Task<DepartmentDto> UpdateDepartmentAsync(DepartmentDto department)
		{
			var existingDepartment = await _departmentRepository.GetDepartmentByIdAsync(department.Id);
			_mapper.Map(department, existingDepartment);
			// You can add validation and other business logic here
			var departmentEntity = await _departmentRepository.UpdateDepartmentAsync(existingDepartment!);

			return _mapper.Map<DepartmentDto>(departmentEntity);
		}
	}
}
