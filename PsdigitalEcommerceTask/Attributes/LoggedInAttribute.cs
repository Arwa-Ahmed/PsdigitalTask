using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PsdigitalEcommerceTask.Attributes
{
    public class LoggedInAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["userid"] == null)
            {
                filterContext.Result = new RedirectResult("/?returnto=" + filterContext.HttpContext.Request.RawUrl);
            }
        }
    }
}