using Medinova.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medinova.Repositories.DoctorRepositories
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(MedinovaContext context) : base(context)
        {
        }
    }
}