using amaris.Core.DTOs.Response;
using amaris.Core.Entities;
using amaris.Core.Interfaces.Repositories;
using amaris.Infrastructure.Data;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Commons.Paging;
using Commons.Response;
using Commos.Repository.Repository;
using System.Text.Json;

namespace amaris.Infrastructure.Repositories
{
    public class TransaccionRepository : GenericRepository<Transaccion>, ITransaccionRepository
    {
        private readonly AmazonDynamoDBClient _client;
        private readonly string _tableName = "Transacciones";

        public TransaccionRepository(DynamoDbContext context)
            : base(context.Client, "Transacciones")
        {
            _client = context.Client;
        }

        public async Task<List<Transaccion>> GetByClienteIdAsync(string clienteId)
        {
            var table = Table.LoadTable(_client, _tableName);
            var filter = new ScanFilter();
            filter.AddCondition("ClienteId", ScanOperator.Equal, clienteId);

            var search = table.Scan(filter);
            var documents = await search.GetNextSetAsync();

            return documents.Select(d => JsonSerializer.Deserialize<Transaccion>(d.ToJson())!).ToList();
        }

        public async Task<List<Transaccion>> GetByFondoIdAsync(string fondoId)
        {
            var table = Table.LoadTable(_client, _tableName);
            var filter = new ScanFilter();
            filter.AddCondition("FondoId", ScanOperator.Equal, fondoId);

            var search = table.Scan(filter);
            var documents = await search.GetNextSetAsync();

            return documents.Select(d => JsonSerializer.Deserialize<Transaccion>(d.ToJson())!).ToList();
        }

        public async Task<List<Transaccion>> GetHistorialTransaccionesAsync(string clienteId)
        {
            // Cargar la tabla de DynamoDB
            var table = Table.LoadTable(_client, _tableName);

            // Crear el filtro para buscar por ClienteId
            var filter = new ScanFilter();
            filter.AddCondition("IdCliente", ScanOperator.Equal, clienteId);

            // Realizar la búsqueda en la tabla con el filtro
            var search = table.Scan(filter);

            // Obtener los documentos de la búsqueda
            var documents = await search.GetNextSetAsync();

            // Deserializar los documentos a una lista de transacciones
            var transacciones = documents.Select(doc => JsonSerializer.Deserialize<Transaccion>(doc.ToJson())).ToList();

            return transacciones;
        }

        public async Task<RecordsResponse<TransaccionResponse>> GetTransaccionPaged(int page, int take)
        {
            var table = Table.LoadTable(_client, _tableName);

            return await PagingExtension.GetPagedAsync(
                table,
                page,
                take,
                doc => JsonSerializer.Deserialize<TransaccionResponse>(doc.ToJson()));
        }
    }
}
