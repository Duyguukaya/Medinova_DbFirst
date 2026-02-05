using AutoMapper;
using Medinova.Dtos.AboutItemDtos;
using Medinova.Models;

namespace Medinova.Mappings.AboutItemMappings
{
    public class AboutItemMapping:Profile
    {
        public AboutItemMapping()
        {
            CreateMap<AboutItem,CreateAboutItemDto>().ReverseMap();
             CreateMap<AboutItem, UpdateAboutItemDto>().ReverseMap();
              CreateMap<AboutItem, ResultAboutItemDto>().ReverseMap();
        }
    }
}