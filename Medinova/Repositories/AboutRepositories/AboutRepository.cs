using Medinova.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medinova.Repositories.AboutRepositories
{
    public class AboutRepository : GenericRepository<About>, IAboutRepository
    {
        public AboutRepository(MedinovaContext context) : base(context)
        {
        }
    }
}