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
    public interface IEmployeeService
    {
        Task<Employee> GetById(int id);
        Task<ApiResponse<EmployeeResponseDTO>> Add(EmployeeRequestDTOCreate request);
        Task<ApiResponse<EmployeeResponseDTO>> Update(EmployeeRequestDTOUpdate request);
        Task<ApiResponse<object>> Delete(int id);
        Task<RecordsResponse<EmployeeResponseDTO>> Get(QueryFilter filter);
    }
}
