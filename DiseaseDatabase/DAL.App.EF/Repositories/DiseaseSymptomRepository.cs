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
    }
}