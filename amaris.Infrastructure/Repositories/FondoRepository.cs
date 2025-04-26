using amaris.Core.DTOs.Response;
using amaris.Core.Entities;
using amaris.Core.Interfaces.Repositories;
using amaris.Infrastructure.Data;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Commons.Paging;
using Commons.Repository.Repository;
using Commons.Response;
using System.Text.Json;

namespace amaris.Infrastructure.Repositories
{
    public class FondoRepository : GenericRepository<Fondo>, IFondoRepository
    {
        private readonly AmazonDynamoDBClient _client;
        private readonly string _tableName = "Fondos";

        public FondoRepository(DynamoDbContext context)
            : base(context.Client, "Fondos")
        {
            _client = context.Client;
        }

        public async Task<RecordsResponse<FondoResponse>> GetFondosPaged(int page, int take)
        {
            var table = Table.LoadTable(_client, _tableName);

            return await PagingExtension.GetPagedAsync(
                table,
                page,
                take,
                doc => JsonSerializer.Deserialize<FondoResponse>(doc.ToJson()));
        }
    }
}