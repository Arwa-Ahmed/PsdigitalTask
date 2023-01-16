using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PsdigitalEcommerceTask.Attributes;
using PsdigitalEcommerceTask.Interfaces;
using PsdigitalEcommerceTask.Models;
using PsdigitalEcommerceTask.ViewModels;

namespace PsdigitalEcommerceTask.Areas.admin.Controllers
{
    [AdminLoggedIn]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: admin/Product
        [Route("/")]
        [Route("/product/index")]
        public ActionResult Index()
        {
            ViewBag.Message = ViewData["Message"];
            return View(_unitOfWork.Products.Get());
        }

        // GET: admin/Product/Details/5
        public ActionResult Details(int? id)
        {
            Product product = _unitOfWork.Products.GetByID(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: admin/Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product productViewModel)
        {
            var file = Request.Files.Count > 0 ? Request.Files["Image"] : null;
            if (file == null || file.ContentLength < 1)
            {
                ModelState.AddModelError("Image", "Invalid Image format or size");
            }
            if (ModelState.IsValid)
            {
                Product product = new Product();
                var ImagePath = Path.Combine(Server.MapPath("~/Images/Product"), file.FileName);
                file.SaveAs(ImagePath);

                product.Image = file.FileName;
                product.Title = productViewModel.Title;
                product.Price = productViewModel.Price;
                product.CreatedAt = DateTime.Now;
                _unitOfWork.Products.Insert(product);
                _unitOfWork.Save();
                ViewData["Message"] = "Product added successfully";
                return RedirectToAction("Index");
            }

            return View(productViewModel);
        }

        // GET: admin/Product/Edit/5
        public ActionResult Edit(int id)
        {
            Product product = _unitOfWork.Products.GetByID(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: admin/Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product productViewModel)
        {
            Product product = _unitOfWork.Products.GetByID(productViewModel.Id);
            if (product == null)
            {
                return HttpNotFound();
            }

            var file = Request.Files.Count > 0 ? Request.Files["Image"] : null;
            ModelState.Remove("Image");
          
            if (ModelState.IsValid)
            {
                if(file.ContentLength > 0)
                {
                    if (product.Image != null)
                        System.IO.File.Delete(Path.Combine(Server.MapPath("~/Images/Product"), product.Image));

                    var ImagePath = Path.Combine(Server.MapPath("~/Images/Product"), file.FileName);
                    file.SaveAs(ImagePath);
                    product.Image = file.FileName;
                }
               
                product.Title = productViewModel.Title;
                product.Price = productViewModel.Price;
                product.UpdatedAt = DateTime.Now;
                _unitOfWork.Products.Update(product);
                _unitOfWork.Save();
              

                ViewData["Message"] = "Product updated successfully";
                return RedirectToAction("Index");
            }

            return View(product);
        }
        public ActionResult Archive(int id)
        {
            Product product = _unitOfWork.Products.GetByID(id);
            if (product == null)
            {
                return HttpNotFound();
            }
          
            product.DeletedAt = DateTime.Now;
            _unitOfWork.Products.Update(product);
            _unitOfWork.Save();


            ViewData["Message"] = "Product Deleted successfully";
            return RedirectToAction("Index");
            
        }
    }
}
