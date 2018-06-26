using System;
using System.Threading.Tasks;
using DAL.App.Interfaces;
using DAL.App.Interfaces.Repositories;
using DAL.Interfaces;
using DAL.Interfaces.Helpers;
using DAL.Interfaces.Repositories;

namespace DAL.App.EF
{
    public class AppUnitOfWork : IAppUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IRepositoryProvider _repositoryProvider;

        public AppUnitOfWork(IDataContext dbContext, IRepositoryProvider repositoryProvider)
        {
            _dbContext = dbContext as ApplicationDbContext;
            if (_dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _repositoryProvider = repositoryProvider;
        }

        public IDiseaseRepository Diseases =>
            GetCustomRepository<IDiseaseRepository>();
        public ISymptomRepository Symptoms =>
            GetCustomRepository<ISymptomRepository>();


        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public IRepository<TEntity> GetEntityRepository<TEntity>() where TEntity : class
        {
            return _repositoryProvider
                .ProvideEntityRepository<TEntity>();
        }

        public TRepositoryInterface GetCustomRepository<TRepositoryInterface>() where TRepositoryInterface : class
        {
            return _repositoryProvider
                .ProvideCustomRepository<TRepositoryInterface>();
        }
    }
}
