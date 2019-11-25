using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EURISTest.Models
{
    [Table("Prodotti")]
    public class Prodotto : IComparable
    {
        [Key]
        public int ProdottoID { get; set; }

        [Required(ErrorMessage = "Il Nome è obbligatorio")]
        [StringLength(20, ErrorMessage = "Nome troppo lungo (max 20 caratteri)")]
        public string Nome { get; set; }

        [StringLength(50, ErrorMessage = "Descrizione troppo lungo (max 50 caratteri)")]
        public string Descrizione { get; set; }

        public ICollection<Vendita> VenditaPro { get; set; }

        public string FKCategoriaID { get; set; }
        [ForeignKey("FKCategoriaID")]
        public Categoria Categorie { get; set; }


        public virtual int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            int ris;
            if (obj is Prodotto newLav)
            {
                ris = Categorie.Nome.CompareTo(newLav.Categorie.Nome);
            }
            else
            {
                throw new ArgumentException("L'oggetto da comparare non è una persona");
            }
            return ris;
        }
    }
}
    