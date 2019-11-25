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
    public class CategoriaController : Controller
    {
        private DataBaseContext db = new DataBaseContext();

        //
        // GET: /Categoria/

        public ActionResult Index()
        {
            return View(db.Categorie.ToList());
        }

        //
        // GET: /Categoria/Details/5

        public ActionResult Details(string id = null)
        {
            Categoria categoria = db.Categorie.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        //
        // GET: /Categoria/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Categoria/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                Guid id = Guid.NewGuid();
                string idS = id.ToString();
                categoria.CatalogID = idS;
                db.Categorie.Add(categoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoria);
        }

        //
        // GET: /Categoria/Edit/5

        public ActionResult Edit(string id = null)
        {
            Categoria categoria = db.Categorie.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        //
        // POST: /Categoria/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoria);
        }



        //
        // GET: /Categoria/Delete/5

        public ActionResult Delete(string id = null)
        {
            Categoria categoria = db.Categorie.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        //
        // POST: /Categoria/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Categoria categoria = db.Categorie.Find(id);
            db.Categorie.Remove(categoria);
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