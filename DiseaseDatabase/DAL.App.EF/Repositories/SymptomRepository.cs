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
    }
}