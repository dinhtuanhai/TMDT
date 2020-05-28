using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCClient.Infrastructure
{
    public interface IHttpClient
    {
        Task<IEnumerable<T>> GetListAsync<T>(string uri) where T : class;
        Task<T> GetAsync<T>(string uri) where T : class;
        Task PostAsync(string uri, object entity);
        Task PutAsync(string uri, object entity);
        Task DeleteAsync(string uri);
    }
}
