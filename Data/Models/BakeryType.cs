using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class BakeryType
    {
        public BakeryType()
        {
            Bakery = new HashSet<Bakery>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Bakery> Bakery { get; set; }
    }
}
