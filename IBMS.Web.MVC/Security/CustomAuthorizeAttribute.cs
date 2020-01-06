using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace IBMS.Web.MVC.Security
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public int menu { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //HttpCookie cookie = filterContext.HttpContext.Request.Cookies["Remember_Me_Portal"];

            if (CurrentSession.LoggedUserPortal == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index" }));
            }
            else
            {
                if (menu != 0 && !CurrentSession.LoggedUserPortal.AllowedFunctionList.Contains(menu))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
                }
                //else if (menu == 0)
                //{
                //    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
                //}
                else
                {
                    filterContext.Controller.ViewBag.LoggedUserName = CurrentSession.LoggedUserPortal.UserName;
                    filterContext.Controller.ViewBag.LoggedUserDesignation = CurrentSession.LoggedUserPortal.DesignationName;
                }
            }
        }
    }
}