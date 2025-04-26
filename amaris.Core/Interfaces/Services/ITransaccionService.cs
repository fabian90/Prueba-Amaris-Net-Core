using amaris.Core.DTOs.Request;
using amaris.Core.DTOs.Response;
using amaris.Core.Entities;
using Commons.Response;

namespace amaris.Core.Interfaces.Services
{
    public interface ITransaccionService
    {
        Task<string> SuscribirAFondoAsync(TransaccionRequest request);
        Task<string> CancelarFondoAsync(TransaccionRequest request);
        Task<List<TransaccionResponse>> GetHistorialAsync(string clienteId);
        Task<RecordsResponse<TransaccionResponse>> GetTransaccionesPagedAsync(int page, int take);
    }
}
