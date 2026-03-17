using Medinova.Dtos;
using Medinova.Enums;
using Medinova.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Medinova.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        MedinovaContext context = new MedinovaContext();

        public ActionResult Index()
        {
            return View();
        }

        // --- PARTIAL ACTIONS ---

        [ChildActionOnly]
        public PartialViewResult _Hero()
        {
            var banner = context.Banners.FirstOrDefault();
            return PartialView(banner);
        }

        [ChildActionOnly]
        public PartialViewResult _About()
        {
            var about = context.Abouts.FirstOrDefault();
            var aboutItems = context.AboutItems.ToList();
            ViewBag.AboutItems = aboutItems;
            return PartialView(about);
        }

        [ChildActionOnly]
        public PartialViewResult _Doctors()
        {
            var doctors = context.Doctors.Include("Department").ToList();
            return PartialView(doctors);
        }

        [ChildActionOnly]
        public PartialViewResult _Testimonials()
        {
            var testimonials = context.Testimonials
                .Where(t => t.IsApproved == true)
                .ToList();
            return PartialView(testimonials);
        }

        [ChildActionOnly]
        public PartialViewResult DefaultAppointment()
        {
            var departments = context.Departments.ToList();
            ViewBag.departments = (from department in departments
                                   select new SelectListItem
                                   {
                                       Text = department.Name,
                                       Value = department.DepartmentId.ToString()
                                   }).ToList();

            var dateList = new List<SelectListItem>();
            for (int i = 1; i <= 14; i++)
            {
                var date = DateTime.Now.AddDays(i);
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    dateList.Add(new SelectListItem
                    {
                        Text = date.ToString("dd MMMM dddd"),
                        Value = date.ToString("yyyy-MM-dd")
                    });
                }
            }
            ViewBag.dateList = dateList;
            return PartialView();
        }

        [HttpPost]
        public ActionResult MakeAppointment(Appointment appointment)
        {
            appointment.IsActive = true;
            context.Appointments.Add(appointment);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult GetDoctorsByDepartmentId(int departmentId)
        {
            var doctors = context.Doctors
                .Where(x => x.DepartmentId == departmentId)
                .Select(doctor => new SelectListItem
                {
                    Text = doctor.FullName,
                    Value = doctor.DoctorId.ToString()
                }).ToList();
            return Json(doctors, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetAvailableHours(DateTime selectedDate, int doctorId)
        {
            var dayStart = selectedDate.Date;
            var dayEnd = dayStart.AddDays(1);

            var bookedTimes = context.Appointments
                .Where(x => x.Doctorld == doctorId
                            && x.AppointmentDate >= dayStart
                            && x.AppointmentDate < dayEnd)
                .Select(x => x.AppointmentTime)
                .ToList();

            var dtoList = new List<AppointmentAvailability>();
            foreach (var hour in Times.AppointmentHours)
            {
                dtoList.Add(new AppointmentAvailability
                {
                    Time = hour,
                    IsBooked = bookedTimes.Any(t => t.Trim() == hour.Trim())
                });
            }
            return Json(dtoList, JsonRequestBehavior.AllowGet);
        }
    }
}