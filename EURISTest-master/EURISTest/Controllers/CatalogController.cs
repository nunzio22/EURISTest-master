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
    public class CatalogController : Controller
    {
        private DataBaseContext db = new DataBaseContext();

        //
        // GET: /Catalog/

        public ActionResult Index()
        {
            return View(db.Catalogs.ToList());
        }

        //
        // GET: /Catalog/Details/5

        public ActionResult Details(string id = null)
        {
            Catalog catalog = db.Catalogs.Find(id);
            if (catalog == null)
            {
                return HttpNotFound();
            }
            var products = db.Products.Include(p => p.Catalogo);
            List<Product> controllo = new List<Product>();
            foreach (var item in products.ToList())
            {
                if (item.Catalogo.CatalogID == catalog.CatalogID)
                    controllo.Add(item);
            }

            return View(controllo);
        }

        //
        // GET: /Catalog/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Catalog/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Catalog catalog)
        {
            if (ModelState.IsValid)
            {
                Guid id = Guid.NewGuid();
                string idS = id.ToString();
                catalog.CatalogID = idS;

                // //se si volesse creare un id auto incrementate si puo fare così
                // var idN=((db.Products.Include(p => p.Catalogo)).ToList()).Count;
                ////se ProducutID fosse intero non servirebbe trasformare idn in stringa con il ToString
                //product.ProductID = idN.ToString();
                db.Catalogs.Add(catalog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(catalog);
        }

        //
        // GET: /Catalog/Edit/5

        public ActionResult Edit(string id = null)
        {
            Catalog catalog = db.Catalogs.Find(id);
            if (catalog == null)
            {
                return HttpNotFound();
            }
            return View(catalog);
        }

        //
        // POST: /Catalog/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Catalog catalog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(catalog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(catalog);
        }

        //
        // GET: /Catalog/Delete/5

        public ActionResult Delete(string id = null)
        {
            Catalog catalog = db.Catalogs.Find(id);
            if (catalog == null)
            {
                return HttpNotFound();
            }
            return View(catalog);
        }

        //
        // POST: /Catalog/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Catalog catalog = db.Catalogs.Find(id);
            db.Catalogs.Remove(catalog);
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