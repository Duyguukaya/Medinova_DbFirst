using Medinova.Filters;
using Medinova.Models;
using Medinova.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Medinova.Areas.Admin.Controllers
{
    [RoleAuthorize("Admin")]
    public class AdminDoctorController : Controller
    {
        private readonly MedinovaContext _context;
        private readonly IGenericRepository<Medinova.Models.Doctor> _doctorRepo;
        private readonly IGenericRepository<Department> _departmentRepo;

        public AdminDoctorController()
        {
            _context = new MedinovaContext();
            _doctorRepo = new GenericRepository<Medinova.Models.Doctor>(_context);
            _departmentRepo = new GenericRepository<Department>(_context);
        }

        public async Task<ActionResult> Index()
        {
            var doctors = await _context.Doctors.Include("Department").ToListAsync();
            return View(doctors);
        }

        public async Task<ActionResult> Create()
        {
            var departments = await _departmentRepo.GetAllAsync();
            ViewBag.Departments = new SelectList(departments, "DepartmentId", "Name");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Medinova.Models.Doctor model)
        {
            if (ModelState.IsValid)
            {
                await _doctorRepo.CreateAsync(model);
                return RedirectToAction("Index");
            }
            var departments = await _departmentRepo.GetAllAsync();
            ViewBag.Departments = new SelectList(departments, "DepartmentId", "Name");
            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var doctor = await _doctorRepo.GetByIdAsync(id);
            if (doctor == null) return HttpNotFound();
            var departments = await _departmentRepo.GetAllAsync();
            ViewBag.Departments = new SelectList(departments, "DepartmentId", "Name", doctor.DepartmentId);
            return View(doctor);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Medinova.Models.Doctor model)
        {
            if (ModelState.IsValid)
            {
                await _doctorRepo.UpdateAsync(model);
                return RedirectToAction("Index");
            }
            var departments = await _departmentRepo.GetAllAsync();
            ViewBag.Departments = new SelectList(departments, "DepartmentId", "Name", model.DepartmentId);
            return View(model);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var doctor = await _context.Doctors.Include("Department").FirstOrDefaultAsync(x => x.DoctorId == id);
            if (doctor == null) return HttpNotFound();
            return View(doctor);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _doctorRepo.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Appointments(int id)
        {
            var doctor = await _doctorRepo.GetByIdAsync(id);
            if (doctor == null) return HttpNotFound();
            var appointments = await _context.Appointments
                .Include("Doctor")
                .Where(x => x.Doctorld == id)
                .OrderBy(x => x.AppointmentDate)
                .ToListAsync();
            ViewBag.DoctorName = doctor.FullName;
            ViewBag.DoctorId = id;
            return View(appointments);
        }
    }
}
