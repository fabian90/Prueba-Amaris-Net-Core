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

        public async Task<List<Fondo>> GetFondosDisponiblesAsync()
        {
            // Cargar la tabla de DynamoDB
            var table = Table.LoadTable(_client, _tableName);

            // Crear un filtro vacío para escanear toda la tabla
            var scanFilter = new ScanFilter();

            // Realizar el escaneo con el filtro
            var search = table.Scan(scanFilter);

            // Obtener los resultados
            var documentos = await search.GetRemainingAsync();

            // Filtrar los documentos para obtener solo los fondos activos
            var fondosDisponibles = documentos
                .Select(doc => JsonSerializer.Deserialize<Fondo>(doc.ToJson()))
                .Where(f => f != null && f.Activo == true)  // Filtrar en memoria los fondos activos
                .ToList();

            return fondosDisponibles;
        }
    }
}