using System.Collections.Generic;
using System.Threading.Tasks;
using WebClient.Models;

namespace WebClient.Services.Interfaces
{
    public interface ISymptomService : IService<Symptom>
    {
        Task<List<Symptom>> GetTopAsync();
        Task<int> GetCountAsync();
    }
}