using Medinova.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Medinova.Repositories.AppointmentRepositories
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(MedinovaContext context) : base(context)
        {
        }

        public async Task<List<Appointment>> GetAppointmentsWithDoctorsAsync()
        {
            return await _context.Appointments
                                 .Include("Doctor")
                                 .ToListAsync();
        }
    }
}