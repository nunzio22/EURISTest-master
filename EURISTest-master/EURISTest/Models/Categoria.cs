using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EURISTest.Models
{
    [Table("Categorie")]
    public class Categoria
    {
        [Key]
        public string CatalogID { get; set; }

        [Required(ErrorMessage = "Il Nome è obbligatorio")]
        [StringLength(20, ErrorMessage = "Nome troppo lungo (max 20 caratteri)")]
        public string Nome { get; set; }

        public ICollection<Prodotto> Prodotti { get; set; }
    }
}