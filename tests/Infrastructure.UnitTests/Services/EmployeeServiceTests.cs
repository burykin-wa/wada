using AutoMapper;
using Core.Interfaces.Repositories;
using Core.Models.EmployeeDomain;
using Infrastructure.Dto.EmployeeDomain;
using Infrastructure.Services;
using Moq;

namespace Infrastructure.UnitTests.Services
{
    [TestClass]
    public class EmployeeServiceTests
    {
        private Mock<IMapper> _mapperMock;
        private Mock<IEmployeeRepository> _employeeRepositoryMock;
        private EmployeeService _employeeService;

        [TestInitialize]
        public void SetUp()
        {
            _mapperMock = new Mock<IMapper>();
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _employeeService = new EmployeeService(_mapperMock.Object, _employeeRepositoryMock.Object);
        }

        [TestMethod]
        public async Task GetEmployeeByIdAsync_ShouldReturnEmployeeDto()
        {
            // Arrange
            var employeeId = 1L;
            var employeeEntity = new EmployeeEntity { Id = employeeId, FirstName = "John", LastName = "Doe", Position = "Engineer" };
            var employeeDto = new EmployeeDto { Id = employeeId, FirstName = "John", LastName = "Doe", Position = "Engineer" };

            _employeeRepositoryMock.Setup(repo => repo.GetEmployeeByIdAsync(employeeId))
                                   .ReturnsAsync(employeeEntity);
            _mapperMock.Setup(m => m.Map<EmployeeDto>(employeeEntity))
                       .Returns(employeeDto);

            // Act
            var result = await _employeeService.GetEmployeeByIdAsync(employeeId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(employeeDto.Id, result.Id);
            Assert.AreEqual(employeeDto.FirstName, result.FirstName);
            Assert.AreEqual(employeeDto.LastName, result.LastName);
        }

        [TestMethod]
        public async Task GetEmployeesAsync_ShouldReturnEmployeeDtos()
        {
            // Arrange
            var employeeEntities = new[]
            {
                new EmployeeEntity { Id = 1, FirstName = "John", LastName = "Doe", Position = "Engineer" },
                new EmployeeEntity { Id = 2, FirstName = "Jane", LastName = "Smith", Position = "Engineer" }
            };
            var employeeDtos = new[]
            {
                new EmployeeDto { Id = 1, FirstName = "John", LastName = "Doe", Position = "Engineer" },
                new EmployeeDto { Id = 2, FirstName = "Jane", LastName = "Smith", Position = "Engineer" }
            };

            _employeeRepositoryMock.Setup(repo => repo.GetEmployeesAsync())
                .ReturnsAsync(employeeEntities);

			_mapperMock.Setup(m => m.Map<IEnumerable<EmployeeDto>>(It.IsAny<IEnumerable<EmployeeEntity>>()))
					   .Returns(employeeDtos);

			// Act
			var result = await _employeeService.GetEmployeesAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Length);
            Assert.AreEqual(employeeDtos[0].Id, result[0].Id);
            Assert.AreEqual(employeeDtos[1].Id, result[1].Id);
        }

        [TestMethod]
        public async Task GetPaginatedEmployeesAsync_ShouldReturnPaginatedEmployees()
        {
            // Arrange
            var employeeEntities = new[]
            {
                new EmployeeEntity { Id = 1, FirstName = "John", LastName = "Doe", Position = "Engineer" },
                new EmployeeEntity { Id = 2, FirstName = "Jane", LastName = "Smith", Position = "Engineer" }
            };
            var employeeDtos = new[]
            {
                new EmployeeDto { Id = 1, FirstName = "John", LastName = "Doe", Position = "Engineer" },
                new EmployeeDto { Id = 2, FirstName = "Jane", LastName = "Smith", Position = "Engineer" }
            };

            _employeeRepositoryMock.Setup(repo => repo.GetEmployeesAsync())
                                   .ReturnsAsync(employeeEntities);
			_mapperMock.Setup(m => m.Map<IEnumerable<EmployeeDto>>(It.IsAny<IEnumerable<EmployeeEntity>>()))
					   .Returns(employeeDtos);

			// Act
			var result = await _employeeService.GetPaginatedEmployeesAsync("");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Content.Length);
            Assert.AreEqual(employeeDtos[0].Id, result.Content[0].Id);
            Assert.AreEqual(employeeDtos[1].Id, result.Content[1].Id);
        }

        [TestMethod]
        public async Task CreateEmployeeAsync_ShouldReturnEmployeeId()
        {
            // Arrange
            var employeeDto = new EmployeeDto { Id = 1, FirstName = "John", LastName = "Doe", Position = "Engineer" };
            var employeeEntity = new EmployeeEntity { Id = 1, FirstName = "John", LastName = "Doe", Position = "Engineer" };

            _mapperMock.Setup(m => m.Map<EmployeeEntity>(employeeDto))
                       .Returns(employeeEntity);
            _employeeRepositoryMock.Setup(repo => repo.InsertEmployeeAsync(employeeEntity))
                                   .ReturnsAsync(employeeEntity.Id);

            // Act
            var result = await _employeeService.CreateEmployeeAsync(employeeDto);

            // Assert
            Assert.AreEqual(employeeEntity.Id, result);
        }

        [TestMethod]
        public async Task UpdateEmployeeAsync_ShouldUpdateEmployee()
        {
            // Arrange
            var employeeDto = new EmployeeDto { Id = 1, FirstName = "John", LastName = "Doe", Position = "Engineer" };
            var employeeEntity = new EmployeeEntity { Id = 1, FirstName = "John", LastName = "Doe", Position = "Engineer" };

            _employeeRepositoryMock.Setup(repo => repo.GetEmployeeByIdAsync(employeeDto.Id))
                                   .ReturnsAsync(employeeEntity);
            _mapperMock.Setup(m => m.Map(employeeDto, employeeEntity));

            // Act
            await _employeeService.UpdateEmployeeAsync(employeeDto);

            // Assert
            _employeeRepositoryMock.Verify(repo => repo.UpdateEmployeeAsync(employeeEntity), Times.Once);
        }

        [TestMethod]
        public async Task DeleteEmployeeAsync_ShouldDeleteEmployee()
        {
            // Arrange
            var employeeId = 1L;

            // Act
            await _employeeService.DeleteEmployeeAsync(employeeId);

            // Assert
            _employeeRepositoryMock.Verify(repo => repo.DeleteEmployeeAsync(employeeId), Times.Once);
        }

        [TestMethod]
        public async Task GetEmployeesAsync_WithFilters_ShouldReturnFilteredEmployees()
        {
            // Arrange
            var filters = new Dictionary<string, object> { { "FirstName", "John" } };
            var first = 0;
            var rows = 10;
            var sortField = "FirstName";
            var sortOrder = 1;
            var multiSortMeta = new List<(string field, int order)>();

            var employeeEntities = new List<EmployeeEntity>
            {
                new EmployeeEntity { Id = 1, FirstName = "John", LastName = "Doe", Position = "Engineer" }
            };

            _employeeRepositoryMock.Setup(repo => repo.GetEmployeesAsync(filters, first, rows, sortField, sortOrder, multiSortMeta))
                                   .ReturnsAsync((employeeEntities.ToArray(), employeeEntities.Count));
            _mapperMock.Setup(m => m.Map<EmployeeDto[]>(employeeEntities.ToArray()))
                       .Returns(employeeEntities.Select(e => new EmployeeDto { Id = e.Id, FirstName = e.FirstName, LastName = e.LastName, Position = "Engineer" }).ToArray());

            // Act
            var result = await _employeeService.GetEmployeesAsync(filters, first, rows, sortField, sortOrder, multiSortMeta);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.employees.Length);
            Assert.AreEqual(employeeEntities[0].Id, result.employees[0].Id);
        }        
    }
}
