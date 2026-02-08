using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medinova.Filters
{
    public class RoleAuthorize : AuthorizeAttribute
    {
        private readonly string _role;

        // Kapıya hangi rolün girebileceğini buradan söylüyoruz
        public RoleAuthorize(string role)
        {
            _role = role;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated) return false;

            // Login anında Session'a attığımız rolleri kontrol et
            var roles = httpContext.Session["UserRoles"] as string[];

            // Kapıdaki rol (Admin, Doctor veya Patient) listede var mı?
            return roles != null && roles.Contains(_role);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Yetkisi yoksa herkesi ortak Login sayfasına gönder
            filterContext.Result = new RedirectResult("/Account/Login");
        }
    }

}