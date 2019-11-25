using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EURISTest.Models
{
    [Table("Cataloghi")]
    public class Catalogo
    {
        [Key]
        public int CatalogoID { get; set; }

        [Required(ErrorMessage = "Il Nome è obbligatorio")]
        [StringLength(20, ErrorMessage = "Nome troppo lungo (max 20 caratteri)")]
        public string Nome { get; set; }

        [StringLength(50, ErrorMessage = "Descrizione troppo lungo (max 50 caratteri)")]
        public string Descrizione { get; set; }

        public ICollection<Vendita> VenditaCat { get; set; }
    }
}