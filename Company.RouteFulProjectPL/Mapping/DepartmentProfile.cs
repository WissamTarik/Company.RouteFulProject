using AutoMapper;
using Company.RouteFullProject.DAL.Models;
using Company.RouteFullProject.PL.Dtos;

namespace Company.RouteFulProject.PL.Mapping
{
    public class DepartmentProfile:Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, CreateDepartmentDto>().ReverseMap();
        }
    }
}
