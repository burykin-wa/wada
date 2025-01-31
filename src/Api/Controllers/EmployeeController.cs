using Infrastructure.Dto.EmployeeDomain;
using Infrastructure.Interfaces.Services;
using Infrastructure.Models.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimeNG.TableFilter.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
    [Authorize]
	[ApiController]
	[Route("api/v1/employee")]
    [Produces("application/json")]
	public class EmployeeController : ControllerBase
	{
		private readonly IEmployeeService _employeeService;
		private readonly ILogger<EmployeeController> _logger;
		public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
		{
			_employeeService = employeeService;
			_logger = logger;
		}

		[HttpGet]
		[SwaggerOperation(OperationId = "getEmployees", Tags = ["employee-controller"])]
		public async Task<ActionResult<EmployeeDto[]>> GetEmployees()
		{
			return Ok(await _employeeService.GetEmployeesAsync());
		}

		[HttpPost("grid")]
		[SwaggerOperation(OperationId = "getEmployeesPrime", Tags = ["employee-controller"])]
		public async Task<PageEmployeeDto> GetEmployees([FromBody] TableFilterModel tableFilterPayload)
		{
			var employees = await _employeeService.GetEmployeesAsync(tableFilterPayload.Filters, tableFilterPayload.First, tableFilterPayload.Rows, tableFilterPayload.SortField, tableFilterPayload.SortOrder, tableFilterPayload.MultiSortMeta?.Select(m => (m.Field, m.Order)).ToList());
			var totalEmployees = employees.Item2;

			var result = new PageEmployeeDto
            {
				Content = employees.Item1.ToArray(),
				First = false,
				Last = false,
				Size = employees.Item1.Length,
				Number = totalEmployees,
				TotalElements = totalEmployees,
				Pageable = new Infrastructure.Models.Pagination.PageableObject
				{
					PageNumber = tableFilterPayload.First,
					PageSize = tableFilterPayload.Rows
				},
				Empty = totalEmployees == 0,
				NumberOfElements = totalEmployees,
				TotalPages = totalEmployees / tableFilterPayload.Rows
			};
			return result;
		}

		[HttpGet("{employeeId}")]
		[SwaggerOperation(OperationId = "getEmployee", Tags = ["employee-controller"])]
		public async Task<ActionResult<EmployeeDto>> GetEmployeeById(long employeeId)
		{
			var employee = await _employeeService.GetEmployeeByIdAsync(employeeId);
			if (employee == null)
			{
				_logger.LogInformation($"Employee with ID {employeeId} not found.");
				return NotFound();
			}
			return Ok(employee);
		}

		[HttpPost]
		[SwaggerOperation(OperationId = "createEmployee", Tags = ["employee-controller"])]
		public async Task<IActionResult> CreateEmployee([FromBody] EmployeeCUDto employeeDto)
		{
			var employeeId = await _employeeService.CreateEmployeeAsync(employeeDto);
			_logger.LogInformation($"Employee with ID {employeeId} created.");
			employeeDto.Id = employeeId;
			return CreatedAtAction(nameof(GetEmployeeById), new { employeeId }, employeeDto);
		}

		[HttpPut("{employeeId}")]
		[SwaggerOperation(OperationId = "updateEmployee", Tags = ["employee-controller"])]
		public async Task<ActionResult<EmployeeDto>> UpdateEmployee(long employeeId, [FromBody] EmployeeCUDto employeeDto)
        {
			var existingEmployee = await _employeeService.GetEmployeeByIdAsync(employeeId);
            if (existingEmployee == null)
            {
                _logger.LogInformation($"Employee with ID {employeeId} not found.");
                return NotFound();
            }

			employeeDto.Id = employeeId;
            return await _employeeService.UpdateEmployeeAsync(employeeDto);
		}

		[HttpDelete("{employeeId}")]
		[SwaggerOperation(OperationId = "deleteEmployee", Tags = ["employee-controller"])]
		public async Task<IActionResult> DeleteEmployee(long employeeId)
		{
            var employeeToRemove = await _employeeService.GetEmployeeByIdAsync(employeeId);
            if (employeeToRemove == null)
            {
                _logger.LogInformation($"Employee with ID {employeeId} not found.");
                return NotFound();
            }

            await _employeeService.DeleteEmployeeAsync(employeeId);
            _logger.LogInformation($"Employee with ID {employeeId} deleted.");            
			
			return NoContent();
		}
	}
}
