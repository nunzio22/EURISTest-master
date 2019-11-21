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
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "La descrizione è obbligatorio")]
        [StringLength(50, ErrorMessage = "La descrizione è troppo lungo (max 50 caratteri)")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Il codice è obbligatoria")]
        [StringLength(10, ErrorMessage = "Il codice è troppo lungo (max 10 caratteri)")]
        public string Code { get; set; }

        [ForeignKey("FKcatalogoID")]
        public Catalog Catalog { get; set; }
    }
}