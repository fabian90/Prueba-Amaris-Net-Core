using amaris.Core.DTOs.Response;
using amaris.Core.Interfaces.Repositories;
using amaris.Core.Interfaces.Services;
using AutoMapper;
using Commons.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amaris.Core.Services
{
    public class FondoService : IFondoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FondoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RecordsResponse<FondoResponse>> GetFondosPagedAsync(int page, int take)
        {
            var fondos = await _unitOfWork.FondoRepository.GetFondosPaged(page, take);
            return fondos;
        }


        /// <summary>
        /// Obtiene los fondos disponibles de un cliente.
        /// </summary>
        /// <param name="clienteId">ID del cliente.</param>
        /// <returns>Una respuesta con los fondos disponibles.</returns>
        public async Task<ApiResponse<List<FondoResponse>>> GetFondosDisponiblesAsync()
        {
            // Llamar al repositorio para obtener los fondos disponibles
            var fondos = await _unitOfWork.FondoRepository.GetFondosDisponiblesAsync();

            // Verificar si se encontraron fondos disponibles
            if (fondos == null || !fondos.Any())
            {
                return new ApiResponse<List<FondoResponse>>
                {
                    Data = new List<FondoResponse>(), // Lista vacía si no hay fondos
                    Success = false,
                    Message = "No se encontraron fondos disponibles."
                };
            }

            // Mapear los fondos a FondoResponse
            var fondoResponses = _mapper.Map<List<FondoResponse>>(fondos);

            // Crear la respuesta con los fondos disponibles
            return new ApiResponse<List<FondoResponse>>
            {
                Data = fondoResponses,
                Success = true,
                Message = "Fondos disponibles obtenidos correctamente."
            };
        }
    }
}
