using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;

namespace WebClient.Services
{
    public class BaseService
    {
        protected readonly HttpClient Client;

        protected BaseService(IConfiguration configuration)
        {
            Client = new HttpClient { BaseAddress = new Uri(configuration["Api:BaseUri"]) };
        }

        protected string Queryable(string url, Dictionary<string, string> query)
        {
            return QueryHelpers.AddQueryString(url, query);
        }

        // Used like this:
        //protected async Task<T> GetAsync<T>(string url, Dictionary<string, string> query)
        //{
        //    return await GetAsync<T>(Queryable(url, query));
        //}

        protected async Task<T> GetAsync<T>(string url)
        {
            var resp = await Client.GetAsync(url);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadAsAsync<T>();
        }

        
        protected async Task<T> PostAsync<T>(string url, object obj)
        {
            var response = await Client.PostAsJsonAsync(url, obj);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<T>();
        }

        protected async Task<T> PutAsync<T>(string url, object obj)
        {
            var response = await Client.PutAsJsonAsync(url, obj);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<T>();
        }

        protected async Task DeleteAsync(string url)
        {
            var response = await Client.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
        }
    }
}
