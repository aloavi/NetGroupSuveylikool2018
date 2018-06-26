using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> AllAsync();
        Task<TEntity> FindAsync(params object[] id); // Find(1) or Find(1,2,3,...) TODO Is params overkill?
        Task AddAsync(TEntity entity);
        TEntity Update(TEntity entity);
        void Remove(TEntity entity);
        Task RemoveAsync(params object[] id);
        Task<int> CountAsync();
        void Clear();
        Task<bool> ExistsAsync(int id);
    }
}
