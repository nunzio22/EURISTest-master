using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace EURISTest.Models
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Catalogo> Cataloghi { get; set; }
        public DbSet<Prodotto> Prodotti { get; set; }
        public DbSet<Vendita> Vendite { get; set; }
        public DbSet<Categoria> Categorie { get; set; }


        public DataBaseContext() : base("DefaultConnection") { }
    }
}