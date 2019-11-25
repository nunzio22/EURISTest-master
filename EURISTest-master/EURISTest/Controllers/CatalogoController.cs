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
    public class CatalogoController : Controller
    {
        private DataBaseContext db = new DataBaseContext();

        //
        // GET: /Catalogo/

        public ActionResult Index()
        {
            return View(db.Cataloghi.ToList());
        }

        //
        // GET: /Catalogo/Details/5

        public ActionResult Details(int id = 0)
        {
            Catalogo catalogo = db.Cataloghi.Find(id);
            if (catalogo == null)
            {
                return HttpNotFound();
            }
            return View(catalogo);
        }


        public ActionResult Prodotti(int id=0)
        {
            Catalogo catalogo = db.Cataloghi.Find(id);
            if (catalogo == null)
            {
                return HttpNotFound();
            }
            var vendite = db.Vendite.Include(v => v.Cataloghi).Include(v => v.Prodotti);
            List<Vendita> pro = new List<Vendita>();
            foreach (var item in vendite)
            {
                if (item.Cataloghi.CatalogoID == catalogo.CatalogoID)
                    pro.Add(item);
            }
           
            return View(pro);
        }

        //
        // GET: /Catalogo/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Catalogo/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Catalogo catalogo)
        {
            if (ModelState.IsValid)
            {
                List<Catalogo> p = new List<Catalogo>();
                var id = p.Count + 1;
                catalogo.CatalogoID = id;
                db.Cataloghi.Add(catalogo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(catalogo);
        }

        public ActionResult NewProdotto(int id = 0)
        {
            List<Catalogo> c = new List<Catalogo>();
            c.Add(db.Cataloghi.Find(id));

            if (c == null)
            {
                return HttpNotFound();
            }
            ViewBag.FKProdottoID = new SelectList(db.Prodotti, "ProdottoID", "Nome");
            ViewBag.FKCatalogoID = new SelectList(c, "CatalogoID", "Nome");
            return View();
        }

        //
        // POST: /ProductCatalog/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewProdotto(Vendita vendita, int id=0)
        {
            if (ModelState.IsValid)
            {
                Guid gu = Guid.NewGuid();
                string idS = gu.ToString();
                vendita.VenditeID = idS;
                vendita.FKCataloghiID = id;
                db.Vendite.Add(vendita);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FKCataloghiID = new SelectList(db.Cataloghi, "CatalogoID", "Nome", vendita.FKCataloghiID);
            ViewBag.FKProdottoID = new SelectList(db.Prodotti, "ProdottoID", "Nome", vendita.FKProdottoID);
            return View(vendita);
        }


        //
        // GET: /Catalogo/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Catalogo catalogo = db.Cataloghi.Find(id);
            if (catalogo == null)
            {
                return HttpNotFound();
            }
            return View(catalogo);
        }

        //
        // POST: /Catalogo/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Catalogo catalogo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(catalogo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(catalogo);
        }




        public ActionResult EditV(string id = null)
        {
          
            Vendita vendita = db.Vendite.Find(id);
            var idp = vendita.FKProdottoID;
            var idc = vendita.FKCataloghiID;
            if (vendita == null)
            {
                return HttpNotFound();
            }
            List<Catalogo> c = new List<Catalogo>();
            c.Add(db.Cataloghi.Find(idc));
            List<Prodotto> p = new List<Prodotto>();
            p.Add(db.Prodotti.Find(idp));
            ViewBag.FKCataloghiID = new SelectList(c, "CatalogoID", "Nome", vendita.FKCataloghiID);
            ViewBag.FKProdottoID = new SelectList(p, "ProdottoID", "Nome", vendita.FKProdottoID);
            return View(vendita);
        }

        //
        // POST: /Vendita/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditV(Vendita vendita)
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
        // GET: /Catalogo/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Catalogo catalogo = db.Cataloghi.Find(id);
            if (catalogo == null)
            {
                return HttpNotFound();
            }
            return View(catalogo);
        }

        //
        // POST: /Catalogo/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Catalogo catalogo = db.Cataloghi.Find(id);
            db.Cataloghi.Remove(catalogo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //
        // GET: /ProductCatalog/Delete/5

        public ActionResult DeletePro(string id = null)
        {
            Vendita vendita = db.Vendite.Find(id);
            if (vendita == null)
            {
                return HttpNotFound();
            }
            return View(vendita);
        }

        //
        // POST: /ProductCatalog/Delete/5

        [HttpPost, ActionName("DeletePro")]
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