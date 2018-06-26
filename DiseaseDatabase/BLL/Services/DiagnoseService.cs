using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Interfaces;

namespace BLL.Services
{
    public class DiagnoseService : IDiagnoseService
    {
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