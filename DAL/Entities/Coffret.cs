using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Coffret
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Titre { get; set; }

        [MaxLength(255)]
        public string Bonus { get; set; }

        [Required]
        public decimal Prix { get; set; }

        [Required]
        public int Quantite { get; set; }

        public string Synopsis { get; set; }

        [MaxLength(255)]
        public string AfficheUrl { get; set; }

        // Relation avec Genre (clé étrangère)
        [Required]
        public int GenreId { get; set; }

        [ForeignKey("GenreId")]
        public Genre Genre { get; set; }

        // Relation N-N avec Client
        public ICollection<ClientCoffret> ClientCoffrets { get; set; }
    }
}
