using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using WebClient.Models;
using WebClient.Services.Interfaces;

namespace WebClient.Services
{
    public class SymptomService : BaseService, ISymptomService
    {
        private readonly string _url;

        public SymptomService(IConfiguration configuration) : base(configuration)
        {
            _url = configuration["Api:Symptoms"];
        }

        public async Task<List<Symptom>> GetAllAsync() => await GetAsync<List<Symptom>>(_url);

        public async Task<List<Symptom>> GetAllAsync(Dictionary<string, string> query) => await GetAsync<List<Symptom>>(Queryable(_url, query));

        public async Task<Symptom> GetByIdAsync(int id) => await GetAsync<Symptom>($"{_url}/{id}");

        public async Task<Symptom> AddAsync(Symptom entity) => await PostAsync<Symptom>(_url, entity);

        public async Task<Symptom> UpdateAsync(Symptom entity) => await PutAsync<Symptom>($"{_url}/{entity.SymptomId}", entity);

        public async Task DeleteAsync(int id) => await DeleteAsync($"{_url}/{id}");
        public async Task<List<Symptom>> GetTopAsync() => await GetAsync<List<Symptom>>($"{_url}/top");

        public async Task<int> GetCountAsync() => await GetAsync<int>($"{_url}/count");
    }
}