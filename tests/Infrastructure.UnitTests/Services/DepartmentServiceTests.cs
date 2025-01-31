using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Dto.EmployeeDomain;
using Infrastructure.Interfaces.Services;
using Core.Interfaces.Repositories;
using Core.Models.EmployeeDomain;
using Infrastructure.Models.Pagination;
using Infrastructure.Services;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.UnitTests.Services
{
    [TestClass]
    public class DepartmentServiceTests
    {
        private Mock<IMapper> _mapperMock;        
        private Mock<IDepartmentRepository> _departmentRepositoryMock;
        private DepartmentService _departmentService;

        [TestInitialize]
        public void SetUp()
        {
            _mapperMock = new Mock<IMapper>();            
            _departmentRepositoryMock = new Mock<IDepartmentRepository>();
            _departmentService = new DepartmentService(_mapperMock.Object, _departmentRepositoryMock.Object);
        }       

        [TestMethod]
        public async Task GetDepartmentByIdAsync_ShouldReturnDepartmentDto()
        {
            // Arrange
            var departmentId = 1L;
            var departmentEntity = new DepartmentEntity { Id = departmentId, Name = "HR" };
            var departmentDto = new DepartmentDto { Id = departmentId, Name = "HR" };

            _departmentRepositoryMock.Setup(repo => repo.GetDepartmentByIdAsync(departmentId))
                                     .ReturnsAsync(departmentEntity);
            _mapperMock.Setup(m => m.Map<DepartmentDto>(departmentEntity))
                       .Returns(departmentDto);

            // Act
            var result = await _departmentService.GetDepartmentByIdAsync(departmentId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(departmentDto.Id, result.Id);
            Assert.AreEqual(departmentDto.Name, result.Name);
        }

        [TestMethod]
        public async Task GetDepartmentsAsync_ShouldReturnDepartmentDtos()
        {
            // Arrange
            var departmentEntities = new[]
            {
                new DepartmentEntity { Id = 1, Name = "HR" },
                new DepartmentEntity { Id = 2, Name = "IT" }
            };
            var departmentDtos = new[]
            {
                new DepartmentDto { Id = 1, Name = "HR" },
                new DepartmentDto { Id = 2, Name = "IT" }
            };

            _departmentRepositoryMock.Setup(repo => repo.GetDepartmentsAsync())
                                     .ReturnsAsync(departmentEntities);
			_mapperMock.Setup(m => m.Map<IEnumerable<DepartmentDto>>(It.IsAny<IEnumerable<DepartmentEntity>>()))
					   .Returns(departmentDtos);

			// Act
			var result = await _departmentService.GetDepartmentsAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Length);
            Assert.AreEqual(departmentDtos[0].Id, result[0].Id);
            Assert.AreEqual(departmentDtos[1].Id, result[1].Id);
        }
    }
}
