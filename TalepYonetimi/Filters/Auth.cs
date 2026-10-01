using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TalepYonetimi.Web.Helpers;

namespace TalepYonetimi.Web.Filters
{
    
        public class Auth : Attribute, IActionFilter
        {
            public void OnActionExecuted(ActionExecutedContext context)
            {
                if (context.HttpContext.Session.KullaniciGetir() == null)
                {
                    context.Result = new RedirectResult("/Kullanicilar/Login");
                }

            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                if (context.HttpContext.Session.KullaniciGetir() == null)
                {
                    context.Result = new RedirectResult("/Kullanicilar/Login");
                }

            }
        }
    
}
