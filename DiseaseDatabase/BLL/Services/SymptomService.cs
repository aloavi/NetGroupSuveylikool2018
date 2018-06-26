using System.Collections.Generic;
using System.Linq;
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

        public async Task<int> SymptomCountAsync()
        {
            return await _uow.Symptoms.CountAsync();
        }

        public async Task<List<SymptomDTO>> GetTopSymptomsAsync(int ammount = 3)
        {
            var syptom = await _uow.Symptoms.GetTopSymptomsAsync(3);
            return syptom.Select(SymptomDTO.CreateFromDomain).ToList();
        }
    }
}