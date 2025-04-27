using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using amaris.Core.DTOs.Response;
using amaris.Core.Entities;
using Commons.Repository.Interfaces;
using Commons.Response;

namespace amaris.Core.Interfaces.Repositories
{
    public interface IFondoRepository : IGenericRepository<Fondo>
    {
        Task<RecordsResponse<FondoResponse>> GetFondosPaged(int page, int take);
        Task<List<Fondo>> GetFondosDisponiblesAsync();
    }
}