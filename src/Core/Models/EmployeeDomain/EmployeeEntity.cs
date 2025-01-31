using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Models.EmployeeDomain
{
	public class EmployeeEntity : BaseEntity
	{
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public required string Position { get; set; }
		public string? About { get; set; }
		public bool FullTime { get; set; }
		public DateOnly HireDate { get; set; }
		public DepartmentEntity? Department { get; set; }
		public long? DepartmentId { get; set; }
		public EmployeeEntity? Supervisor { get; set; }
		public long? SupervisorId { get; set; }
	}

	internal class EmployeeConfiguration : BaseEntityConfiguration<EmployeeEntity>
	{
		public override void Configure(EntityTypeBuilder<EmployeeEntity> builder)
		{
			base.Configure(builder);

			builder
				.HasOne(b => b.Supervisor)
				.WithMany()
				.OnDelete(DeleteBehavior.SetNull);

			builder
				.HasOne(b => b.Department)
				.WithMany()
				.OnDelete(DeleteBehavior.SetNull);

			builder.HasData(GetTestEmployees());
		}

		private static IEnumerable<EmployeeEntity> GetTestEmployees()
		{
			yield return new EmployeeEntity
			{
				Id = 1,
				FirstName = "John",
				LastName = "Doe",
				Position = "Manager",
				FullTime = true,
				HireDate = new DateOnly(2015, 1, 10),
				DepartmentId = 1
			};

			yield return new EmployeeEntity
			{
				Id = 2,
				FirstName = "Jane",
				LastName = "Smith",
				Position = "Senior Manager",
				FullTime = true,
				HireDate = new DateOnly(2012, 3, 15),
				DepartmentId = 2
			};

			yield return new EmployeeEntity
			{
				Id = 3,
				FirstName = "Alice",
				LastName = "Johnson",
				Position = "HR Specialist",
				FullTime = true,
				HireDate = new DateOnly(2018, 5, 20),
				DepartmentId = 1,
				SupervisorId = 1
			};
			yield return new EmployeeEntity
			{
				Id = 4,
				FirstName = "Bob",
				LastName = "Brown",
				Position = "IT Specialist",
				FullTime = true,
				HireDate = new DateOnly(2017, 6, 10),
				DepartmentId = 2,
				SupervisorId = 2
			};
			yield return new EmployeeEntity
			{
				Id = 5,
				FirstName = "Charlie",
				LastName = "Davis",
				Position = "HR Assistant",
				FullTime = false,
				HireDate = new DateOnly(2019, 7, 22),
				DepartmentId = 1,
				SupervisorId = 1
			};
			yield return new EmployeeEntity
			{
				Id = 6,
				FirstName = "Diana",
				LastName = "Miller",
				Position = "Software Developer",
				FullTime = true,
				HireDate = new DateOnly(2016, 9, 15),
				DepartmentId = 2,
				SupervisorId = 2
			};
			yield return new EmployeeEntity
			{
				Id = 7,
				FirstName = "Ethan",
				LastName = "Wilson",
				Position = "Network Administrator",
				FullTime = true,
				HireDate = new DateOnly(2020, 8, 1),
				DepartmentId = 2,
				SupervisorId = 2
			};
			yield return new EmployeeEntity
			{
				Id = 8,
				FirstName = "Fiona",
				LastName = "Moore",
				Position = "IT Support",
				FullTime = false,
				HireDate = new DateOnly(2021, 11, 11),
				DepartmentId = 2,
				SupervisorId = 2
			};
			yield return new EmployeeEntity
			{
				Id = 9,
				FirstName = "George",
				LastName = "Taylor",
				Position = "HR Coordinator",
				FullTime = true,
				HireDate = new DateOnly(2018, 12, 5),
				DepartmentId = 1,
				SupervisorId = 1
			};
			yield return new EmployeeEntity
			{
				Id = 10,
				FirstName = "Hannah",
				LastName = "Anderson",
				Position = "IT Manager",
				FullTime = true,
				HireDate = new DateOnly(2014, 2, 25),
				DepartmentId = 2,
				SupervisorId = 2
			};
			yield return new EmployeeEntity
			{
				Id = 11,
				FirstName = "Ian",
				LastName = "Thomas",
				Position = "Database Administrator",
				FullTime = true,
				HireDate = new DateOnly(2013, 4, 17),
				DepartmentId = 2,
				SupervisorId = 2
			};
			yield return new EmployeeEntity
			{
				Id = 12,
				FirstName = "Jack",
				LastName = "White",
				Position = "HR Intern",
				FullTime = false,
				HireDate = new DateOnly(2022, 5, 19),
				DepartmentId = 1,
				SupervisorId = 1
			};
			yield return new EmployeeEntity
			{
				Id = 13,
				FirstName = "Karen",
				LastName = "Hall",
				Position = "System Analyst",
				FullTime = true,
				HireDate = new DateOnly(2019, 10, 30),
				CreatedAt = DateTime.UtcNow,
				DepartmentId = 2,
				SupervisorId = 2
			};
		}
	}
}
