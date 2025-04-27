using amaris.Core.DTOs.Response;
using amaris.Core.Interfaces.Services;
using Commons.Response;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace amaris.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        /// <summary>
        /// Obtiene un cliente por su documento
        /// </summary>
        /// <param name="documento">Documento del cliente</param>
        /// <returns>Cliente encontrado</returns>
        [HttpGet("{documento}")]
        public async Task<IActionResult> GetByDocumentoAsync(string documento)
        {
            var response = await _clienteService.GetByDocumentoAsync(documento);

            if (!response.Success)
            {
                return BadRequest(response.Message); // Retorna un error si no es exitoso
            }

            return Ok(response); // Retorna el cliente si la operación es exitosa
        }

        /// <summary>
        /// Obtiene el saldo de un cliente
        /// </summary>
        /// <param name="clienteId">Id del cliente</param>
        /// <returns>Saldo del cliente</returns>
        [HttpGet("saldo/{clienteId}")]
        public async Task<IActionResult> GetSaldoAsync(string clienteId)
        {
            var response = await _clienteService.GetSaldoAsync(clienteId);

            if (!response.Success)
            {
                return BadRequest(response.Message); // Retorna un error si no es exitoso
            }

            return Ok(response); // Retorna el saldo si la operación es exitosa
        }

        /// <summary>
        /// Verifica si un cliente tiene saldo disponible para una operación
        /// </summary>
        /// <param name="clienteId">Id del cliente</param>
        /// <param name="montoRequerido">Monto requerido para la operación</param>
        /// <returns>Indica si el cliente tiene saldo suficiente</returns>
        [HttpGet("saldo-disponible/{clienteId}/{montoRequerido}")]
        public async Task<IActionResult> TieneSaldoDisponible(string clienteId, decimal montoRequerido)
        {
            var response = await _clienteService.TieneSaldoDisponible(clienteId, montoRequerido);

            if (!response.Success)
            {
                return BadRequest(response.Message); // Retorna un error si no es exitoso
            }

            return Ok(response); // Retorna si el cliente tiene saldo suficiente o no
        }
    }
}