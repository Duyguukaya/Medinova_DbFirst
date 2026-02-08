using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Medinova.Filters; 
using Medinova.Models; 
using Medinova.Repositories; 

namespace Medinova.Areas.Admin.Controllers
{
    [RoleAuthorize("Admin")] 
    public class AdminAboutController : Controller
    {
      
        private readonly IGenericRepository<About> _aboutRepository;

        public AdminAboutController()
        {
            var context = new MedinovaContext();
            _aboutRepository = new GenericRepository<About>(context);
        }

        public async Task<ActionResult> Index()
        {
            return View();
        }
    }
}