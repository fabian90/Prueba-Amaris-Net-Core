using amaris.Core.DTOs.Request;
using amaris.Core.DTOs.Response;
using amaris.Core.Entities;

namespace amaris.Core.Interfaces.Services
{
    public interface IClienteService
    {
        Task<Cliente?> GetByDocumentoAsync(string documento);
        Task<decimal> GetSaldoAsync(string clienteId);
        Task<bool> TieneSaldoDisponible(string clienteId, decimal montoRequerido);
    }
}
