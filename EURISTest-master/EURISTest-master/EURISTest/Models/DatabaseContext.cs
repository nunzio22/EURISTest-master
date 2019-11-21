using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EURISTest.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Catalog> Catalog { get; set; }
        public DbSet<Product> Product { get; set; }

        // costruttore che fa uso di una connessione già 
        // specificata in web.config
        public DatabaseContext() :
            base("DefaultConnection")
        { }
    }
}