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
        Task<IndexViewModel> GetCatalog(string bakeryType, string searchString);
        Task<Bakery> GetBakery(int id);
        Task<BakeryType> GetType(int id);
        Task<IEnumerable<BakeryType>> GetTypes();
        Task<IEnumerable<Bakery>> GetListBakery(string type);
        Task CreateBakery(Bakery bakery);
        Task UpdateBakery(int id, Bakery bakery);
        Task CreateType(BakeryType bakeryType);
        Task UpdateType(int id, BakeryType bakeryType);
        Task DeleteBakery(int id);
    }
}
