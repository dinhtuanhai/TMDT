using Data.Models;
using MVCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCClient.Services
{
    public interface IBakeryService
    {
        Task<IndexViewModel> GetCatalog();
        Task<Bakery> GetBakery(int id);
        Task<IEnumerable<Bakery>> GetListBakery(string type);
    }
}
