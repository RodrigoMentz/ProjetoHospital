using System.Linq.Expressions;

namespace ProjetoHospital
{
    public interface IGenericRepository<TEntity>
        where TEntity : class
    {
        Task<TEntity> InsertAsync(TEntity model);

        Task<IList<TEntity>> InsertRangeAsync(IList<TEntity> model);

        Task<bool> UpdateAsync(TEntity model);

        Task<bool> UpdateRangeAsync(
            IEnumerable<TEntity> model);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> where);

        Task<bool> DeleteAsync(TEntity model);

        Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> where);

        Task<bool> DeleteAsync(params object[] keys);

        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> where);

        Task<IEnumerable<TEntity>> FindAllAsync(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> FindAsync(params object[] keys);

        Task<TEntity> FindAsync(
           Expression<Func<TEntity, bool>> predicate,
           params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> where);

        IQueryable<TEntity> Get();

        IQueryable<TEntity> Get(params Expression<Func<TEntity, object>>[] includes);

        Task<int> SaveAsync();

        void Dispose();
    }
}