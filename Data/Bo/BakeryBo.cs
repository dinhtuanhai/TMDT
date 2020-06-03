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
        public async Task<IEnumerable<BakeryType>> GetAllBakeryTypes()
        {
            return await _context.BakeryType.Where(x => x.Status == true).ToListAsync();
        }

        public async Task<IEnumerable<Bakery>> GetBakeries(string bakeryType = null, string searchString = null)
        {
            var bakeries = from m in _context.Bakery
                           where m.Status == 1
                         select m;

            if (!string.IsNullOrEmpty(bakeryType))
            {
                bakeries = bakeries.Where(m => m.IdtypeNavigation.Name.Contains(bakeryType));
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                bakeries = bakeries.Where(m => m.Name.Contains(searchString));
            }

            return await bakeries.OrderBy(x => x.Idtype).ToListAsync();
        }

    }
}
