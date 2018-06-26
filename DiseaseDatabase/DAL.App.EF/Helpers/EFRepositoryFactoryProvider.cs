using System;
using System.Collections.Generic;
using System.Text;
using DAL.App.EF.Repositories;
using DAL.App.Interfaces.Repositories;
using DAL.EF;
using DAL.Interfaces;
using DAL.Interfaces.Helpers;

namespace DAL.App.EF.Helpers
{
    public class EFRepositoryFactoryProvider : IRepositoryFactoryProvider
    {
        private static readonly Dictionary<Type, Func<IDataContext, object>> _customRepositoryFactories = GetCustomRepoFactories();

        public Func<IDataContext, object> GetFactoryForStandarRepo<TEntity>() where TEntity : class
        {
            return (context) => new EFRepository<TEntity>(context as ApplicationDbContext);
        }

        public Func<IDataContext, object> GetFactoryForCustomRepo<TRepositoryInterface>() where TRepositoryInterface : class
        {

            _customRepositoryFactories.TryGetValue(typeof(TRepositoryInterface), out var factory);
            return factory;
        }

        private static Dictionary<Type, Func<IDataContext, object>> GetCustomRepoFactories()
        {
            return new Dictionary<Type, Func<IDataContext, object>>()
            {
                { typeof(IDiseaseRepository), (dataContext) =>  new DiseaseRepository(dataContext as ApplicationDbContext)},
                { typeof(ISymptomRepository), (dataContext) =>  new SymptomRepository(dataContext as ApplicationDbContext)},
                { typeof(IDiseaseSymptomRepository), (dataContext) =>  new DiseaseSymptomRepository(dataContext as ApplicationDbContext)},
            };
        }

    }
}
