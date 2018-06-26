using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IInfoService
    {
        Task<List<DiseaseDTO>> GetTopDiseasesAsync(int amount = 3);
        Task<int> SymptomCountAsync();
        Task<List<SymptomDTO>> GetTopSymptomsAsync(int ammount = 3);
    }
}