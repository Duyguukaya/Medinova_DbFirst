using Medinova.Dtos;
using Medinova.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace Medinova.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        MedinovaContext context = new MedinovaContext();
    
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginDto model)
        {
            // 1. Kullanıcıyı buluyoruz
            var user = context.Users.FirstOrDefault(x => x.UserName == model.UserName && x.Password == model.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
                return View(model);
            }

            // 2. Kullanıcının tüm rollerini çekip diziye (Array) çeviriyoruz
            var roles = user.Roles.Select(x => x.RoleName).ToArray();
            Session["UserRoles"] = roles;

            // 3. Standart oturum bilgilerini dolduruyoruz
            FormsAuthentication.SetAuthCookie(user.UserName, false);
            Session["userName"] = user.UserName;
            Session["fullName"] = user.FirstName + " " + user.LastName;

            // 4. TRAFİK YÖNLENDİRMESİ (Burada kimin nereye gideceğine karar veriyoruz)
            // Eğer rollerin içinde Admin varsa Admin Area'sına gönder
            if (roles.Contains("Admin"))
            {
                return RedirectToAction("Index", "AdminAbout", new { area = "Admin" });
            }
            // Eğer rollerin içinde Doktor varsa Doktor Area'sına gönder
            else if (roles.Contains("Doctor"))
            {
                return RedirectToAction("Index", "DoctorDashboard", new { area = "Doctor" });
            }
            // Eğer rollerin içinde Hasta varsa Hasta Area'sına gönder
            else if (roles.Contains("Patient"))
            {
                return RedirectToAction("Index", "PatientPanel", new { area = "Patient" });
            }

            // Eğer hiçbir rolü yoksa ana sayfaya gönder
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}