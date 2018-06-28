using System.Threading.Tasks;
using DAL.Interfaces.Repositories;
using Domain;

namespace DAL.App.Interfaces.Repositories
{
    public interface IDiseaseSymptomRepository : IRepository<DiseaseSymptom>
    {
        Task RemoveByDiseaseAsync(int id);
    }
}