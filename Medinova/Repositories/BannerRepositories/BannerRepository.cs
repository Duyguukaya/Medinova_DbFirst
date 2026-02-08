using Medinova.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medinova.Repositories.BannerRepositories
{
    public class BannerRepository : GenericRepository<Banner>, IBannerRepository
    {
        public BannerRepository(MedinovaContext context) : base(context)
        {
        }
    }
}