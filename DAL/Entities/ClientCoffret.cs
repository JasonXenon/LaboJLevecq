using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ClientCoffret
    {
        [Required]
        public int ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client Client { get; set; }

        [Required]
        public int CoffretId { get; set; }

        [ForeignKey("CoffretId")]
        public Coffret Coffret { get; set; }

        [Required]
        public int Quantité { get; set; }

        [Required]
        public DateTime DateAchat { get; set; }
    }
}
