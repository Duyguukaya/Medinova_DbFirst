using Medinova.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medinova.Repositories.AppointmentRepositories
{
    internal interface IAppointmentRepository: IGenericRepository<Appointment>
    {
        Task<List<Appointment>> GetAppointmentsWithDoctorsAsync();
    }
}
