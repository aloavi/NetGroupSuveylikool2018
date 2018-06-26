using DAL.App.Interfaces.Repositories;
using DAL.Interfaces;

namespace DAL.App.Interfaces
{
    public interface IAppUnitOfWork : IUnitOfWork
    {
        IDiseaseRepository Diseases { get; }
        ISymptomRepository Symptoms { get; }
    }
}
