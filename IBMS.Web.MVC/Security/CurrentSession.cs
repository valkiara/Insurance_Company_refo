using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IBMS.Web.MVC.Models;

namespace IBMS.Web.MVC.Security
{
    public class CurrentSession
    {
        public static LoginData LoggedUserPortal
        {
            get
            {
                if (HttpContext.Current.Session["LoggedUserPortal"] == null)
                {
                    return null;
                }
                else
                {
                    return (LoginData)HttpContext.Current.Session["LoggedUserPortal"];
                }
            }
            set
            {
                HttpContext.Current.Session["LoggedUserPortal"] = value;
            }
        }
    }
}