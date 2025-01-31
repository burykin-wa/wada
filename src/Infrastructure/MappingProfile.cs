using AutoMapper;
using Core.Models.EmployeeDomain;
using Infrastructure.Dto.EmployeeDomain;

namespace Api
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<EmployeeEntity, EmployeeDto>()
				.ForMember(dest => dest.HireDate, opt => opt.MapFrom(src =>
					(src.HireDate.ToDateTime(TimeOnly.MinValue) - DateTime.UnixEpoch).TotalMilliseconds))
				.ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src =>
					(src.CreatedAt - DateTime.UnixEpoch).TotalMilliseconds));

			CreateMap<EmployeeDto, EmployeeEntity>()
				.ForMember(x => x.HireDate, opt => opt.MapFrom(src =>
					DateOnly.FromDateTime(DateTime.UnixEpoch.AddMilliseconds(src.HireDate))))
				.ForMember(x => x.CreatedAt, opt => opt.Ignore())
				.ForMember(x => x.UpdatedAt, opt => opt.Ignore())
				.AfterMap((src, dest, context) =>
				{
					if (context.Items.TryGetValue("IsNew", out var isNew) && (bool)isNew)
					{
						dest.CreatedAt = DateTime.UtcNow;
						dest.UpdatedAt = default;
					}
					else
					{
						dest.UpdatedAt = DateTime.UtcNow;
					}
				});

			CreateMap<EmployeeCUDto, EmployeeEntity>()
				.IncludeBase<EmployeeDto, EmployeeEntity>();

			CreateMap<DepartmentDto, DepartmentEntity>();
			CreateMap<DepartmentEntity, DepartmentDto>();
		}
	}
}
