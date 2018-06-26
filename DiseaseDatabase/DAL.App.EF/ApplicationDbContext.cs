using DAL.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class ApplicationDbContext : DbContext, IDataContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Disease> Diseases { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }
        public DbSet<DiseaseSymptom> DiseaseSymptoms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
