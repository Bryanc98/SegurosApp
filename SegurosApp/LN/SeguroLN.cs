using Microsoft.EntityFrameworkCore;
using SegurosApp.Data;
using SegurosApp.Dto;
using SegurosApp.Models;

namespace SegurosApp.LN
{
    public class SeguroLN
    {
        private readonly AppDbContext _context;

        public SeguroLN(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto<object>> ConsultarClientePorCedula(string cedula)
        {
            var cliente = await _context.Clientes
                .Include(c => c.Cliente_Seguros!)
                .ThenInclude(cs => cs.Seguro)
                .Include(c => c.Cliente_Seguros!)
                .ThenInclude(cs => cs.Plan)
                .Include(c => c.Cliente_Seguros!)
                .ThenInclude(cs => cs.ProductoFinanciero)
                .FirstOrDefaultAsync(c => c.Cedula == cedula);

            if (cliente == null)
            {
                return new ResponseDto<object>(false, "Cliente no encontrado.", null);
            }

            var clienteSeguros = cliente.Cliente_Seguros?.Select(cs => new
            {
                Cliente = new
                {
                    cliente.Nombre,
                    cliente.Apellidos,
                    cliente.Cedula,
                    cliente.FechaNacimiento
                },
                TipoSeguro = cs.Seguro?.CodigoProducto,
                TipoPlan = cs.Plan?.CodigoPlan,
                NumeroPoliza = cs.NumeroPoliza,
                TipoProductoFinanciero = cs.ProductoFinanciero?.CodigoProducto,
                FechaRegistro = cs.FechaRegistro,
                FechaVencimiento = cs.FechaRegistro.AddYears(1),
                ValorCuota = cs.Plan?.couta
            }).ToList();

            return new ResponseDto<object>(true, "Consulta realizada exitosamente", clienteSeguros);
        }



    }
}