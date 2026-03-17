using Medinova.Filters;
using Medinova.Models;
using Medinova.Repositories;
using Medinova.Repositories.AppointmentRepositories;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Medinova.Areas.Admin.Controllers
{
    [RoleAuthorize("Admin")]
    public class AdminAppointmentController : Controller
    {
        private readonly MedinovaContext _context;
        private readonly AppointmentRepository _appointmentRepo;
        private readonly IGenericRepository<Medinova.Models.Doctor> _doctorRepo;

        public AdminAppointmentController()
        {
            _context = new MedinovaContext();
            _appointmentRepo = new AppointmentRepository(_context);
            _doctorRepo = new GenericRepository<Medinova.Models.Doctor>(_context);
        }

        public async Task<ActionResult> Index()
        {
            var appointments = await _appointmentRepo.GetAppointmentsWithDoctorsAsync();
            return View(appointments);
        }

        public async Task<ActionResult> Details(int id)
        {
            var appointment = await _context.Appointments.Include("Doctor").FirstOrDefaultAsync(x => x.Appointmentid == id);
            if (appointment == null) return HttpNotFound();
            return View(appointment);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return HttpNotFound();
            var doctors = await _doctorRepo.GetAllAsync();
            ViewBag.Doctors = new SelectList(doctors, "DoctorId", "FullName", appointment.Doctorld);
            ViewBag.Times = Medinova.Enums.Times.AppointmentHours;
            return View(appointment);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Appointment model)
        {
            if (ModelState.IsValid)
            {
                await _appointmentRepo.UpdateAsync(model);
                return RedirectToAction("Index");
            }
            var doctors = await _doctorRepo.GetAllAsync();
            ViewBag.Doctors = new SelectList(doctors, "DoctorId", "FullName", model.Doctorld);
            ViewBag.Times = Medinova.Enums.Times.AppointmentHours;
            return View(model);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var appointment = await _context.Appointments.Include("Doctor").FirstOrDefaultAsync(x => x.Appointmentid == id);
            if (appointment == null) return HttpNotFound();
            return View(appointment);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _appointmentRepo.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> ToggleStatus(int id)
        {
            var appointment = await _appointmentRepo.GetByIdAsync(id);
            if (appointment == null) return HttpNotFound();
            appointment.IsActive = !appointment.IsActive;
            await _appointmentRepo.UpdateAsync(appointment);
            return RedirectToAction("Index");
        }
    }
}
