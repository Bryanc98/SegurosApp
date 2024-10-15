using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegurosApp.Models
{
    public class Cliente
    {
        [Key]
        public int ID { get; set; }
        public required string Nombre { get; set; }
        public required string Apellidos { get; set; }

        public required string Cedula { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public ICollection<Cliente_Seguro>? Cliente_Seguros { get; set; }
        public ICollection<Cliente_ProductoFinanciero>? Cliente_ProductoFinancieros { get; set; }


    }
}
