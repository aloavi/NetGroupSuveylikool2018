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
    }
}