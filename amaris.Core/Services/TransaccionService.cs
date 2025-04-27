using amaris.Core.DTOs.Request;
using amaris.Core.DTOs.Response;
using amaris.Core.Entities;
using amaris.Core.Interfaces.Repositories;
using amaris.Core.Interfaces.Services;
using Commons.Response;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amaris.Core.Services
{
    public class TransaccionService : ITransaccionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TransaccionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<TransaccionResponse>> SuscribirAFondoAsync(TransaccionRequest request)
        {
            try
            {
                var cliente = await _unitOfWork.ClienteRepository.GetById(request.IdCliente);
                var fondo = await _unitOfWork.FondoRepository.GetById(request.IdFondo);

                if (cliente == null || fondo == null)
                {
                    return new ApiResponse<TransaccionResponse>
                    {
                        Success = false,
                        Message = "Cliente o Fondo no encontrado."
                    };
                }

                if (cliente.Saldo < fondo.MontoMinimo)
                {
                    return new ApiResponse<TransaccionResponse>
                    {
                        Success = false,
                        Message = $"No tiene saldo disponible para vincularse al fondo {fondo.Nombre}."
                    };
                }

                var transaccion = new Transaccion
                {
                    IdTransaccion = Guid.NewGuid().ToString(),
                    IdCliente = cliente.IdCliente,
                    IdFondo = fondo.IdFondo,
                    Tipo = "Apertura",
                    Monto = fondo.MontoMinimo,
                    Fecha = DateTime.UtcNow,
                    MedioNotificacion = request.MedioNotificacion,
                    Descripcion = "Suscripción a fondo " + fondo.Nombre
                };

                cliente.Saldo -= fondo.MontoMinimo;

                await _unitOfWork.TransaccionRepository.Add(transaccion);
                await _unitOfWork.ClienteRepository.Update(cliente);

                var response = _mapper.Map<TransaccionResponse>(transaccion);

                return new ApiResponse<TransaccionResponse>
                {
                    Data = response,
                    Success = true,
                    Message = "Suscripción realizada exitosamente."
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<TransaccionResponse>
                {
                    Success = false,
                    Message = $"Error al suscribirse al fondo: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<TransaccionResponse>> CancelarFondoAsync(TransaccionRequest request)
        {
            try
            {
                var cliente = await _unitOfWork.ClienteRepository.GetById(request.IdCliente);
                var fondo = await _unitOfWork.FondoRepository.GetById(request.IdFondo);

                if (cliente == null || fondo == null)
                {
                    return new ApiResponse<TransaccionResponse>
                    {
                        Success = false,
                        Message = "Cliente o Fondo no encontrado."
                    };
                }

                var transaccion = new Transaccion
                {
                    IdTransaccion = Guid.NewGuid().ToString(),
                    IdCliente = cliente.IdCliente,
                    IdFondo = fondo.IdFondo,
                    Tipo = "Cancelación",
                    Monto = fondo.MontoMinimo,
                    Fecha = DateTime.UtcNow,
                    MedioNotificacion = request.MedioNotificacion,
                    Descripcion = "Cancelación de fondo " + fondo.Nombre
                };

                cliente.Saldo += fondo.MontoMinimo;

                await _unitOfWork.TransaccionRepository.Add(transaccion);
                await _unitOfWork.ClienteRepository.Update(cliente);

                var response = _mapper.Map<TransaccionResponse>(transaccion);

                return new ApiResponse<TransaccionResponse>
                {
                    Data = response,
                    Success = true,
                    Message = "Cancelación realizada exitosamente."
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<TransaccionResponse>
                {
                    Success = false,
                    Message = $"Error al cancelar el fondo: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<List<TransaccionResponse>>> GetHistorialAsync(string clienteId)
        {
            try
            {
                var historial = await _unitOfWork.TransaccionRepository.GetHistorialTransaccionesAsync(clienteId);

                if (historial == null || !historial.Any())
                {
                    return new ApiResponse<List<TransaccionResponse>>
                    {
                        Success = false,
                        Message = "No se encontraron transacciones para el cliente."
                    };
                }

                var respuesta = _mapper.Map<List<TransaccionResponse>>(historial);

                return new ApiResponse<List<TransaccionResponse>>
                {
                    Data = respuesta,
                    Success = true,
                    Message = "Historial de transacciones obtenido exitosamente."
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<TransaccionResponse>>
                {
                    Success = false,
                    Message = $"Error al obtener historial de transacciones: {ex.Message}"
                };
            }
        }

        public async Task<RecordsResponse<TransaccionResponse>> GetTransaccionesPagedAsync(int page, int take)
        {
            var historial = await _unitOfWork.TransaccionRepository.GetTransaccionPaged(page, take);
            return historial;
        }
    }

}
