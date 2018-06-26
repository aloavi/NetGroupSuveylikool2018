using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Interfaces;
using DAL.App.Interfaces;

namespace BLL.Services
{
    public class SymptomService : ISymptomService
    {
        private readonly IAppUnitOfWork _uow;

        public SymptomService(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task<int> SymptomCountAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<SymptomDTO>> GetTopSymptomsAsync(int ammount = 3)
        {
            throw new System.NotImplementedException();
        }
    }
}