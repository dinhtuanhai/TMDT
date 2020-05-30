using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCClient.Models
{
    public class BakeryViewModel
    {
        public int Id { get; set; }
        public int? Idtype { get; set; }
        public string Name { get; set; }
        public long? Price { get; set; }
        public double? Rating { get; set; }
        public string Describe { get; set; }
        public int? Status { get; set; }
    }
}
