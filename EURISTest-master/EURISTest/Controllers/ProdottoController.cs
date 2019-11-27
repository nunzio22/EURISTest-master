using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EURISTest.Models;
//per i comenti guardare catalogocontroller
namespace EURISTest.Controllers
{
    public class ProdottoController : Controller
    {
        private DataBaseContext db = new DataBaseContext();

        //
        // GET: /Prodotto/

        public ActionResult Index(string categorie, string searchString)
        {
            var ordinata = (db.Prodotti.Include(p => p.Categorie));

            if (!String.IsNullOrEmpty(searchString))
            {
                ordinata = ordinata.Where(s => s.Nome.Contains(searchString));
            }
            var CatQuary = from d in db.Categorie
                           orderby d.CatalogID
                           select d.Nome;
            var CatList = CatQuary.ToList();
            ViewBag.Categorie = new SelectList(CatList);

            if (!string.IsNullOrEmpty(categorie))
            {
                ordinata = ordinata.Where(x => x.Categorie.Nome == categorie);
            }
            var ordinato = ordinata.ToList();

            ordinato.Sort();
            return View(ordinato);
        }

        //
        // GET: /Prodotto/Details/5

        public ActionResult Details(int id = 0)
        {
            Prodotto prodotto = db.Prodotti.Find(id);
            if (prodotto == null)
            {
                return HttpNotFound();
            }
            return View(prodotto);
        }

        public ActionResult Cataloghi(int id = 0)
        {
            Prodotto prodotto = db.Prodotti.Find(id);
            if (prodotto == null)
            {
                return HttpNotFound();
            }
            var vendite = db.Vendite.Include(v => v.Cataloghi).Include(v => v.Prodotti);
            List<Vendita> pro = new List<Vendita>();
            foreach (var item in vendite)
            {
                if (item.Prodotti.ProdottoID == prodotto.ProdottoID)
                    pro.Add(item);
            }
            
            return View(pro);
        }

        //
        // GET: /Prodotto/Create

        public ActionResult Create()
        {
            ViewBag.FKCategoriaID = new SelectList(db.Categorie, "CatalogID", "Nome");
            return View();
        }

        //
        // POST: /Prodotto/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Prodotto prodotto)
        {
            if (ModelState.IsValid)
            {
                List<Prodotto> p = new List<Prodotto>();
                var id = p.Count + 1;
                prodotto.ProdottoID = id;
                db.Prodotti.Add(prodotto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FKCategoriaID = new SelectList(db.Categorie, "CatalogID", "Nome", prodotto.FKCategoriaID);
            return View(prodotto);
        }

        //
        // GET: /Prodotto/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Prodotto prodotto = db.Prodotti.Find(id);
            if (prodotto == null)
            {
                return HttpNotFound();
            }
            ViewBag.FKCategoriaID = new SelectList(db.Categorie, "CatalogID", "Nome", prodotto.FKCategoriaID);
            return View(prodotto);
        }

        //
        // POST: /Prodotto/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Prodotto prodotto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prodotto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FKCategoriaID = new SelectList(db.Categorie, "CatalogID", "Nome", prodotto.FKCategoriaID);
            return View(prodotto);
        }

        //
        // GET: /Prodotto/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Prodotto prodotto = db.Prodotti.Find(id);
            if (prodotto == null)
            {
                return HttpNotFound();
            }
            return View(prodotto);
        }

        //
        // POST: /Prodotto/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prodotto prodotto = db.Prodotti.Find(id);
            db.Prodotti.Remove(prodotto);
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