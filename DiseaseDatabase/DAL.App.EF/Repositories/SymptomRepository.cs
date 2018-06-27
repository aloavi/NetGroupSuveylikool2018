using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.Interfaces.Repositories;
using DAL.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class SymptomRepository : EFRepository<Symptom>, ISymptomRepository
    {
        public SymptomRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Symptom> FindByNameAsync(string name)
        {
            return await RepositoryDbSet.FirstOrDefaultAsync(s => s.SymptomName == name);

        }

        public async Task<List<Symptom>> GetTopSymptomsAsync(int take)
        {
            return await RepositoryDbSet
                .OrderByDescending(s=>s.Diseases.Count)
                .ThenBy(d => d.SymptomName)
                .Take(take)
                .ToListAsync();
        }

        public async Task<List<Symptom>> GetSymptomsWithDiseases()
        {
            return await RepositoryDbSet
                .Include(s=>s.Diseases)
                .ThenInclude(s=>s.Disease)
                .ToListAsync();
        }
    }
}