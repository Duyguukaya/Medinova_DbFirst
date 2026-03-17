using Medinova.Filters;
using Medinova.Models;
using Medinova.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Medinova.Areas.Admin.Controllers
{
    [RoleAuthorize("Admin")]
    public class AdminUserController : Controller
    {
        private readonly MedinovaContext _context;

        public AdminUserController()
        {
            _context = new MedinovaContext();
        }

        public async Task<ActionResult> Index()
        {
            var users = await _context.Users.Include("Roles").ToListAsync();
            return View(users);
        }

        public async Task<ActionResult> Create()
        {
            ViewBag.Roles = await _context.Roles.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(User model, int[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                if (selectedRoles != null)
                {
                    foreach (var roleId in selectedRoles)
                    {
                        var role = await _context.Roles.FindAsync(roleId);
                        if (role != null) model.Roles.Add(role);
                    }
                }
                _context.Users.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Roles = await _context.Roles.ToListAsync();
            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var user = await _context.Users.Include("Roles").FirstOrDefaultAsync(x => x.UserId == id);
            if (user == null) return HttpNotFound();
            ViewBag.Roles = await _context.Roles.ToListAsync();
            ViewBag.UserRoleIds = user.Roles.Select(r => r.RoleId).ToList();
            return View(user);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(User model, int[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.Include("Roles").FirstOrDefaultAsync(x => x.UserId == model.UserId);
                if (user == null) return HttpNotFound();

                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                if (!string.IsNullOrWhiteSpace(model.Password))
                    user.Password = model.Password;

                user.Roles.Clear();
                if (selectedRoles != null)
                {
                    foreach (var roleId in selectedRoles)
                    {
                        var role = await _context.Roles.FindAsync(roleId);
                        if (role != null) user.Roles.Add(role);
                    }
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Roles = await _context.Roles.ToListAsync();
            return View(model);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var user = await _context.Users.Include("Roles").FirstOrDefaultAsync(x => x.UserId == id);
            if (user == null) return HttpNotFound();
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.Include("Roles").FirstOrDefaultAsync(x => x.UserId == id);
            if (user == null) return HttpNotFound();
            user.Roles.Clear();
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
