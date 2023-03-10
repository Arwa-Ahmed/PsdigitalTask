using System.Web.Mvc;

namespace PsdigitalEcommerceTask.Areas.admin
{
    public class adminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "admin",
                "admin/{controller}/{action}/{id}",
                new { action = "Index",controller = "product", id = UrlParameter.Optional }
            );
        }
    }
}