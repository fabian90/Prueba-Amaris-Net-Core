using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using amaris.Core.Entities;
using Commons.Repository.Interfaces;

namespace amaris.Core.Interfaces.Repositories
{
    public interface IClienteRepository : IGenericRepository<Cliente>
    {
        Task<Cliente?> GetByDocumentoAsync(string documento);
    }
}
