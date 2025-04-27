using amaris.Core.DTOs.Response;
using amaris.Core.Interfaces.Services;
using Commons.Response;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace amaris.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FondoController : ControllerBase
    {
        private readonly IFondoService _fondoService;

        public FondoController(IFondoService fondoService)
        {
            _fondoService = fondoService;
        }

        /// <summary>
        /// Obtiene los fondos disponibles para un cliente
        /// </summary>
        /// <param name="clienteId">Id del cliente</param>
        /// <returns>Lista de fondos disponibles</returns>
        [HttpGet("disponibles")]
        public async Task<IActionResult> GetFondosDisponiblesAsync()
        {
            var response = await _fondoService.GetFondosDisponiblesAsync();

            if (!response.Success)
            {
                return BadRequest(response.Message); // Retorna un error si no es exitoso
            }

            return Ok(response); // Retorna la lista de fondos disponibles
        }

        /// <summary>
        /// Obtiene los fondos paginados
        /// </summary>
        /// <param name="page">Número de página</param>
        /// <param name="take">Número de elementos por página</param>
        /// <returns>Lista de fondos</returns>
        //[HttpGet("paginados")]
        //public async Task<IActionResult> GetFondosPagedAsync(int page, int take)
        //{
        //    var response = await _fondoService.GetFondosPagedAsync(page, take);

        //    return Ok(response); // Retorna los fondos paginados
        //}
    }
}
