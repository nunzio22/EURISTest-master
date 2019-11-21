using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EURISTest.Models
{
    [Table("Products")]
    public class Product : IComparable
    {
        [Key]
        public string ProductID { get; set; }

        public string Description { get; set; }

        public string FKCatalogID { get; set; }
        [ForeignKey("FKCatalogID")]
        public Catalog Catalogo { get; set; }


        public virtual int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            int ris;
            Product newLav = obj as Product;
            if (newLav != null)
            {
                ris = Catalogo.Description.CompareTo(newLav.Catalogo.Description);
            }
            else
            {
                throw new ArgumentException("L'oggetto da comparare non è una persona");
            }
            return ris;
        }
    }
}