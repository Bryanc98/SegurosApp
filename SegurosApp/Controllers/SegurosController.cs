using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SegurosApp.Dto;
using SegurosApp.LN;
using SegurosApp.Models;

namespace SegurosApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SegurosController : ControllerBase
    {
        private readonly ClienteLN _clienteLN;
        private readonly SeguroLN _seguroLN;

        public SegurosController(ClienteLN clienteLN, SeguroLN seguroLN)
        {
            _clienteLN = clienteLN;
            _seguroLN = seguroLN;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClienteDto datos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await Task.Run(() => _clienteLN.RegistrarCliente(datos));
                if (result.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result.Message);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{cedula}")]
        public async Task<IActionResult> Get(string cedula)
        {
            try
            {
                var result = await _seguroLN.ConsultarClientePorCedula(cedula);
                if (result.Success)
                {
                    return Ok(result.Data);
                }
                else
                {
                    return NotFound(result.Message);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
