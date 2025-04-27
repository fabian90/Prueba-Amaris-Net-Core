using amaris.Core.Interfaces.Repositories;
using amaris.Core.Interfaces.Services;
using amaris.Core.Services;
using amaris.Infrastructure.repositories;
using amaris.Infrastructure.Repositories;

namespace amaris.Api.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            #region Services
            services.AddScoped<IFondoService, FondoService>();
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<ITransaccionService, TransaccionService>();
            #endregion

            #region Repositories
            // Asegúrate de registrar los repositorios
            services.AddScoped<IFondoRepository, FondoRepository>();  // Registra el repositorio de Fondo
            services.AddScoped<IClienteRepository, ClienteRepository>();  // Registra el repositorio de Cliente
            services.AddScoped<ITransaccionRepository, TransaccionRepository>();  // Registra el repositorio de Transacción

            // Registra UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion
        }
    }
}
