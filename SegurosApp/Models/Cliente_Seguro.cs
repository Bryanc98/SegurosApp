using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegurosApp.Models
{
    public class Cliente_Seguro
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int ClienteID { get; set; }
        public Cliente? Cliente { get; set; }

        [Required]
        public int SeguroId { get; set; }
        public Seguro? Seguro { get; set; }

        [Required]
        public int PlanId { get; set; }
        public Plan? Plan { get; set; }

        [Required]
        public DateTime FechaRegistro { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z0-9]{3}-[A-Za-z0-9]{3}-\d{4}$", ErrorMessage = "El número de póliza debe tener el formato XXX-XXX-0000")]
        public required string NumeroPoliza { get; set; }

        [Required]
        public int ProductoFinancieroId { get; set; }
        public ProductoFinanciero? ProductoFinanciero { get; set; }

    }
}
