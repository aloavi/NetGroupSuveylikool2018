using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext RepositoryDbContext;
        protected readonly DbSet<TEntity> RepositoryDbSet;

        public EFRepository(DbContext dbContext)
        {
            RepositoryDbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

            RepositoryDbSet = RepositoryDbContext.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> AllAsync()
        {
            return await RepositoryDbSet.ToListAsync();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await RepositoryDbSet.AddAsync(entity);
        }

        public virtual TEntity Update(TEntity entity)
        {
            return RepositoryDbSet.Update(entity).Entity;
        }

        public virtual void Remove(TEntity entity)
        {
            RepositoryDbSet.Remove(entity);
        }


        public virtual async Task RemoveAsync(params object[] id)
        {
            Remove(await FindAsync(id));
        }

        public async Task<int> CountAsync()
        {
            return await RepositoryDbSet.CountAsync();
        }

        public void Clear()
        {
            RepositoryDbSet.RemoveRange(RepositoryDbSet);
        }

        public virtual async Task<TEntity> FindAsync(params Object[] id)
        {
            return await RepositoryDbSet.FindAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await FindAsync(id) != null;
        }
    }
}
