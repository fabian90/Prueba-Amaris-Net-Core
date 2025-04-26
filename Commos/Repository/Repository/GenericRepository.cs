using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Commons.Repository.Entities;
using Commons.Repository.Interfaces;
using System.Text.Json;
using System.Linq.Expressions;

namespace Commons.Repository.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly AmazonDynamoDBClient _client;
        private readonly string _tableName;

        public GenericRepository(IAmazonDynamoDB client, string tableName)
        {
            _client = (AmazonDynamoDBClient)client;
            _tableName = tableName;
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            var table = Table.LoadTable(_client, _tableName);
            var document = Document.FromJson(JsonSerializer.Serialize(entity));
            await table.PutItemAsync(document);
            return entity;
        }

        public async Task AddRange(IEnumerable<TEntity> entities)
        {
            var table = Table.LoadTable(_client, _tableName);
            var batch = table.CreateBatchWrite();
            foreach (var entity in entities)
            {
                var doc = Document.FromJson(JsonSerializer.Serialize(entity));
                batch.AddDocumentToPut(doc);
            }
            await batch.ExecuteAsync();
        }

        public async Task Delete(string id)
        {
            var table = Table.LoadTable(_client, _tableName);
            await table.DeleteItemAsync(id);
        }

        public async Task Delete(TEntity entity)
        {
            var table = Table.LoadTable(_client, _tableName);
            await table.DeleteItemAsync(entity.Id);
        }

        public async Task DeleteRange(IEnumerable<TEntity> entities)
        {
            var table = Table.LoadTable(_client, _tableName);
            var batch = table.CreateBatchWrite();
            foreach (var entity in entities)
            {
                batch.AddKeyToDelete(entity.Id);
            }
            await batch.ExecuteAsync();
        }

        public Task Delete(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException("Requiere un escaneo completo de la tabla con filtros.");
        }

        public Task<IEnumerable<TEntity>> GetAll()
        {
            throw new NotImplementedException("Se debe implementar con un escaneo completo de DynamoDB.");
        }

        public async Task<TEntity> GetById(string id)
        {
            var table = Table.LoadTable(_client, _tableName);
            var item = await table.GetItemAsync(id);
            return item != null ? JsonSerializer.Deserialize<TEntity>(item.ToJson()) : null;
        }

        public TEntity GetByKey(params object[] primaryKeys)
        {
            if (primaryKeys.Length == 1 && primaryKeys[0] is string id)
            {
                return GetById(id).Result;
            }
            throw new ArgumentException("Incorrect primary key.");
        }

        public Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException("Filtrar con expresiones requiere escanear toda la tabla.");
        }

        public Task<List<TEntity>> GetFilter(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException("No implementado: se requiere un escaneo con filtro.");
        }

        public Task<bool> Exists(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException("Verificar existencia requiere escaneo con filtro.");
        }

        public async Task Update(TEntity entity)
        {
            var table = Table.LoadTable(_client, _tableName);
            var document = Document.FromJson(JsonSerializer.Serialize(entity));
            await table.PutItemAsync(document);
        }

        public async Task UpdateRange(IEnumerable<TEntity> entities)
        {
            var table = Table.LoadTable(_client, _tableName);
            var batch = table.CreateBatchWrite();
            foreach (var entity in entities)
            {
                var doc = Document.FromJson(JsonSerializer.Serialize(entity));
                batch.AddDocumentToPut(doc);
            }
            await batch.ExecuteAsync();
        }

        public Task UpdateProperties(TEntity entity, params Expression<Func<TEntity, object>>[] properties)
        {
            throw new NotImplementedException("DynamoDB no permite updates parciales fácilmente.");
        }

        public void Dispose()
        {
            _client.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
