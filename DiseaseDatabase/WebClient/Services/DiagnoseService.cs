using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebClient.Models;
using WebClient.Services.Interfaces;

namespace WebClient.Services
{
    public class DiagnoseService : BaseService, IDiagnoseService
    {
        private readonly string _url;

        public DiagnoseService(IConfiguration configuration) : base(configuration)
        {
            _url = configuration["Api:Diagnose"];
        }


        public async Task<List<Disease>> DiagnoseBySymptomsAsync(List<Symptom> symptoms) => await PostAsync<List<Disease>>(_url, symptoms);
    }
}