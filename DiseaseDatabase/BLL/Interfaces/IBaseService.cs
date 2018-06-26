using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IBaseService<TDTO> where TDTO : class
    {
        Task<List<TDTO>> GetAllAsync();
        Task<TDTO> GetByIdAsync(int id);
        Task<TDTO> AddAsync(TDTO dto);
        Task<TDTO> Update(TDTO dto);
        Task<TDTO> Remove(int id);
        Task<bool> ExistsAsync(int id);
    }
}