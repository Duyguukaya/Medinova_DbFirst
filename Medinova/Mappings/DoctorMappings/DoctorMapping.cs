using AutoMapper;
using Medinova.Dtos.DoctorDtos;
using Medinova.Models;

namespace Medinova.Mappings.DoctorMappings
{
    public class DoctorMapping:Profile
    {
        public DoctorMapping()
        {
            CreateMap<Doctor,CreateDoctorDto>().ReverseMap();
            CreateMap<Doctor, UpdateDoctorDto>().ReverseMap();
            CreateMap<Doctor, ResultDoctorDto>().ReverseMap();
        }
    }
}