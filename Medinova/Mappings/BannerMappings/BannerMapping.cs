using AutoMapper;
using Medinova.Dtos.BannerDtos;
using Medinova.Models;

namespace Medinova.Mappings.BannerMappings
{
    public class BannerMapping : Profile
    {
        public BannerMapping()
        {
            CreateMap<Banner, CreateBannerDto>().ReverseMap();
            CreateMap<Banner, UpdateBannerDto>().ReverseMap();
            CreateMap<Banner, ResultBannerDto>().ReverseMap();
        }
    }
}