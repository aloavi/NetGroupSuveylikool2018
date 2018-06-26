using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IDiagnoseService
    {
        Task<List<DiseaseDTO>> DiagnoseAsync(List<SymptomDTO> symptoms);
        Task<SymptomDTO> DiagnoseInteractiveAsync(List<SymptomDTO> symptoms);
    }
}