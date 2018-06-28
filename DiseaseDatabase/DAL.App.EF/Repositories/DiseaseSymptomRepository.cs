using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.Interfaces.Repositories;
using DAL.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class DiseaseSymptomRepository : EFRepository<DiseaseSymptom>, IDiseaseSymptomRepository
    {
        public DiseaseSymptomRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task RemoveByDiseaseAsync(int id)
        {
            RepositoryDbSet.RemoveRange(await GetByDiseaseIdAsync(id));
        }

        private async Task<List<DiseaseSymptom>> GetByDiseaseIdAsync(int id)
        {
            return await RepositoryDbSet.Where(d => d.DiseaseId == id).ToListAsync();
        }
    }
}