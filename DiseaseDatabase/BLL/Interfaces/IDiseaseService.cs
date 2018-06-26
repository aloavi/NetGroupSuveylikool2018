using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IDiseaseService
    {
        Task<List<DiseaseDTO>> GetTopDiseasesAsync(int amount = 3);
    }
}