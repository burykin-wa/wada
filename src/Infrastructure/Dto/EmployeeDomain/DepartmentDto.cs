using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dto.EmployeeDomain
{
	public class DepartmentDto
	{
        public virtual long Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
	}

	public class DepartmentCUDto : DepartmentDto
	{
		[Obsolete]
		public override long Id { get => base.Id; set => base.Id = value; }
	}
}
