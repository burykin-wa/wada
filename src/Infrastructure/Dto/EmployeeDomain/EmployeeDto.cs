using Microsoft.Extensions.FileSystemGlobbing.Internal;

namespace Infrastructure.Dto.EmployeeDomain
{
    public class EmployeeDto
    {
        public virtual long Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Position { get; set; }

        public string? About { get; set; }
        public bool FullTime {  get; set; }
        public long HireDate { get; set; }

        public virtual DepartmentDto? Department { get; set; }
        public virtual EmployeeDto? Supervisor { get; set; }

        [Obsolete]
        public virtual long? SupervisorId { get; set; }

        [Obsolete]
        public virtual long? DepartmentId { get; set; }

        public virtual long? CreatedAt { get; set; }                
    }

	public class EmployeeCUDto : EmployeeDto  //11.11.2024 for schema naming
	{
        [Obsolete]
		public override long Id { get => base.Id; set => base.Id = value; }

		[Obsolete]
		public override long? CreatedAt { get => base.CreatedAt; set => base.CreatedAt = value; }

        [Obsolete]
		public override DepartmentDto? Department { get => base.Department; set => base.Department = value; }

		public override long? DepartmentId { get => base.DepartmentId; set => base.DepartmentId = value; }

		[Obsolete]
		public override EmployeeDto? Supervisor { get => base.Supervisor; set => base.Supervisor = value; }

		public override long? SupervisorId { get => base.SupervisorId; set => base.SupervisorId = value; }
	}
}
