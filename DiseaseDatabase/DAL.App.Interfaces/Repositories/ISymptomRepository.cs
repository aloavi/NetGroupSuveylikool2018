using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Interfaces.Repositories;
using Domain;

namespace DAL.App.Interfaces.Repositories
{
    public interface ISymptomRepository : IRepository<Symptom>
    {
        Task<Symptom> FindByNameAsync(string name);
        Task<List<Symptom>> GetTopSymptomsAsync(int take);
        Task<List<Symptom>> GetSymptomsWithDiseases();
    }
}