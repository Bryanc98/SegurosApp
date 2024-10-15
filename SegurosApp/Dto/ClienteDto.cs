using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace SegurosApp.Dto
{
    public class ClienteDto
    {
        [Required]
        public required string Nombre { get; set; }

        [Required]
        public required string Apellidos { get; set; }

        [Required]
        [RegularExpression(@"^\d{3}-\d{7}-\d{1}$", ErrorMessage = "La cédula debe tener el formato 000-0000000-0.")]
        public required string Cedula { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public required string CodigoSeguro { get; set; }
        public required string CodigoPlan { get; set; }

        public required string CodigoProductoFinanciero { get; set; }
        public required int NumeroProductoFinanciero { get; set; }

    }
}
