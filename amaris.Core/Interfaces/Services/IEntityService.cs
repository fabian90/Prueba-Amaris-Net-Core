using Commons.RequestFilter;
using Commons.Response;
using ifxNetworks.Core.DTOs.Request;
using ifxNetworks.Core.DTOs.Response;
using ifxNetworks.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ifxNetworks.Core.Interfaces.Services
{
    public interface IEntityService
    {
        Task<Entity> GetById(int id);
        Task<ApiResponse<EntityResponseDTO>> Add(EntityRequestDTOCreate request);
        Task<ApiResponse<EntityResponseDTO>> Update(EntityRequestDTOUpdate request);
        Task<ApiResponse<object>> Delete(int id);
        Task<RecordsResponse<EntityResponseDTO>> Get(QueryFilter filter);
    }
}
