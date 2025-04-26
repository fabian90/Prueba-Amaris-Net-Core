using Commons.Repository.Entities;
using Commons.Response;
using System.Linq.Expressions;

namespace Commons.Repository.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> Add(TEntity entity);
        Task AddRange(IEnumerable<TEntity> entities);
        Task Delete(string id);
        Task DeleteRange(IEnumerable<TEntity> entities);
        Task Delete(Expression<Func<TEntity, bool>> predicate);
        Task Delete(TEntity entity);
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(string id);
        TEntity GetByKey(params object[] primaryKeys);
        Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetFilter(Expression<Func<TEntity, bool>> predicate);
        Task<bool> Exists(Expression<Func<TEntity, bool>> predicate);
        Task Update(TEntity entity);
        Task UpdateRange(IEnumerable<TEntity> entities);
        Task UpdateProperties(TEntity entity, params Expression<Func<TEntity, object>>[] properties);
        void Dispose();
    }
}
