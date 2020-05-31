using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DTO
{
    public class IndexDTO
    {
        public string SearchString { get; set; }
        public string TypeSearch { get; set; }
        public IEnumerable<BakeryType> AllBakeryTypes { get; set; }
        public IEnumerable<Bakery> Bakeries { get; set; }
    }
}
