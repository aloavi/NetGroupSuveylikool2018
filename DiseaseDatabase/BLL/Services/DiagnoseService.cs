using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Interfaces;
using DAL.App.Interfaces;
using Domain;

namespace BLL.Services
{
    public class DiagnoseService : IDiagnoseService
    {
        private readonly IAppUnitOfWork _uow;

        public DiagnoseService(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<DiseaseDTO>> DiagnoseAsync(List<SymptomDTO> symptoms)
        {
            List<Disease> diseases = await _uow.Diseases.GetBySymptomsAsync(symptoms
                .Select(SymptomDTO.CreateFromDTO)
                .ToList());

            return diseases.Select(DiseaseDTO.CreateFromDomainWithDiseases).ToList();
        }

        public Task<SymptomDTO> DiagnoseInteractiveAsync(List<SymptomDTO> symptoms)
        {
            throw new System.NotImplementedException();
        }
    }
}