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

        public async Task<IEnumerable<BakeryType>> GetTypes()
        {
            var uri = _baseUrl + $"/types";
            return await _httpClient.GetListAsync<BakeryType>(uri);
        }

        public async Task<IndexViewModel> GetCatalog(string bakeryType, string searchString)
        {
            var uri = _baseUrl + $"/catalog?searchString={searchString}&bakeryType={bakeryType}";

            return await _httpClient.GetAsync<IndexViewModel>(uri);
        }

        public async Task<IEnumerable<Bakery>> GetListBakery(string type)
        {
            var uri = _baseUrl + $"/listbakery";
            return await _httpClient.GetListAsync<Bakery>(uri);
        }

        public async Task CreateBakery(Bakery bakery)
        {
            var uri = _baseUrl;

            await _httpClient.PostAsync(uri, bakery);
        }
        public async Task UpdateBakery(int id, Bakery bakery)
        {
            var uri = _baseUrl + $"/{id}";

            await _httpClient.PutAsync(uri, bakery);
        }

        public async Task CreateType(BakeryType bakeryType)
        {
            var uri = _baseUrl + "/createtype";

            await _httpClient.PostAsync(uri, bakeryType);
        }
        public async Task UpdateType(int id, BakeryType bakeryType)
        {
            var uri = _baseUrl + $"/updatetype/{id}";

            await _httpClient.PutAsync(uri, bakeryType);
        }

        public async Task DeleteBakery(int id)
        {
            var uri = _baseUrl + $"/{id}";

            await _httpClient.DeleteAsync(uri);
        }

        public async Task<BakeryType> GetType(int id)
        {
            var uri = _baseUrl + $"/type/{id}";
            return await _httpClient.GetAsync<BakeryType>(uri);
        }
    }
}
