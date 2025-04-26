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
    public interface ITransaccionRepository : IGenericRepository<Transaccion>
    {
        Task<List<Transaccion>> GetByClienteIdAsync(string clienteId);
        Task<List<Transaccion>> GetByFondoIdAsync(string fondoId);
        Task<List<Transaccion>> GetHistorialTransaccionesAsync(string clienteId);
        Task<RecordsResponse<TransaccionResponse>> GetTransaccionPaged(int page, int take);
    }
}
