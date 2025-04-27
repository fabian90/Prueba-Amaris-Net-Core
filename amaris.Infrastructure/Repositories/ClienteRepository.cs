using amaris.Core.DTOs.Response;
using amaris.Core.Entities;
using amaris.Core.Interfaces.Repositories;
using amaris.Infrastructure.Data;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Commons.Response;
using Commos.Repository.Repository;
using System.Text.Json;

namespace amaris.Infrastructure.Repositories
{
    public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
    {
        private readonly AmazonDynamoDBClient _client;
        private readonly string _tableName = "Clientes";

        public ClienteRepository(DynamoDbContext context)
            : base(context.Client, "Clientes")
        {
            _client = context.Client;
        }

        public async Task<Cliente?> GetByDocumentoAsync(string documento)
        {
            var table = Table.LoadTable(_client, _tableName);
            var filter = new ScanFilter();
            filter.AddCondition("Documento", ScanOperator.Equal, documento);

            var search = table.Scan(filter);
            var documents = await search.GetNextSetAsync();

            var doc = documents.FirstOrDefault();
            return doc != null ? JsonSerializer.Deserialize<Cliente>(doc.ToJson()) : null;
        }

        public async Task<RecordsResponse<ClienteResponse>> GetClientePaged(int page, int take)
        {
            var fondosTable = Table.LoadTable(_client, "Clientes");

            return await Commons.Paging.PagingExtension.GetPagedAsync(
                fondosTable,
                page,
                take,
                doc => JsonSerializer.Deserialize<ClienteResponse>(doc.ToJson()));
        }
    }
}