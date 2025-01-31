using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Models.EmployeeDomain
{
	public class DepartmentEntity : BaseEntity
	{
		public required string Name { get; set; }
		public string? Description { get; set; }
	}

	internal class DepartmentConfiguration : BaseEntityConfiguration<DepartmentEntity>
	{
		public override void Configure(EntityTypeBuilder<DepartmentEntity> builder)
		{
			base.Configure(builder);

			builder.HasData(GetTestDepartments());
		}

		private static IEnumerable<DepartmentEntity> GetTestDepartments()
		{
			yield return new DepartmentEntity { Id = 1, Name = "HR", Description = "Human Resources" };
			yield return new DepartmentEntity { Id = 2, Name = "IT", Description = "Information Technology" };
		}
	}
}
