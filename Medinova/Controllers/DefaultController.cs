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
        public PartialViewResult _AISearch()
        {
            return PartialView();
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

        // ─── AI DOKTOR ONERISI ───────────────────────────────────────────────
        [HttpPost]
        public async Task<JsonResult> GetAIRecommendation(string symptom)
        {
            if (string.IsNullOrWhiteSpace(symptom))
                return Json(new { success = false, message = "Lutfen bir belirti veya hastalik girin." });

            try
            {
                var depts = context.Departments.Select(d => d.Name).ToList();
                var docs = context.Doctors.Include("Department")
                    .Select(d => new { d.FullName, Dept = d.Department.Name })
                    .ToList();

                string deptList = string.Join(", ", depts);
                string docList = string.Join("; ", docs.Select(d => d.FullName + " (" + d.Dept + ")"));

                var lines = new System.Text.StringBuilder();
                lines.AppendLine("Bir hasta su belirtileri bildirdi: " + symptom);
                lines.AppendLine();
                lines.AppendLine("Hastanemizin bolumleri: " + deptList);
                lines.AppendLine("Doktorlarimiz: " + docList);
                lines.AppendLine();
                lines.AppendLine("Lutfen SADECE asagidaki JSON formatinda yanit ver:");
                lines.AppendLine("{");
                lines.AppendLine("  \"tavsiye\": \"2-3 cumlelik saglik tavsiyesi\",");
                lines.AppendLine("  \"bolum\": \"En uygun bolum (yukaridaki listeden)\",");
                lines.AppendLine("  \"doktor\": \"En uygun doktor (yukaridaki listeden)\",");
                lines.AppendLine("  \"aciklama\": \"Neden bu oneriyi yaptigini 1-2 cumle\"");
                lines.AppendLine("}");
                string prompt = lines.ToString();

                string apiKey = "sk-proj-BURAYA_API_KEYINI_YAZ";

                using (var client = new System.Net.Http.HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiKey);

                    var bodyObj = new
                    {
                        model = "gpt-3.5-turbo",
                        messages = new[]
                        {
                            new { role = "system", content = "Sen bir hastane asistanisin. Yalnizca JSON formatinda yanit ver. Turkce yaz." },
                            new { role = "user",   content = prompt }
                        },
                        max_tokens = 500,
                        temperature = 0.7
                    };

                    string requestBody = Newtonsoft.Json.JsonConvert.SerializeObject(bodyObj);

                    var httpContent = new System.Net.Http.StringContent(
                        requestBody,
                        System.Text.Encoding.UTF8,
                        "application/json"
                    );

                    var response = await client.PostAsync(
                        "https://api.openai.com/v1/chat/completions",
                        httpContent
                    );

                    string responseString = await response.Content.ReadAsStringAsync();
                    dynamic responseJson = Newtonsoft.Json.JsonConvert.DeserializeObject(responseString);

                    if (responseJson.error != null)
                        return Json(new { success = false, message = "AI servisi su an yanit veremiyor." });

                    string aiContent = responseJson.choices[0].message.content.ToString().Trim();

                    if (aiContent.Contains("{"))
                    {
                        int start = aiContent.IndexOf("{");
                        int end = aiContent.LastIndexOf("}");
                        if (end > start)
                            aiContent = aiContent.Substring(start, end - start + 1);
                    }

                    dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(aiContent);

                    return Json(new
                    {
                        success = true,
                        tavsiye = result.tavsiye != null ? (string)result.tavsiye : "",
                        bolum = result.bolum != null ? (string)result.bolum : "",
                        doktor = result.doktor != null ? (string)result.doktor : "",
                        aciklama = result.aciklama != null ? (string)result.aciklama : ""
                    });
                }
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Bir hata olustu. Lutfen tekrar deneyin." });
            }
        }
    }
}