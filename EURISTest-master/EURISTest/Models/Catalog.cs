using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EURISTest.Models
{
    [Table("Catalogs")]
    public class Catalog
    {
        [Key]
        public string CatalogID { get; set; }

        public string Description { get; set; }

        public ICollection<Product> Prodotti { get; set; }
    }
}