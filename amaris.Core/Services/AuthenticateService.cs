
using amaris.Core.Interfaces.Repositories;
using ifxNetworks.Core.DTOs.Response;
using ifxNetworks.Core.Interfaces.Services;
using ifxNetworks.Core.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Core.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordService _passwordService;
        private readonly TokenOptions _options;

        public AuthenticateService(IUnitOfWork unitOfWork, IPasswordService passwordService, IOptions<TokenOptions> options)
        {
            _unitOfWork = unitOfWork;
            _passwordService = passwordService;
            _options = options.Value;
        }

        public UserAuthResponseDTO ValidateUser(string username, string password)
        {
            UserAuthResponseDTO response = new();
            string responseToken = string.Empty;

            var user = _unitOfWork.AuthenticateRepository.GetFilter(x => x.UserName == username).FirstOrDefault();
            
            if (user.Id > 0)
            {
                var result = _passwordService.Check(user.Password, password);
                var roles = new List<string>() { "Admin" };
                if (result == true)
                {
                    responseToken = this.GetToken(user.Id, roles);
                }

                response.IdRole = 1;
                response.Username = user.UserName;
                response.FullName = user.FirstName + " " + user.LastName;
                response.Token = responseToken;
            }

            return response;
        }

        private string GetToken(int idUser, List<string> roles)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _options.Subject),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                new Claim("IdUser", idUser.ToString()),
                new Claim("IdRol",  string.Join(",", roles)),
                new Claim(ClaimTypes.Role,  string.Join(",", roles))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _options.Issuer,
                _options.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_options.TokenExpirationTime)),
                signingCredentials: signingCredentials
                );
            
            string responseToken = new JwtSecurityTokenHandler().WriteToken(token);
            return responseToken;
        }
    }
}
