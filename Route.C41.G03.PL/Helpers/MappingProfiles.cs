using AutoMapper;
using Route.C41.G03.DAL.Models;
using Route.C41.G03.PL.ViewModels;

namespace Route.C41.G03.PL.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();

            CreateMap<DepartmentViewModel, Department>().ReverseMap();
            //CreateMap<DepartmentViewModel, Department>()
            //.ForMember(dest => dest.Employees, opt => opt.MapFrom(src => src.Employees))
            //.ReverseMap()
            //.ForMember(dest => dest.Employees, opt => opt.MapFrom(src => src.Employees));
        }
    }
}
