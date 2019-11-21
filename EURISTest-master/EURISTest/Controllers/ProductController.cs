using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EURISTest.Models;

namespace EURISTest.Controllers
{
    public class ProductController : Controller
    {
        private DataBaseContext db = new DataBaseContext();

        //
        // GET: /Product/

        public ActionResult Index()
        {
            List<Product> ordinata = new List<Product>();
            ordinata = (db.Products.Include(p => p.Catalogo)).ToList();
            ordinata.Sort();
            return View(ordinata);
        }

        //
        // GET: /Product/Details/5

        public ActionResult Details(string id = null)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // GET: /Product/Create

        public ActionResult Create()
        {
            ViewBag.FKCatalogID = new SelectList(db.Catalogs, "CatalogID", "Description");
            return View();
        }

        //
        // POST: /Product/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                Guid id = Guid.NewGuid();
                string idS = id.ToString();
                product.ProductID = idS;
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FKCatalogID = new SelectList(db.Catalogs, "CatalogID", "Description", product.FKCatalogID);
            return View(product);
        }

        //
        // GET: /Product/Edit/5

        public ActionResult Edit(string id = null)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.FKCatalogID = new SelectList(db.Catalogs, "CatalogID", "Description", product.FKCatalogID);
            return View(product);
        }

        //
        // POST: /Product/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FKCatalogID = new SelectList(db.Catalogs, "CatalogID", "Description", product.FKCatalogID);
            return View(product);
        }

        //
        // GET: /Product/Delete/5

        public ActionResult Delete(string id = null)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // POST: /Product/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}