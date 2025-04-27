using amaris.Core.DTOs.Response;
using Commons.Response;

namespace amaris.Core.Interfaces.Services
{
    public interface IFondoService
    {
        Task<RecordsResponse<FondoResponse>> GetFondosPagedAsync(int page, int take);
        Task<ApiResponse<List<FondoResponse>>> GetFondosDisponiblesAsync();
    }
}
