using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCClient.Models
{
    public class IndexBakeryTypeViewModel
    {
        public IEnumerable<BakeryType> bakeryTypes { get; set; }
        public string searchString { get; set; }
    }
}
