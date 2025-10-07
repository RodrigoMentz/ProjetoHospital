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

        Task<IEnumerable<TEntity>> FindAllAsync(
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null);

        Task<List<TDerived>> FindAllDerivedAsync<TDerived>(
            Expression<Func<TDerived, bool>> predicate,
            params Expression<Func<TDerived, object>>[] includeProperties)
            where TDerived : class, TEntity;

        Task<TEntity> FindAsync(params object[] keys);

        Task<TEntity> FindAsync(
           Expression<Func<TEntity, bool>> predicate,
           params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> where);

        Task<TDerived?> FindDerivedAsync<TDerived>(
            Expression<Func<TDerived, bool>> predicate,
            params Expression<Func<TDerived, object>>[] includeProperties)
            where TDerived : class, TEntity;

        IQueryable<TEntity> Get();

        IQueryable<TEntity> Get(params Expression<Func<TEntity, object>>[] includes);

        Task<int> SaveAsync();

        void Dispose();
    }
}