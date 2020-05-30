using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IBakery : IRepository<Bakery>
    {
        Task<List<string>> GetAllBakeryTypes();
    }
}
