using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EURIS.Service;
using EURIS.Entities;
using EURISTest.Models;

namespace EURISTest.Controllers
{
    public class CatalogController : Controller
    {
        //
        // GET: /Product/

        private DatabaseContext db = new DatabaseContext();

        // GET: /Azienda/
        public ActionResult Index()
        {
            return View(db.Catalog.ToList());
        }

    }
}
