using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Interfaces;
using DAL.App.Interfaces;

namespace BLL.Services
{
    public class DiagnoseService : IDiagnoseService
    {
        private readonly IAppUnitOfWork _uow;

        public DiagnoseService(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task<List<DiseaseDTO>> DiagnoseAsync(List<SymptomDTO> symptoms)
        {
            throw new System.NotImplementedException();
        }

        public Task<SymptomDTO> DiagnoseInteractiveAsync(List<SymptomDTO> symptoms)
        {
            throw new System.NotImplementedException();
        }
    }
}