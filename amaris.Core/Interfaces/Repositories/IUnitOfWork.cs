namespace amaris.Core.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IFondoRepository FondoRepository { get; }
        IClienteRepository ClienteRepository { get; }
        ITransaccionRepository TransaccionRepository { get; }

        void SaveChanges();

        Task SaveChangesAsync();
    }
}
