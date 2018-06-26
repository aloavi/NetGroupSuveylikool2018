using System.Threading.Tasks;
using DAL.Interfaces.Repositories;
using Domain;

namespace DAL.App.Interfaces.Repositories
{
    public interface IDiseaseRepository : IRepository<Disease>
    {
        Task<Disease> FindByNameAsync(string name);
    }
}