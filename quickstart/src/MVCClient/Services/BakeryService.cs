using Data.Models;
using Microsoft.Extensions.Options;
using MVCClient.Infrastructure;
using MVCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCClient.Services
{
    public class BakeryService : IBakeryService
    {
        private readonly string _baseUrl;
        private readonly IHttpClient _httpClient;

        public BakeryService(IHttpClient httpClient, IOptions<AppSettings> appSettings)
        {
            _httpClient = httpClient;
            _baseUrl = appSettings.Value.ApiUrl;
        }

        public async Task<Bakery> GetBakery(int id)
        {
            var uri = _baseUrl + $"/{id}";
            return await _httpClient.GetAsync<Bakery>(uri);
        }

        public async Task<IndexViewModel> GetCatalog()
        {
            var uri = _baseUrl + $"/catalog";

            return await _httpClient.GetAsync<IndexViewModel>(uri);
        }

        public async Task<IEnumerable<Bakery>> GetListBakery(string type)
        {
            var uri = _baseUrl + $"/listbakery";
            return await _httpClient.GetListAsync<Bakery>(uri);
        }
    }
}
