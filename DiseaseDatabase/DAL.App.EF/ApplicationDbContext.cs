using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class ApplicationDbContext : DbContext, IDataContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Domain.Disease> Diseases { get; set; }
        public DbSet<Domain.Symptom> Symptoms { get; set; }
        public DbSet<Domain.DiseaseSymptom> DiseaseSymptoms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
