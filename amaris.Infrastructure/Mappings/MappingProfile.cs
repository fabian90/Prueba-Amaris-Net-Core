using AutoMapper;
using amaris.Core.DTOs.Request;
using amaris.Core.DTOs.Response;
using amaris.Core.Entities;

namespace amaris.Infrastructure.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Clientes
            CreateMap<Cliente,ClienteRequest>();
            CreateMap<Cliente, ClienteResponse>();
            CreateMap<ClienteRequest, Cliente>();
            CreateMap<ClienteResponse, Cliente>();
            #endregion
            #region Fondos
            CreateMap<Fondo, FondoResponse>();
            CreateMap<FondoRequest, Fondo>();
            CreateMap<FondoResponse, Fondo>();
            #endregion
            #region Transaccion
            CreateMap<Transaccion, TransaccionRequest>();
            CreateMap<Transaccion, TransaccionResponse>();
            CreateMap<TransaccionRequest, Transaccion>();
            CreateMap<TransaccionResponse, Transaccion>();
            #endregion
        }
    }
}
