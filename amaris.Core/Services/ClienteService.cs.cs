using amaris.Core.DTOs.Response;
using amaris.Core.Entities;
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
    public class ClienteService : IClienteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClienteService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<ClienteResponse>> GetByDocumentoAsync(string documento)
        {
            try
            {
                var cliente = await _unitOfWork.ClienteRepository.GetByDocumentoAsync(documento);

                if (cliente == null)
                {
                    return new ApiResponse<ClienteResponse>
                    {
                        Success = false,
                        Message = "Cliente no encontrado."
                    };
                }

                var response = _mapper.Map<ClienteResponse>(cliente);

                return new ApiResponse<ClienteResponse>
                {
                    Data = response,
                    Success = true,
                    Message = "Cliente obtenido correctamente."
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ClienteResponse>
                {
                    Success = false,
                    Message = $"Error al obtener cliente: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<decimal>> GetSaldoAsync(string clienteId)
        {
            try
            {
                var cliente = await _unitOfWork.ClienteRepository.GetById(clienteId);

                if (cliente == null)
                {
                    return new ApiResponse<decimal>
                    {
                        Success = false,
                        Message = "Cliente no encontrado."
                    };
                }

                return new ApiResponse<decimal>
                {
                    Data = cliente.Saldo,
                    Success = true,
                    Message = "Saldo obtenido correctamente."
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<decimal>
                {
                    Success = false,
                    Message = $"Error al obtener saldo: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<bool>> TieneSaldoDisponible(string clienteId, decimal montoRequerido)
        {
            try
            {
                var saldoResponse = await GetSaldoAsync(clienteId);

                if (!saldoResponse.Success)
                {
                    return new ApiResponse<bool>
                    {
                        Success = false,
                        Message = saldoResponse.Message
                    };
                }

                bool tieneSaldo = saldoResponse.Data >= montoRequerido;

                return new ApiResponse<bool>
                {
                    Data = tieneSaldo,
                    Success = true,
                    Message = tieneSaldo
                        ? "El cliente tiene saldo suficiente."
                        : "El cliente no tiene saldo suficiente."
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = $"Error al verificar saldo disponible: {ex.Message}"
                };
            }
        }
    }
}
