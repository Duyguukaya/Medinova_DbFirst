using Medinova.Filters;
using Medinova.Models;
using Medinova.Repositories;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Medinova.Areas.Admin.Controllers
{
    [RoleAuthorize("Admin")]
    public class AdminBannerController : Controller
    {
        private readonly MedinovaContext _context;
        private readonly IGenericRepository<Banner> _bannerRepo;

        public AdminBannerController()
        {
            _context = new MedinovaContext();
            _bannerRepo = new GenericRepository<Banner>(_context);
        }

        public async Task<ActionResult> Index()
        {
            var list = await _bannerRepo.GetAllAsync();
            return View(list);
        }

        public ActionResult Create() => View();

        [HttpPost]
        public async Task<ActionResult> Create(Banner model)
        {
            if (ModelState.IsValid)
            {
                await _bannerRepo.CreateAsync(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var item = await _bannerRepo.GetByIdAsync(id);
            if (item == null) return HttpNotFound();
            return View(item);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Banner model)
        {
            if (ModelState.IsValid)
            {
                await _bannerRepo.UpdateAsync(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var item = await _bannerRepo.GetByIdAsync(id);
            if (item == null) return HttpNotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _bannerRepo.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
