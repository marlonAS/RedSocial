using RedSocial.Controllers;
using System.Web.Mvc;

namespace RedSocial.Filtros
{
    public class VerificarSesion : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated && !(filterContext.Controller is AccountController))
            {
                filterContext.HttpContext.Response.Redirect("~/Account/Login");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}