using AutoMapper;
using Medinova.Dtos.AboutDtos;
using Medinova.Models;

namespace Medinova.Mappings.AboutMappings
{
    public class AboutMapping : Profile
    {
        public AboutMapping()
        {
            CreateMap<About,CreateAboutDto>().ReverseMap();
             CreateMap<About, UpdateAboutDto>().ReverseMap();
             CreateMap<About, ResultAboutDto>().ReverseMap();
        }
    }
}