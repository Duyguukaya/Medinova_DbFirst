using AutoMapper;
using Medinova.Dtos.DepartmentDtos;
using Medinova.Models;

namespace Medinova.Mappings.DepartmentMappings
{
    public class DepartmentMapping:Profile
    {
        public DepartmentMapping()
        {
            CreateMap<Department, CreateDepartmentDto>().ReverseMap();
            CreateMap<Department,UpdateDepartmentDto>().ReverseMap();
             CreateMap<Department, ResultDepartmentDto>().ReverseMap();
        }
    }
}