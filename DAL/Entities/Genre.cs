using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Label { get; set; }

        // Relation 1-N avec Coffret
        public ICollection<Coffret> Coffrets { get; set; }
    }
}
