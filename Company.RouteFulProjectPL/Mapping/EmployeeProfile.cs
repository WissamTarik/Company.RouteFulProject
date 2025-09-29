using AutoMapper;
using Company.RouteFullProject.DAL.Models;
using Company.RouteFulProject.PL.Dtos;

namespace Company.RouteFulProject.PL.Mapping
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeDto, Employee>().ReverseMap()
                .ForMember(d=>d.Name,o=>o.MapFrom(o=>o.Name));
        }
    }
}
