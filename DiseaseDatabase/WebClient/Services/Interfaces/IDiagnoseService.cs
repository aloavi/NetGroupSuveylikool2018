using System.Collections.Generic;
using System.Threading.Tasks;
using WebClient.Models;
using WebClient.ViewModels;

namespace WebClient.Services.Interfaces
{
    public interface IDiagnoseService
    {
        Task<List<Disease>> DiagnoseBySymptomsAsync(List<Symptom> symptoms);
    }
}