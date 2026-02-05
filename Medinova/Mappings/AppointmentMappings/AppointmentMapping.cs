using AutoMapper;
using Medinova.Dtos.AppointmentDtos;
using Medinova.Models;

namespace Medinova.Mappings.AppointmentMappings
{
    public class AppointmentMapping : Profile
    {
        public AppointmentMapping()
        {
            CreateMap<Appointment, CreateAppointmentDto>().ReverseMap();
            CreateMap<Appointment, UpdateAppointmentDto>().ReverseMap();
            CreateMap<Appointment, ResultAppointmentDto>().ReverseMap();

        }
    }
}