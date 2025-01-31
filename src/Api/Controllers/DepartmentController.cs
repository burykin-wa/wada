using Infrastructure.Dto.EmployeeDomain;
using Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/v1/department")]
    [Produces("application/json")]
	public class DepartmentController: ControllerBase
	{
		private readonly IDepartmentService _departmentService;
		private readonly ILogger<EmployeeController> _logger;
		public DepartmentController(IDepartmentService departmentService, ILogger<EmployeeController> logger)
		{
			_departmentService = departmentService;
			_logger = logger;
		}

		[HttpGet("{departmentId}")]
		[SwaggerOperation(OperationId = "getDepartment", Tags = ["department-controller"])]
		public async Task<IActionResult> GetDepartmentById(long departmentId)
		{
			var department = await _departmentService.GetDepartmentByIdAsync(departmentId);
			if (department == null)
			{
				_logger.LogInformation($"Department with ID {departmentId} not found.");
				return NotFound();
			}
			return Ok(department);
		}

		[HttpGet]
		[SwaggerOperation(OperationId = "getDepartments", Tags = ["department-controller"])]
		public async Task<ActionResult<DepartmentDto[]>> GetDepartments()
		{
			return Ok(await _departmentService.GetDepartmentsAsync());
		}

		[HttpPost]
		[SwaggerOperation(OperationId = "createDepartment", Tags = ["department-controller"])]
		public async Task<IActionResult> CreateDepartment([FromBody] DepartmentCUDto departmentDto)
		{
			var departmentId = await _departmentService.CreateDepartmentAsync(departmentDto);
			_logger.LogInformation($"Department with ID {departmentId} created.");
			departmentDto.Id = departmentId;
			return CreatedAtAction(nameof(GetDepartmentById), new { departmentId }, departmentDto);
		}

		[HttpPut("{departmentId}")]
		[SwaggerOperation(OperationId = "updateDepartment", Tags = ["department-controller"])]
		public async Task<ActionResult<DepartmentDto>> UpdateDepartment(long departmentId, [FromBody] DepartmentCUDto departmentDto)
		{
			var existingDepartment = await _departmentService.GetDepartmentByIdAsync(departmentId);
			if (existingDepartment == null)
			{
				_logger.LogInformation($"Department with ID {departmentId} not found.");
				return NotFound();
			}

			departmentDto.Id = departmentId;
			return await _departmentService.UpdateDepartmentAsync(departmentDto);
		}

		[HttpDelete("{departmentId}")]
		[SwaggerOperation(OperationId = "deleteDepartment", Tags = ["department-controller"])]
		public async Task<IActionResult> DeleteDepartment(long departmentId)
		{
			var departmentToRemove = await _departmentService.GetDepartmentByIdAsync(departmentId);
			if (departmentToRemove == null)
			{
				_logger.LogInformation($"Department with ID {departmentId} not found.");
				return NotFound();
			}

			await _departmentService.DeleteDepartmentAsync(departmentId);
			_logger.LogInformation($"Department with ID {departmentId} deleted.");

			return NoContent();
		}
	}
}
