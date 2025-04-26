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
    public interface IClienteRepository : IGenericRepository<Cliente>
    {
        Task<Cliente?> GetByDocumentoAsync(string documento);
        Task<RecordsResponse<ClienteResponse>> GetClientePaged(int page, int take);
    }
}
