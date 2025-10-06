namespace ProjetoHospital
{
    using Microsoft.EntityFrameworkCore;
    using System.Linq.Expressions;

    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
         where TEntity : class
    {
        protected readonly ProjetoHospitalContext Context;

        public GenericRepository(ProjetoHospitalContext context)
        {
            this.Context = context;
        }

        protected DbSet<TEntity> DbSet
        {
            get
            {
                return this.Context.Set<TEntity>();
            }
        }

        public async Task<TEntity> InsertAsync(TEntity model)
        {
            this.DbSet.Add(model);
            await this.SaveAsync();

            return model;
        }

        public async Task<bool> UpdateAsync(TEntity model)
        {
            var entry = this.Context.Entry(model);

            this.DbSet.Attach(model);

            entry.State = EntityState.Modified;

            return await this.SaveAsync() > 0;
        }

        public async Task<bool> DeleteAsync(TEntity model)
        {
            var entry = this.Context.Entry(model);

            this.DbSet.Attach(model);

            entry.State = EntityState.Deleted;

            return await this.SaveAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> where)
        {
            var model = this.DbSet.Where<TEntity>(where).FirstOrDefault<TEntity>();

            return (model != null) && await this.DeleteAsync(model);
        }

        public async Task<bool> DeleteAsync(params object[] keys)
        {
            var model = this.DbSet.Find(keys);
            return (model != null) && await this.DeleteAsync(model);
        }

        public async Task<IList<TEntity>> InsertRangeAsync(
            IList<TEntity> model)
        {
            this.DbSet.AddRange(model);
            await this.SaveAsync();

            return model;
        }

        public async Task<bool> UpdateRangeAsync(
            IEnumerable<TEntity> model)
        {
            foreach (var data in model)
            {
                var entry = this.Context.Entry(data);
                this.DbSet.Attach(data);
                entry.State = EntityState.Modified;
            }

            return (await this.SaveAsync()) > 0;
        }

        public async Task<TEntity> FindAsync(params object[] keys)
        {
            return await this.DbSet.FindAsync(keys);
        }

        public async Task<TEntity> FindAsync(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            if (includeProperties != null && includeProperties.Length != 0)
            {
                var query = this.DbSet.Include(includeProperties.First());

                foreach (var property in includeProperties.Skip(1))
                {
                    query = query.Include(property);
                }

                return await query
                    .SingleOrDefaultAsync(predicate)
                    .ConfigureAwait(false);
            }

            return await this.DbSet
                .SingleOrDefaultAsync(predicate)
                .ConfigureAwait(false);
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> where)
        {
            return await this.DbSet.Where<TEntity>(where).FirstOrDefaultAsync<TEntity>();
        }

        public async Task<TDerived?> FindDerivedAsync<TDerived>(
            Expression<Func<TDerived, bool>> predicate,
            params Expression<Func<TDerived, object>>[] includeProperties)
            where TDerived : class, TEntity
        {
            IQueryable<TDerived> query = this.DbSet.OfType<TDerived>();

            if (includeProperties != null && includeProperties.Length > 0)
            {
                query = query.Include(includeProperties.First());

                foreach (var include in includeProperties.Skip(1))
                {
                    query = query.Include(include);
                }
            }

            return await query.SingleOrDefaultAsync(predicate)
                              .ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var data = this.DbSet
                .Where(predicate)
                .AsQueryable();

            if (includes != null && includes.Any())
            {
                foreach (var include in includes)
                {
                    data = data.Include(include);
                }
            }

            return await data
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> where)
        {
            return await this.DbSet.Where<TEntity>(where).ToListAsync();
        }

        public async Task<List<TDerived>> FindAllDerivedAsync<TDerived>(
            Expression<Func<TDerived, bool>> predicate,
            params Expression<Func<TDerived, object>>[] includeProperties)
            where TDerived : class, TEntity
        {
            IQueryable<TDerived> query = this.DbSet.OfType<TDerived>();

            if (includeProperties != null
                && includeProperties.Length > 0)
            {
                query = query.Include(includeProperties.First());

                foreach (var include in includeProperties.Skip(1))
                {
                    query = query.Include(include);
                }
            }

            return await query
                .Where(predicate)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public IQueryable<TEntity> Get()
        {
            return this.DbSet;
        }

        public IQueryable<TEntity> Get(params Expression<Func<TEntity, object>>[] includes)
        {
            return includes.Aggregate(
                this.Get(),
                (current, expression) => current.Include(expression));
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where)
        {
            var any = await this.DbSet
                .Where<TEntity>(where)
                .AnyAsync()
                .ConfigureAwait(false);

            return any;
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> where)
        {
            var total = await this.DbSet
                .Where<TEntity>(where)
                .CountAsync()
                .ConfigureAwait(false);

            return total;
        }

        public async Task<int> SaveAsync()
        {
            return await this.Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            this.Dispose();
        }
    }
}