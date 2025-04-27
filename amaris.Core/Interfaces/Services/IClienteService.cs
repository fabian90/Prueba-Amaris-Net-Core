 using amaris.Core.DTOs.Request;
using amaris.Core.DTOs.Response;
using amaris.Core.Entities;
using Commons.Response;

namespace amaris.Core.Interfaces.Services
{
    public interface IClienteService
    {
        Task<ApiResponse<ClienteResponse>> GetByDocumentoAsync(string documento);
        Task<ApiResponse<decimal>> GetSaldoAsync(string clienteId);
        Task<ApiResponse<bool>> TieneSaldoDisponible(string clienteId, decimal montoRequerido);
    }
}
