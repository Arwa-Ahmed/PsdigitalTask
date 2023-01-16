using PsdigitalEcommerceTask.Interfaces;
using PsdigitalEcommerceTask.Models;
using PsdigitalEcommerceTask.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PsdigitalEcommerceTask.Services
{
    public class ShoppingCartService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public string ShoppingCartId { get; set; }
        public const string CartCookieKey = "CartId";
        public static ShoppingCartService GetCart(HttpContextBase context)
        { 
            var cart = new ShoppingCartService(new UnitOfWork(new PsdigitalEcommerceTaskContext()));
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        public int AddToCart(Product product)
        {
            var cartItem = _unitOfWork.Carts.GetByCondition(
                c => c.SessionId == new Guid(ShoppingCartId)
                && c.ProductId == product.Id);

            if (cartItem == null)
            {
                cartItem = new Cart()
                {
                    ProductId = product.Id,
                    SessionId = new Guid(ShoppingCartId),
                    Quantity = 1,
                    CreatedAt = DateTime.Now
                };
                _unitOfWork.Carts.Insert(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }
            _unitOfWork.Save();
            return cartItem.Quantity;
        }
        public int RemoveFromCart(int id)
        {
            // Get the cart
            var cartItem = _unitOfWork.Carts.GetByCondition(
                  c => c.SessionId == new Guid(ShoppingCartId)
                  && c.Id == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                    itemCount = cartItem.Quantity;
                }
                else
                {
                    _unitOfWork.Carts.Delete(cartItem);
                }
                // Save changes
                _unitOfWork.Save();
            }
            return itemCount;
        }
        public void EmptyCart()
        {
            var cartItems = _unitOfWork.Carts.Get(
                cart => cart.SessionId == new Guid(ShoppingCartId));

            foreach (var cartItem in cartItems)
            {
                _unitOfWork.Carts.Delete(cartItem);
            }
            // Save changes
            _unitOfWork.Save();
        }
        public List<Cart> GetCartItems()
        {
            return _unitOfWork.Carts.Get(
                cart => cart.SessionId == new Guid(ShoppingCartId)).ToList();
        }
        public int GetCount()
        {
            int? count = _unitOfWork.Carts.Get(cart => cart.SessionId == new Guid(ShoppingCartId)).Sum(c => c.Quantity);
            return count ?? 0;
        }
        public decimal GetTotal()
        {
            decimal? total =(decimal) _unitOfWork.Carts.Get(cart => cart.SessionId == new Guid(ShoppingCartId)).Sum(a => a.Product.Price * a.Quantity);

            return total ?? decimal.Zero;
        }
    
        public string GetCartId(HttpContextBase context)
        {
            if (context.Request.Cookies[CartCookieKey] == null)
            {

                if (context.Session["userid"] != null)
                {
                    var user = _unitOfWork.Users.GetByID(context.Session["userid"]);
                    var CartSession = _unitOfWork.CartSessions.GetByCondition(a => a.UserId == user.Id);
                    if (CartSession == null)
                    {
                        var NewCartSession = new CartSession { Id = Guid.NewGuid(), UserId = user.Id, CreatedAt = DateTime.Now };
                        _unitOfWork.CartSessions.Insert(NewCartSession);
                        _unitOfWork.Save();

                        CookieService.Save(CartCookieKey, NewCartSession.Id.ToString(),context);
                    }
                    else
                    {
                        CookieService.Save(CartCookieKey, CartSession.Id.ToString(), context);
                    }
                }
                else
                {
                    Guid tempCartId = Guid.NewGuid();
                    CookieService.Save(CartCookieKey, tempCartId.ToString(),context);
                }
                return context.Response.Cookies.Get(CartCookieKey).Value;
            }
            return context.Request.Cookies[CartCookieKey].Value;
        }
        public void MigrateCart(Guid sessionId)
        {
            var cartItems = _unitOfWork.Carts.Get(
                cart => cart.SessionId == new Guid(ShoppingCartId));

            foreach (var cartItem in cartItems)
            {
                cartItem.SessionId = sessionId;
                _unitOfWork.Carts.Update(cartItem);
            }
            // Save changes
            _unitOfWork.Save();
        }
        public int UpdateCartQuantity(int id,int quantity)
        {
            // Get the cart
            var cartItem = _unitOfWork.Carts.GetByCondition(
                  c => c.SessionId == new Guid(ShoppingCartId)
                  && c.Id == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (quantity > 1)
                {
                    cartItem.Quantity = quantity;
                }
                else
                {
                    _unitOfWork.Carts.Delete(cartItem);
                }
                // Save changes
                _unitOfWork.Save();
                itemCount = cartItem.Quantity;
            }
            return itemCount;
        }
        public void RemoveCart(int id)
        {
            // Get the cart
            var cartItem = _unitOfWork.Carts.GetByCondition(
                  c => c.SessionId == new Guid(ShoppingCartId)
                  && c.Id == id);

            if (cartItem != null)
            {
                _unitOfWork.Carts.Delete(cartItem);
                _unitOfWork.Save();
            }
        }
    }
}