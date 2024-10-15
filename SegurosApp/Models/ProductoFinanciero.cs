using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegurosApp.Models
{
    public class ProductoFinanciero
    {
        [Key]
        public int Id { get; set; }
        public required string CodigoProducto { get; set; }
        public required string Nombre { get; set; }

    }
}
