using PsdigitalEcommerceTask.Interfaces;
using PsdigitalEcommerceTask.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PsdigitalEcommerceTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ActionResult Index()
        {
           ViewBag.Products =  _unitOfWork.Products.Get(p=>p.DeletedAt == null);
            return View();
        }
        public ActionResult UpdateCartCookie()
        {
            var cart = ShoppingCartService.GetCart(this.HttpContext);

            var results = new
            {
                CartCount = cart.GetCount(),
            };

            return Json(results, JsonRequestBehavior.AllowGet);
        }
    }
}