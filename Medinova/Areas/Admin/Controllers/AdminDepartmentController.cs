using Medinova.Filters;
using Medinova.Models;
using Medinova.Repositories;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Medinova.Areas.Admin.Controllers
{
    [RoleAuthorize("Admin")]
    public class AdminDepartmentController : Controller
    {
        private readonly MedinovaContext _context;
        private readonly IGenericRepository<Department> _departmentRepo;

        public AdminDepartmentController()
        {
            _context = new MedinovaContext();
            _departmentRepo = new GenericRepository<Department>(_context);
        }

        public async Task<ActionResult> Index()
        {
            var list = await _departmentRepo.GetAllAsync();
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Department model)
        {
            if (ModelState.IsValid)
            {
                await _departmentRepo.CreateAsync(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var item = await _departmentRepo.GetByIdAsync(id);
            if (item == null) return HttpNotFound();
            return View(item);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Department model)
        {
            if (ModelState.IsValid)
            {
                await _departmentRepo.UpdateAsync(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var item = await _departmentRepo.GetByIdAsync(id);
            if (item == null) return HttpNotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _departmentRepo.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
