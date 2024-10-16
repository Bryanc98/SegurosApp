using Microsoft.AspNetCore.Mvc;
using SegurosApp.Dto;
using Microsoft.EntityFrameworkCore;
using SegurosApp.Data;
using SegurosApp.Models;

namespace SegurosApp.LN
{
    public class ClienteLN
    {
        private readonly AppDbContext _context;

        public ClienteLN(AppDbContext context)
        {
            this._context = context;
        }

        public ResponseDto<object> RegistrarCliente(ClienteDto datosCompra)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                int edadCliente = CalcularEdad(datosCompra.FechaNacimiento);
                var datosplanSeleccionado = _context.Planes.FirstOrDefault(p => p.CodigoPlan == datosCompra.CodigoPlan);

                if (datosplanSeleccionado == null)
                {
                    return new ResponseDto<object>(false, "El plan seleccionado no existe.", null);
                }

                if (edadCliente < datosplanSeleccionado!.EdadMin || edadCliente > datosplanSeleccionado!.EdadMax)
                {
                    return new ResponseDto<object>(false, "El cliente no se encuentra en el rango de edad válido para adquirir este producto", null);
                }

                var seguroSeleccionado = _context.Seguros.FirstOrDefault(s => s.CodigoProducto == datosCompra.CodigoSeguro);
                if (seguroSeleccionado == null)
                {
                    return new ResponseDto<object>(false, "El seguro seleccionado no existe.", null);
                }

                var productoFinancieroPermitido = _context.Seguro_ProductoFinancieros
                    .Include(sp => sp.ProductoFinanciero)
                    .Any(sp => sp.SeguroId == seguroSeleccionado.Id && sp.ProductoFinanciero!.CodigoProducto == datosCompra.CodigoProductoFinanciero);

                if (!productoFinancieroPermitido)
                {
                    return new ResponseDto<object>(false, "El producto financiero no está permitido para el seguro seleccionado.", null);
                }

                var productoFinancieroSeleccionado = _context.ProductoFinancieros.FirstOrDefault(pf => pf.CodigoProducto == datosCompra.CodigoProductoFinanciero);

                // Guardar datos del cliente o si existe lo actualiza
                var cliente = _context.Clientes.FirstOrDefault(c => c.Cedula == datosCompra.Cedula);
                if (cliente != null)
                {
                    // Actualizar datos del cliente existente
                    cliente.Nombre = datosCompra.Nombre;
                    cliente.Apellidos = datosCompra.Apellidos;
                    cliente.FechaNacimiento = datosCompra.FechaNacimiento;
                    _context.Clientes.Update(cliente);
                }
                else
                {
                    // Guardar datos del nuevo cliente
                    cliente = new Cliente
                    {
                        Nombre = datosCompra.Nombre,
                        Apellidos = datosCompra.Apellidos,
                        Cedula = datosCompra.Cedula,
                        FechaNacimiento = datosCompra.FechaNacimiento
                    };
                    _context.Clientes.Add(cliente);
                }

                // Guardar cambios en el contexto para asegurar que el cliente tiene un ID
                _context.SaveChanges();

                // Verifica si el cliente ya tiene una poliza de este mismo tipo
                var polizaExistente = _context.Cliente_Seguros
                    .Any(cs => cs.ClienteID == cliente.ID && cs.SeguroId == seguroSeleccionado.Id && cs.PlanId == datosplanSeleccionado.Id);

                if (polizaExistente)
                {
                    return new ResponseDto<object>(false, "El cliente ya tiene una póliza de este tipo.", null);
                }

                // Guardar Metodo de Pago
                var datosPago = new Cliente_ProductoFinanciero
                {
                    ClienteId = cliente.ID,
                    ProductoFinancieroId = productoFinancieroSeleccionado!.Id,
                    Numeroproducto = datosCompra.NumeroProductoFinanciero
                };

                _context.Cliente_ProductoFinancieros.Add(datosPago);

                var ultimoSeguro = _context.Cliente_Seguros
                    .Where(cs => cs.SeguroId == seguroSeleccionado.Id && cs.PlanId == datosplanSeleccionado.Id)
                    .OrderByDescending(cs => cs.ID)
                    .FirstOrDefault();

                int secuencial = ultimoSeguro != null ? int.Parse(ultimoSeguro.NumeroPoliza.Split('-').Last()) + 1 : 1;

                string numeroPoliza = $"{seguroSeleccionado.CodigoProducto}-{datosplanSeleccionado.CodigoPlan}-{secuencial:D4}";

                var datosSeguro = new Cliente_Seguro
                {
                    ClienteID = cliente.ID,
                    SeguroId = seguroSeleccionado.Id,
                    PlanId = datosplanSeleccionado.Id,
                    FechaRegistro = DateTime.Now,
                    NumeroPoliza = numeroPoliza,
                    ProductoFinancieroId = productoFinancieroSeleccionado!.Id
                };

                _context.Cliente_Seguros.Add(datosSeguro);
                _context.SaveChanges();

                transaction.Commit();

                return new ResponseDto<object>(true, "Compra realizada exitosamente", new
                {
                    NumeroPoliza = numeroPoliza,
                    MontoPagar = datosplanSeleccionado.couta,
                    FechaVigencia = DateTime.Now.AddYears(1)
                });
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return new ResponseDto<object>(false, "Error al registrar el cliente: " + ex.Message, null);
            }
        }



        public int CalcularEdad(DateTime fechaNacimiento)
        {
            DateTime fechaActual = DateTime.Now;
            int edad = fechaActual.Year - fechaNacimiento.Year;
            return edad;
        }
    }
}
