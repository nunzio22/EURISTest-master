using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace EURISTest.Models
{
    [Table("Vendite")]
    public class Vendita 
    {
        [Key]
        public string VenditeID { get; set; }
        public int FKCataloghiID { get; set; }
        [ForeignKey("FKCataloghiID")]
        public Catalogo Cataloghi { get; set; }
        public int FKProdottoID { get; set; }
        [ForeignKey("FKProdottoID")]
        public Prodotto Prodotti { get; set; }
        public int PrezzoI { get; set; }
        public string Prezzo
        {
            get
            {
                var ris = PrezzoI.ToString("C");
                return ris;
            }
        }

    }
}