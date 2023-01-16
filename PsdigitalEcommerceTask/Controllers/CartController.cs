using PsdigitalEcommerceTask.Attributes;
using PsdigitalEcommerceTask.Interfaces;
using PsdigitalEcommerceTask.Services;
using PsdigitalEcommerceTask.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PsdigitalEcommerceTask.Controllers
{
    [LoggedIn]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Cart
        public ActionResult Index()
        {
            var user = _unitOfWork.Users.GetByID(HttpContext.Session["userid"]);
            var cartService = ShoppingCartService.GetCart(this.HttpContext);
            var cartItems = cartService.GetCartItems();
            var cart = new CartViewModel
            {
                Carts = cartItems,
                SubTotal = cartService.GetTotal(),
                ItemsCount = cartService.GetCount()

            };
            return View(cart);
        }

        public ActionResult AddToCart(int id)
        {
            var product = _unitOfWork.Products.GetByID(id);

            var cart = ShoppingCartService.GetCart(this.HttpContext);

            cart.AddToCart(product);

            return RedirectToAction("Index");
        }


        // AJAX: /Cart/RemoveCart/5
        [HttpPost]
        public ActionResult RemoveCart(int id)
        {
            var cart = ShoppingCartService.GetCart(this.HttpContext);

            cart.RemoveCart(id);

            var results = new 
            {
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = 0,
                CartId = id
            };
            return Json(results);
        }

        [HttpPost]
        public ActionResult UpdateQuantityInCart(int id,int quantity)
        {
            var cart = ShoppingCartService.GetCart(this.HttpContext);

            int itemCount = cart.UpdateCartQuantity(id,quantity);

            var results = new
            {
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                CartId = id
            };
            return Json(results);
        }

        public ActionResult GetCartCount()
        {
            var cart = ShoppingCartService.GetCart(this.HttpContext);

            var results = new
            {
                CartCount = cart.GetCount(),
            };
            return Json(results);
        }

        

    }
}