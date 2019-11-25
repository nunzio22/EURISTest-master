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
    public class VenditaController : Controller
    {
        private DataBaseContext db = new DataBaseContext();

        //
        // GET: /Vendita/

        public ActionResult Index()
        {
            var vendite = db.Vendite.Include(v => v.Cataloghi).Include(v => v.Prodotti);
            return View(vendite.ToList());
        }

        //
        // GET: /Vendita/Details/5

        public ActionResult Details(string id = null)
        {
            Vendita vendita = db.Vendite.Find(id);
            if (vendita == null)
            {
                return HttpNotFound();
            }
            return View(vendita);
        }

        //
        // GET: /Vendita/Create

        public ActionResult Create()
        {
            ViewBag.FKCataloghiID = new SelectList(db.Cataloghi, "CatalogoID", "Nome");
            ViewBag.FKProdottoID = new SelectList(db.Prodotti, "ProdottoID", "Nome");
            return View();
        }

        //
        // POST: /Vendita/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Vendita vendita)
        {
            if (ModelState.IsValid)
            {
                Guid id = Guid.NewGuid();
                string idS = id.ToString();
                vendita.VenditeID = idS;
                db.Vendite.Add(vendita);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FKCataloghiID = new SelectList(db.Cataloghi, "CatalogoID", "Nome", vendita.FKCataloghiID);
            ViewBag.FKProdottoID = new SelectList(db.Prodotti, "ProdottoID", "Nome", vendita.FKProdottoID);
            return View(vendita);
        }

        //
        // GET: /Vendita/Edit/5

        public ActionResult Edit(string id = null)
        {
            Vendita vendita = db.Vendite.Find(id);
            if (vendita == null)
            {
                return HttpNotFound();
            }
            ViewBag.FKCataloghiID = new SelectList(db.Cataloghi, "CatalogoID", "Nome", vendita.FKCataloghiID);
            ViewBag.FKProdottoID = new SelectList(db.Prodotti, "ProdottoID", "Nome", vendita.FKProdottoID);
            return View(vendita);
        }

        //
        // POST: /Vendita/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Vendita vendita)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vendita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FKCataloghiID = new SelectList(db.Cataloghi, "CatalogoID", "Nome", vendita.FKCataloghiID);
            ViewBag.FKProdottoID = new SelectList(db.Prodotti, "ProdottoID", "Nome", vendita.FKProdottoID);
            return View(vendita);
        }

        //
        // GET: /Vendita/Delete/5

        public ActionResult Delete(string id = null)
        {
            Vendita vendita = db.Vendite.Find(id);
            if (vendita == null)
            {
                return HttpNotFound();
            }
            return View(vendita);
        }

        //
        // POST: /Vendita/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Vendita vendita = db.Vendite.Find(id);
            db.Vendite.Remove(vendita);
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