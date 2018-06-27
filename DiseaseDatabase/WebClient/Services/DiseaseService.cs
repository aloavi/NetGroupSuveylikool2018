using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using WebClient.Models;
using WebClient.Services.Interfaces;

namespace WebClient.Services
{
    public class DiseaseService : BaseService, IDiseaseService
    {
        private readonly string _url;
        protected DiseaseService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(configuration, httpContextAccessor)
        {
            _url = configuration["Api:Books"];
        }

        public async Task<List<Disease>> GetAllAsync() => await GetAsync<List<Disease>>(_url);

        public async Task<List<Disease>> GetAllAsync(Dictionary<string, string> query) => await GetAsync<List<Disease>>(Queryable(_url, query));

        public async Task<Disease> GetByIdAsync(int id) => await GetAsync<Disease>($"{_url}/{id}");

        public async Task<Disease> AddAsync(Disease entity) => await PostAsync(_url, entity);

        public async Task<Disease> UpdateAsync(Disease entity) => await PutAsync($"{_url}/{entity.DiseaseId}", entity);

        public async Task DeleteAsync(int id) => await DeleteAsync($"{_url}/{id}");
    }
}