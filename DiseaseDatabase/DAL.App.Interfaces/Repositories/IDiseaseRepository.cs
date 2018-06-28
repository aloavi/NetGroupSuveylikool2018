using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Interfaces.Repositories;
using Domain;

namespace DAL.App.Interfaces.Repositories
{
    public interface IDiseaseRepository : IRepository<Disease>
    {
        Task<Disease> FindByNameAsync(string name);
        Task<List<Disease>> GetTopDiseasesAsync(int take);
        Task<List<Disease>> GetBySymptomsAsync(List<Symptom> symptoms);
        Task<List<Disease>> GetDiseasesWithSymptomsAsync();
        Task<Disease> GetByIdWithSymptomsAsync(int id);
    }
}