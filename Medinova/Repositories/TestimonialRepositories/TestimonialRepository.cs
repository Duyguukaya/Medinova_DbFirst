using Medinova.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medinova.Repositories.TestimonialRepositories
{
    public class TestimonialRepository : GenericRepository<Testimonial>, ITestimonialRepository
    {
        public TestimonialRepository(MedinovaContext context) : base(context)
        {
        }
    }
}