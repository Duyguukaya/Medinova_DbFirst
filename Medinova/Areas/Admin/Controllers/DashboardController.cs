using Medinova.Filters;
using Medinova.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Medinova.Areas.Admin.Controllers
{
    [RoleAuthorize("Admin")]
    public class DashboardController : Controller
    {
        private readonly MedinovaContext _context;

        public DashboardController()
        {
            _context = new MedinovaContext();
        }

        public async Task<ActionResult> Index()
        {
            ViewBag.DoctorCount = await System.Data.Entity.QueryableExtensions.CountAsync(_context.Doctors);
            ViewBag.AppointmentCount = await System.Data.Entity.QueryableExtensions.CountAsync(_context.Appointments);
            ViewBag.DepartmentCount = await System.Data.Entity.QueryableExtensions.CountAsync(_context.Departments);
            ViewBag.UserCount = await System.Data.Entity.QueryableExtensions.CountAsync(_context.Users);
            return View();
        }
    }
}
