using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EURISTest.Models
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Product> Products { get; set; }

        public DataBaseContext() : base("DefaultConnection") { }
    }
}