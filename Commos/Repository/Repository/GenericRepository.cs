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

        public void AddRange(IEnumerable<TEntity> entities)
        {
            var table = Table.LoadTable(_client, _tableName);
            var batch = table.CreateBatchWrite();
            foreach (var entity in entities)
            {
                var doc = Document.FromJson(JsonSerializer.Serialize(entity));
                batch.AddDocumentToPut(doc);
            }
            batch.ExecuteAsync().Wait(); // Podrías hacer este método async también
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

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            var table = Table.LoadTable(_client, _tableName);
            var batch = table.CreateBatchWrite();
            foreach (var entity in entities)
            {
                batch.AddKeyToDelete(entity.Id);
            }
            batch.ExecuteAsync().Wait();
        }

        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException("Se requiere un escaneo completo de la tabla. No recomendado en producción.");
        }

        public IEnumerable<TEntity> GetAll()
        {
            throw new NotImplementedException("Para obtener todos los elementos, se debe usar un escaneo completo.");
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

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException("Consulta con expresión requiere usar Scan con filtros.");
        }

        public List<TEntity> GetFilter(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException("Consulta con filtro requiere usar Scan con filtros.");
        }

        public bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException("No implementado. Requiere escanear la tabla.");
        }

        public async Task Update(TEntity entity)
        {
            var table = Table.LoadTable(_client, _tableName);
            var document = Document.FromJson(JsonSerializer.Serialize(entity));
            await table.PutItemAsync(document);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            var table = Table.LoadTable(_client, _tableName);
            var batch = table.CreateBatchWrite();
            foreach (var entity in entities)
            {
                var doc = Document.FromJson(JsonSerializer.Serialize(entity));
                batch.AddDocumentToPut(doc);
            }
            batch.ExecuteAsync().Wait();
        }

        public void UpdateProperties(TEntity entity, params Expression<Func<TEntity, object>>[] properties)
        {
            throw new NotImplementedException("DynamoDB requiere expresiones de actualización manuales.");
        }

        public void Dispose()
        {
            _client.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
