using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Evidence_01.Models;
using Evidence_01.Models.ViewModels;

namespace Evidence_01.Controllers
{
    public class HomeController : Controller
    {
        ProductDbEntities db = new ProductDbEntities();
        public ActionResult Index()
        {
            var products = db.Products.Include("Category");
            return View(products.OrderBy(x => x.ProductId).ToList());
        }
        public ActionResult Create()
        {
            ViewBag.Category = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(ProductsViewModel evm)
        {
            if (ModelState.IsValid)
            {
                if (evm.PictureUpload != null)
                {
                    string filepath = Path.Combine("~/Images", Guid.NewGuid().ToString() + Path.GetExtension(evm.PictureUpload.FileName));
                    evm.PictureUpload.SaveAs(Server.MapPath(filepath));

                    Product product = new Product
                    {
                        ProductName = evm.ProductName,
                        Price = evm.Price,
                        Picture = filepath,
                        CategoryId = evm.CategoryId,
                        OrderDate = evm.OrderDate
                    };
                    db.Products.Add(product);
                    db.SaveChanges();
                    return PartialView("_success");
                }
                else
                {
                    return PartialView("_error");
                }
            }
            ViewBag.Category = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View(evm);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ProductsViewModel evm = new ProductsViewModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Price = product.Price,
                Picture = product.Picture,
                CategoryId = product.CategoryId,
                OrderDate = (DateTime)product.OrderDate
            };
            ViewBag.Category = new SelectList(db.Categories, "CategoryId", "CategoryName", evm.CategoryId);
            return View(evm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductsViewModel evm)
        {
            if (ModelState.IsValid)
            {
                string filepath = evm.Picture;
                if (evm.PictureUpload != null)
                {
                    filepath = Path.Combine("~/Images", Guid.NewGuid().ToString() + Path.GetExtension(evm.PictureUpload.FileName));
                    evm.PictureUpload.SaveAs(Server.MapPath(filepath));

                    Product product = new Product
                    {
                        ProductId = evm.ProductId,
                        ProductName = evm.ProductName,
                        Price = evm.Price,
                        Picture = filepath,
                        CategoryId = evm.CategoryId,
                        OrderDate = evm.OrderDate
                    };
                    db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    Product product = new Product
                    {
                        ProductId = evm.ProductId,
                        ProductName = evm.ProductName,
                        Price = evm.Price,
                        Picture = filepath,
                        CategoryId = evm.CategoryId,
                        OrderDate = evm.OrderDate
                    };
                    db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Category = new SelectList(db.Categories, "CategoryId", "CategoryName", evm.ProductId);
            return View(evm);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}