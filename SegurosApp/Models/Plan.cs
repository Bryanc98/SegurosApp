using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegurosApp.Models
{
    public class Plan
    {
        [Key]
        public int Id { get; set; }
        public required string CodigoPlan { get; set; }
        public required string Nombre { get; set; }

        public int SeguroId { get; set; }
        public Seguro? Seguro { get; set; }

        public decimal couta { get; set; }
        public int EdadMin { get; set; }
        public int EdadMax { get; set; }
    }
}
