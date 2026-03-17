using Medinova.Enums;
using Medinova.Filters;
using Medinova.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Medinova.Areas.Patient.Controllers
{
    [RoleAuthorize("Patient")]
    public class PatientPanelController : Controller
    {
        private readonly MedinovaContext _context;

        public PatientPanelController()
        {
            _context = new MedinovaContext();
        }

        private User GetCurrentUser()
        {
            string userName = Session["userName"] as string;
            if (string.IsNullOrEmpty(userName)) return null;
            return _context.Users.FirstOrDefault(u => u.UserName == userName);
        }

        public async Task<ActionResult> Index()
        {
            var user = GetCurrentUser();
            if (user == null) return RedirectToAction("Login", "Account", new { area = "" });

            string email = user.UserName; // e-posta = kullanıcı adı varsayımı
            string fullName = (user.FirstName + " " + user.LastName).Trim();

            var appointments = await _context.Appointments
                .Include("Doctor")
                .Where(a => a.Email == email || a.FullName == fullName)
                .OrderByDescending(a => a.AppointmentDate)
                .ToListAsync();

            ViewBag.FullName = fullName;
            ViewBag.TotalCount = appointments.Count;
            ViewBag.ActiveCount = appointments.Count(a => a.IsActive == true);
            ViewBag.CancelledCount = appointments.Count(a => a.IsActive != true);

            return View(appointments);
        }

        public async Task<ActionResult> NewAppointment()
        {
            var departments = await _context.Departments.ToListAsync();
            ViewBag.Departments = new SelectList(departments, "DepartmentId", "Name");

            // İlerleyen 14 gün için tarih listesi (hafta içi)
            var dateList = new List<SelectListItem>();
            for (int d = 1; d <= 14; d++)
            {
                var date = DateTime.Today.AddDays(d);
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    dateList.Add(new SelectListItem
                    {
                        Value = date.ToString("yyyy-MM-dd"),
                        Text = date.ToString("dd MMMM yyyy, dddd")
                    });
                }
            }
            ViewBag.DateList = dateList;
            ViewBag.Times = Times.AppointmentHours;

            var user = GetCurrentUser();
            if (user != null)
            {
                ViewBag.FullName = (user.FirstName + " " + user.LastName).Trim();
                ViewBag.Email = user.UserName;
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> NewAppointment(Appointment model)
        {
            // Çakışma kontrolü: aynı doktor, aynı tarih ve saatte başka randevu var mı?
            bool conflict = await _context.Appointments.AnyAsync(a =>
                a.Doctorld == model.Doctorld &&
                a.AppointmentDate == model.AppointmentDate &&
                a.AppointmentTime == model.AppointmentTime &&
                a.IsActive == true);

            if (conflict)
            {
                ModelState.AddModelError("", "Seçtiğiniz saat dolu. Lütfen farklı bir saat seçiniz.");
            }

            if (ModelState.IsValid)
            {
                model.IsActive = true;
                _context.Appointments.Add(model);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Randevunuz başarıyla oluşturuldu!";
                return RedirectToAction("Index");
            }

            // Hata varsa formu yeniden doldur
            var departments = await _context.Departments.ToListAsync();
            ViewBag.Departments = new SelectList(departments, "DepartmentId", "Name");
            var dateList = new List<SelectListItem>();
            for (int d = 1; d <= 14; d++)
            {
                var date = DateTime.Today.AddDays(d);
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                    dateList.Add(new SelectListItem { Value = date.ToString("yyyy-MM-dd"), Text = date.ToString("dd MMMM yyyy, dddd") });
            }
            ViewBag.DateList = dateList;
            ViewBag.Times = Times.AppointmentHours;
            return View(model);
        }

        // AJAX: Bölüme göre doktorları getir
        public async Task<JsonResult> GetDoctorsByDepartment(int departmentId)
        {
            var doctors = await _context.Doctors
                .Where(d => d.DepartmentId == departmentId)
                .Select(d => new { d.DoctorId, d.FullName })
                .ToListAsync();
            return Json(doctors, JsonRequestBehavior.AllowGet);
        }

        // AJAX: Doktor + tarih için dolu saatleri getir
        public async Task<JsonResult> GetAvailableHours(int doctorId, string date)
        {
            if (!DateTime.TryParse(date, out DateTime parsedDate))
                return Json(Times.AppointmentHours, JsonRequestBehavior.AllowGet);

            var bookedTimes = await _context.Appointments
                .Where(a => a.Doctorld == doctorId && a.AppointmentDate == parsedDate && a.IsActive == true)
                .Select(a => a.AppointmentTime)
                .ToListAsync();

            var available = Times.AppointmentHours
                .Select(t => new { time = t, available = !bookedTimes.Contains(t) })
                .ToList();

            return Json(available, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Cancel(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return HttpNotFound();

            // Sadece gelecekteki randevular iptal edilebilir
            if (appointment.AppointmentDate.HasValue && appointment.AppointmentDate.Value < DateTime.Today)
            {
                TempData["Error"] = "Geçmiş tarihli randevular iptal edilemez.";
                return RedirectToAction("Index");
            }

            // Randevunun bu kullanıcıya ait olduğunu doğrula
            var user = GetCurrentUser();
            if (user != null)
            {
                string fullName = (user.FirstName + " " + user.LastName).Trim();
                if (appointment.Email != user.UserName && appointment.FullName != fullName)
                {
                    TempData["Error"] = "Bu randevuyu iptal etme yetkiniz yok.";
                    return RedirectToAction("Index");
                }
            }

            appointment.IsActive = false;
            await _context.SaveChangesAsync();
            TempData["Success"] = "Randevunuz başarıyla iptal edildi.";
            return RedirectToAction("Index");
        }
    }
}
