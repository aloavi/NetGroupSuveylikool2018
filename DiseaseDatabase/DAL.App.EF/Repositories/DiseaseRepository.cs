using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.Interfaces.Repositories;
using DAL.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class DiseaseRepository : EFRepository<Disease>, IDiseaseRepository
    {
        public DiseaseRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Disease> FindByNameAsync(string name)
        {
            return await RepositoryDbSet.FirstOrDefaultAsync(d => d.DiseaseName == name);
        }

        public async Task<List<Disease>> GetTopDiseasesAsync(int take)
        {
            return await RepositoryDbSet
                .OrderByDescending(d => d.Symptoms.Count)
                .ThenBy(d => d.DiseaseName)
                .Take(take)
                .ToListAsync();
        }

        public async Task<List<Disease>> GetBySymptomsAsync(List<Symptom> symptoms)
        {
            var diseases = RepositoryDbSet.Include(d=>d.Symptoms).ThenInclude(s => s.Symptom).AsQueryable();

            foreach (var symptom in symptoms)
            {
                diseases = diseases.Where(d => d.Symptoms.Any(s => s.SymptomId == symptom.SymptomId));
            }

            var ret = await diseases
                .OrderBy(d => d.Symptoms.Count)
                .ThenBy(d => d.DiseaseName)
                .ToListAsync();

            foreach (var disease in ret)
            {
                disease.Symptoms = disease.Symptoms.OrderBy(s => s.Symptom.SymptomName).ToList();
            }

            return ret;
        }

        public async Task<List<Disease>> GetDiseasesWithSymptoms()
        {
            return await RepositoryDbSet
                .Include(d=> d.Symptoms)
                .ThenInclude(s => s.Symptom)
                .ToListAsync();
        }
    }
}