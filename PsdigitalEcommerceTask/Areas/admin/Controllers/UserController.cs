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

namespace PsdigitalEcommerceTask.Areas.admin.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: admin/user/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: admin/user/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel Admin, string returnto)
        {
            Admin.Password = Crypto.HashPassword((Admin.Password));
            Admin Account =_unitOfWork.Admins.GetByCondition(u=>u.Username == Admin.Username);
            if (Account == null)
            {
                ViewBag.Error = "Incorrect Username or Password!";
            }else if (Crypto.VerifyHashedPassword(Account.Password, Admin.Password))
            {
                ViewBag.Error = "Incorrect Username or Password!";
            }
            else
            {
                Session["adminid"] = Account.Id;
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
            if (Session["userid"] != null && Guid.TryParse(Session["adminid"].ToString(), out Id))
                Session.Abandon();
            Session.Clear();
            return Redirect("~");
        }
        public ActionResult CreateAdmin()
        {
            Admin Admin = new Admin();
            if (ModelState.IsValid)
            {
                Admin.Id = Guid.NewGuid();
                Admin.Password = Crypto.HashPassword("12345678");
                Admin.Username = "Admin";
                Admin.CreatedAt = DateTime.Now;
                Admin.Name = "Admin";
                _unitOfWork.Admins.Insert(Admin);
                _unitOfWork.Save();

            }

            return RedirectToAction("Index", "Home");
        }


    }
}
