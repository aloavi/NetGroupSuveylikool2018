using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Interfaces;
using DAL.App.Interfaces;

namespace BLL.Services
{
    public class DiseaseService : IDiseaseService
    {
        private readonly IAppUnitOfWork _uow;

        public DiseaseService(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task<List<DiseaseDTO>> GetTopDiseasesAsync(int amount = 3)
        {
            throw new System.NotImplementedException();
        }
    }
}