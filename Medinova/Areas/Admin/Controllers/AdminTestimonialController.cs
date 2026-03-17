using Medinova.Filters;
using Medinova.Models;
using Medinova.Repositories;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Medinova.Areas.Admin.Controllers
{
    [RoleAuthorize("Admin")]
    public class AdminTestimonialController : Controller
    {
        private readonly MedinovaContext _context;
        private readonly IGenericRepository<Testimonial> _repo;

        public AdminTestimonialController()
        {
            _context = new MedinovaContext();
            _repo = new GenericRepository<Testimonial>(_context);
        }

        public async Task<ActionResult> Index()
        {
            var list = await _repo.GetAllAsync();
            return View(list);
        }

        public ActionResult Create() => View();

        [HttpPost]
        public async Task<ActionResult> Create(Testimonial model)
        {
            if (ModelState.IsValid)
            {
                await _repo.CreateAsync(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null) return HttpNotFound();
            return View(item);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Testimonial model)
        {
            if (ModelState.IsValid)
            {
                await _repo.UpdateAsync(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null) return HttpNotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _repo.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
