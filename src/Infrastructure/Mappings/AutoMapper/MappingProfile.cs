using AutoMapper;
using Core.DTOs.Company;
using Core.DTOs.Employee;
using Core.DTOs.Identity;
using Core.Entities;
using Core.Identity;

namespace Infrastructure.Mappings.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateCompanyDto, Company>()
            .ForMember(x => x.CreatedBy, opt => opt.MapFrom(x => x.Name));
        CreateMap<Company, ListCompanyDto>();
        CreateMap<UpdateCompanyDto, Company>();

        CreateMap<CreateCompanyDto, Employee>();
        CreateMap<Employee, CreateEmployeeDto>().ReverseMap();
        CreateMap<Employee, ListEmployeeDto>().ReverseMap();
        CreateMap<UpdateEmployeeDto, Employee>();

        CreateMap<Employee, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Id, opt => opt.Ignore());


        CreateMap<ApplicationUser, SendEmailServiceRequest>();
    }
}
