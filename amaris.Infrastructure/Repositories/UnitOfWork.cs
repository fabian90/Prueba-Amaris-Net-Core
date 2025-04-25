using amaris.Core.Interfaces.Repositories;
using amaris.Infrastructure.Data;
using Commons.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amaris.Infrastructure.repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DynamoDbContext _context;

        public IFondoRepository Fondos { get; }
        public IClienteRepository Clientes { get; }
        public ITransaccionRepository Transacciones { get; }

        public UnitOfWork(DynamoDbContext context,
                          IFondoRepository fondoRepository,
                          IClienteRepository clienteRepository,
                          ITransaccionRepository transaccionRepository)
        {
            _context = context;
            Fondos = fondoRepository;
            Clientes = clienteRepository;
            Transacciones = transaccionRepository;
        }

        public void SaveChanges()
        {
            // En DynamoDB no se requiere una operación de SaveChanges como en EF
        }

        public async Task SaveChangesAsync()
        {
            // En DynamoDB se pueden hacer operaciones asincrónicas individualmente
            await Task.CompletedTask;
        }

        public void Dispose()
        {
            // Liberar recursos si es necesario (por ejemplo, cerrar conexiones)
        }
    }
}
    
