using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCClient.Models
{
    public class IndexViewModel
    {
        public string SearchString { get; set; }
        public string TypeSearch { get; set; }
        public IEnumerable<BakeryType> AllBakeryTypes { get; set; }
        public IList<Bakery> Bakeries { get; set; }
        public IList<Bakery> BakeriesSale { get; set; }
        public IList<Bakery> BakeriesNew { get; set; }
    }
}
