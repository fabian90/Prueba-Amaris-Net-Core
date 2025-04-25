using Commons.Repository.Entities;
using System.Linq.Expressions;

namespace Commons.Repository.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        Task Delete(string id);
        void DeleteRange(IEnumerable<TEntity> entities);
        void Delete(Expression<Func<TEntity, bool>> predicate);
        Task Delete(TEntity entity);
        IEnumerable<TEntity> GetAll();
        Task<TEntity> GetById(string id);
        TEntity GetByKey(params object[] primaryKeys);
        TEntity Get(Expression<Func<TEntity, bool>> predicate);
        List<TEntity> GetFilter(Expression<Func<TEntity, bool>> predicate);
        bool Exists(Expression<Func<TEntity, bool>> predicate);
        Task Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void UpdateProperties(TEntity entity, params Expression<Func<TEntity, object>>[] properties);
        void Dispose();
    }
}