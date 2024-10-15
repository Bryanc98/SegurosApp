using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegurosApp.Models
{
    public class Seguro_ProductoFinanciero
    {
        [Key]
        public int Id { get; set; }

        public int SeguroId { get; set; }
        public Seguro? Seguro { get; set; }

        public int ProductoFinancieroId { get; set; }
        public ProductoFinanciero? ProductoFinanciero { get; set; }


    }
}
