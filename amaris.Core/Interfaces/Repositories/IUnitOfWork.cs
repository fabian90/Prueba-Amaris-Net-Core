namespace amaris.Core.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IFondoRepository Fondos { get; }
        IClienteRepository Clientes { get; }
        ITransaccionRepository Transacciones { get; }

        void SaveChanges();

        Task SaveChangesAsync();
    }
}
