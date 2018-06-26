using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<DiseaseDTO>> GetTopDiseasesAsync(int amount = 3)
        {
            var diseases = await _uow.Diseases.GetTopDiseasesAsync(3);
            return diseases.Select(DiseaseDTO.CreateFromDomain).ToList();
        }
    }
}