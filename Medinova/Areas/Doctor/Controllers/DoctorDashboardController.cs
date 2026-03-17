using Medinova.Filters;
using Medinova.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Medinova.Areas.Doctor.Controllers
{
    [RoleAuthorize("Doctor")]
    public class DoctorDashboardController : Controller
    {
        private readonly MedinovaContext _context;

        public DoctorDashboardController()
        {
            _context = new MedinovaContext();
        }

        // Oturum açmış doktorun kullanıcı adından Doctor kaydını bul
        private async Task<Models.Doctor> GetCurrentDoctorAsync()
        {
            string userName = Session["userName"] as string;
            if (string.IsNullOrEmpty(userName)) return null;

            // Kullanıcı adıyla eşleşen User'ı bul, ardından aynı isimde Doctor'ı getir
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            if (user == null) return null;

            string fullName = (user.FirstName + " " + user.LastName).Trim();
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.FullName == fullName);
            return doctor;
        }

        public async Task<ActionResult> Index()
        {
            var doctor = await GetCurrentDoctorAsync();
            if (doctor == null)
            {
                ViewBag.Error = "Doktor kaydınız bulunamadı. Lütfen yönetici ile iletişime geçin.";
                ViewBag.TodayCount = 0;
                ViewBag.TotalCount = 0;
                ViewBag.UpcomingCount = 0;
                return View();
            }

            var today = DateTime.Today;
            ViewBag.DoctorName = doctor.FullName;
            ViewBag.DoctorId = doctor.DoctorId;
            ViewBag.TodayCount = await _context.Appointments
                .CountAsync(a => a.Doctorld == doctor.DoctorId && a.AppointmentDate == today && a.IsActive == true);
            ViewBag.TotalCount = await _context.Appointments
                .CountAsync(a => a.Doctorld == doctor.DoctorId);
            ViewBag.UpcomingCount = await _context.Appointments
                .CountAsync(a => a.Doctorld == doctor.DoctorId && a.AppointmentDate >= today && a.IsActive == true);
            return View();
        }

        public async Task<ActionResult> MyAppointments()
        {
            var doctor = await GetCurrentDoctorAsync();
            if (doctor == null)
            {
                ViewBag.Error = "Doktor kaydınız bulunamadı.";
                return View(new System.Collections.Generic.List<Appointment>());
            }

            ViewBag.DoctorName = doctor.FullName;
            var appointments = await _context.Appointments
                .Where(a => a.Doctorld == doctor.DoctorId)
                .OrderByDescending(a => a.AppointmentDate)
                .ToListAsync();

            return View(appointments);
        }

        public async Task<ActionResult> TodayAppointments()
        {
            var doctor = await GetCurrentDoctorAsync();
            if (doctor == null)
            {
                ViewBag.Error = "Doktor kaydınız bulunamadı.";
                return View(new System.Collections.Generic.List<Appointment>());
            }

            var today = DateTime.Today;
            ViewBag.DoctorName = doctor.FullName;
            var appointments = await _context.Appointments
                .Where(a => a.Doctorld == doctor.DoctorId && a.AppointmentDate == today)
                .OrderBy(a => a.AppointmentTime)
                .ToListAsync();

            return View(appointments);
        }
    }
}
