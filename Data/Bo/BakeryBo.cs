using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Bo
{
    public class BakeryBo : Repository<Bakery>, IBakery
    {
        private readonly TMDTContext _context;
        public BakeryBo(TMDTContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<string>> GetAllBakeryTypes()
        {
            return await _context.BakeryType.Select(x => x.Name).ToListAsync();
        }
    }
}
