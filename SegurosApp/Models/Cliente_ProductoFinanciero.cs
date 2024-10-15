using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegurosApp.Models
{
    public class Cliente_ProductoFinanciero
    {
        [Key]
        public int ID { get; set; }
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        public int ProductoFinancieroId { get; set; }
        public ProductoFinanciero? ProductoFinanciero { get; set; }

        public int Numeroproducto { get; set; }
    }
}
