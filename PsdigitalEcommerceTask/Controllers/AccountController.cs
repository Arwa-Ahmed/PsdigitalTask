using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using PsdigitalEcommerceTask.Attributes;
using PsdigitalEcommerceTask.Interfaces;
using PsdigitalEcommerceTask.Models;
using PsdigitalEcommerceTask.ViewModels;

namespace PsdigitalEcommerceTask.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel user, string returnto)
        {
            user.Password = Crypto.HashPassword((user.Password));
            User Account =_unitOfWork.Users.GetByCondition(u=>u.Username == user.Username);
            if (Account == null)
            {
                ViewBag.Error = "Incorrect Username or Password!";
            }else if (Crypto.VerifyHashedPassword(Account.Password, user.Password))
            {
                ViewBag.Error = "Incorrect Username or Password!";
            }
            else
            {
                Session["userid"] = Account.Id;
                Session["name"] = Account.Name;
                Session["username"] = Account.Username;
                if(returnto != null)
                {
                    return Redirect(returnto);
                }
                return RedirectToAction("Index", "Home");
            }
              

            return View();
        }
        [LoggedIn]
        public ActionResult Logout(string returnto)
        {
            Guid Id;
            if (Session["userid"] != null && Guid.TryParse(Session["userid"].ToString(), out Id))
                Session.Abandon();
            Session.Clear();
            return Redirect("~");
        }
        public ActionResult CreateUser()
        {
            User user = new User();
            if (ModelState.IsValid)
            {
                user.Id = Guid.NewGuid();
                user.Password = Crypto.HashPassword("12345678");
                user.Email = "user@user.com";
                user.Username = "user";
                user.CreatedAt = DateTime.Now;
                user.Name = "user";
                _unitOfWork.Users.Insert(user);
                _unitOfWork.Save();

            }

            return RedirectToAction("Index", "Home");
        }


    }
}
