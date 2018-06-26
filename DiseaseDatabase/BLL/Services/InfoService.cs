using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Interfaces;

namespace BLL.Services
{
    public class InfoService : IInfoService
    {
        public Task<List<DiseaseDTO>> GetTopDiseasesAsync(int amount = 3)
        {
            throw new System.NotImplementedException();
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