using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PsdigitalEcommerceTask.Attributes
{
    public class AdminLoggedInAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["adminid"] == null)
            {
                filterContext.Result = new RedirectResult("/admin/User/Login/?returnto=" + filterContext.HttpContext.Request.RawUrl);
            }
        }
    }
}