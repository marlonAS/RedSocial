using RedSocial.DB;
using System.Linq;
using System.Web.Mvc;

namespace RedSocial.Controllers
{
    public class PostsController : Controller
    {
        // GET: Posts
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult cargarPosts()
        {
            using (RedSocialEntities db = new RedSocialEntities())
            {
                return PartialView("cargarPosts", db.Posts.Include("AspNetUsers").Include("Likes").Include("Comments").OrderByDescending(p => p.Fecha).ToList());
            }
        }
    }
}