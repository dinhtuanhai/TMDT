using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCClient.Models
{
    public class EditBakeryViewModel
    {
        public Bakery bakeryModify { get; set; }
        public IEnumerable<BakeryType> listBakeryType { get; set; }
    }
}
