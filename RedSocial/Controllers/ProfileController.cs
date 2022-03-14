using RedSocial.DB;
using RedSocial.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace RedSocial.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index(String Error = null)
        {
            ProfileViewModel model;
            using (RedSocialEntities db = new RedSocialEntities())
            {
                model = (from d in db.AspNetUsers
                         where d.UserName == System.Web.HttpContext.Current.User.Identity.Name
                         select new ProfileViewModel
                         {
                             Nombre = d.Nombre,
                             Apellido = d.Apellido,
                             Telefono = d.PhoneNumber,
                             Foto = d.Foto,
                         }).FirstOrDefault();

            }
            ViewBag.Imagen = new WebImage(model.Foto);
            ViewBag.Error = Error;
            return View(model);
        }
        [HttpPost]
        public ActionResult Editar(ProfileViewModel model)
        {
            try
            {
                HttpPostedFileBase http = Request.Files[0];
                WebImage webImage = new WebImage(http.InputStream);
                model.Foto = webImage.GetBytes();

                using (RedSocialEntities db = new RedSocialEntities())
                {
                    var odb = db.AspNetUsers.Where(d => d.UserName == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();
                    odb.Nombre = model.Nombre;
                    odb.Apellido = model.Apellido;
                    odb.PhoneNumber = model.Telefono;
                    odb.Foto = model.Foto;

                    db.Entry(odb).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return Redirect("~/Profile/Index");
            }
            catch (Exception ex)
            {
                if(ex.Message.Contains("An image could not be constructed from the content provided"))
                    return Redirect("~/Profile/Index?Error=El formato de la imagen es incorrecto");
                return Redirect("~/Profile/Index?Error="+ex.Message);
            }

        }
        public ActionResult imagen()
        {
            try
            {
                using (RedSocialEntities db = new RedSocialEntities())
                {
                    var model = (from d in db.AspNetUsers
                                 where d.UserName == System.Web.HttpContext.Current.User.Identity.Name
                                 select d.Foto
                         ).FirstOrDefault();

                    return File(model, "jpg");
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}