
using ifxNetworks.Core.DTOs.Response;

namespace ifxNetworks.Core.Interfaces.Services
{
    public interface IAuthenticateService
    {
        UserAuthResponseDTO ValidateUser(string username, string password);
    }
}
