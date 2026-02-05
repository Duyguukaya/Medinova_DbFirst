using AutoMapper;
using Medinova.Dtos.TestimonialDtos;
using Medinova.Models;

namespace Medinova.Mappings.TestimonialMappings
{
    public class TestimonialMapping:Profile
    {
        public TestimonialMapping()
        {
            CreateMap<Testimonial, CreateTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, UpdateTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, ResultTestimonialDto>().ReverseMap();
        }
    }
}