using System.Collections.Generic;
using System.Threading.Tasks;
using WebClient.Models;

namespace WebClient.Services.Interfaces
{
    public interface IDiseaseService : IService<Disease>
    {
        Task<List<Disease>> GetTopAsync();
        Task PostCsvAsync(string[] csv);
    }
}