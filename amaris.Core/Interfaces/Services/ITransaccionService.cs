using amaris.Core.DTOs.Request;
using amaris.Core.DTOs.Response;
using amaris.Core.Entities;
using Commons.Response;

namespace amaris.Core.Interfaces.Services
{
    public interface ITransaccionService
    {
        Task<ApiResponse<TransaccionResponse>> SuscribirAFondoAsync(TransaccionRequest request);
        Task<ApiResponse<TransaccionResponse>> CancelarFondoAsync(TransaccionRequest request);
        Task<ApiResponse<List<TransaccionResponse>>> GetHistorialAsync(string clienteId);
        Task<RecordsResponse<TransaccionResponse>> GetTransaccionesPagedAsync(int page, int take);
    }
}
