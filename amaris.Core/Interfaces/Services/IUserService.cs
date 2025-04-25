using Commons.RequestFilter;
using Commons.Response;
using ifxNetworks.Core.DTOs.Request;
using ifxNetworks.Core.DTOs.Response;
using ifxNetworks.Core.Entities;

namespace ifxNetworks.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<User> GetByUserName(string userName);
        Task<ApiResponse<UserResponseDTO>> Add(UserRequestDTO request);
        Task<ApiResponse<UserResponseDTO>> Update(UserRequestDTO request);
        Task<ApiResponse<object>> Delete(int id);
        Task<RecordsResponse<UserResponseDTO>> Get(QueryFilter filter);
    }
}