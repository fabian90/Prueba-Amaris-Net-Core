using AutoMapper;
using Commons.RequestFilter;
using Commons.Response;
using FluentValidation;
using amaris.Core.DTOs.Request;
using amaris.Core.DTOs.Response;
using amaris.Core.Entities;
using amaris.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace amaris.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransaccionController : ControllerBase
    {
        private readonly ITransaccionService _transaccionService;
        private readonly IMapper _mapper;
        private readonly IValidator<TransaccionRequest> _validator;

        public TransaccionController(ITransaccionService transaccionService, IMapper mapper,
            IValidator<TransaccionRequest> validator)
        {
            _transaccionService = transaccionService;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpPost("suscribir")]
        public async Task<IActionResult> SuscribirAFondo([FromBody] TransaccionRequest request)
        {
            var validation = await _validator.ValidateAsync(request);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors?.Select(e => new ValidationResult()
                {
                    Code = e.ErrorCode,
                    PropertyName = e.PropertyName,
                    Message = e.ErrorMessage
                }));
            }

            var response = await _transaccionService.SuscribirAFondoAsync(request);
            return Ok(response);
        }

        [HttpPost("cancelar")]
        public async Task<IActionResult> CancelarFondo([FromBody] TransaccionRequest request)
        {
            var validation = await _validator.ValidateAsync(request);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors?.Select(e => new ValidationResult()
                {
                    Code = e.ErrorCode,
                    PropertyName = e.PropertyName,
                    Message = e.ErrorMessage
                }));
            }

            var response = await _transaccionService.CancelarFondoAsync(request);
            return Ok(response);
        }

        /// <summary>
        /// Retrieve all transactions for a client
        /// </summary>
        /// <param name="clienteId">Client Id</param>
        /// <returns></returns>
        [HttpGet("{clienteId}")]
        public async Task<IActionResult> GetHistorialAsync(string clienteId)
        {
            var response = await _transaccionService.GetHistorialAsync(clienteId);

            // Verificar si la respuesta es exitosa
            if (response.Success)
            {
                return Ok(response); // Retorna la respuesta si es exitosa
            }

            return BadRequest(response.Message); // Retorna un error si no es exitosa
        }
    }
}
