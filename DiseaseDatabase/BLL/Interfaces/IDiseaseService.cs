using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IDiseaseService : IBaseService<DiseaseDTO>
    {
        Task<List<DiseaseDTO>> GetTopDiseasesAsync(int take = 3);
    }
}