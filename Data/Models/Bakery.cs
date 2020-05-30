using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Bakery
    {
        public Bakery()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public int? Idtype { get; set; }
        public string Name { get; set; }
        public long? Price { get; set; }
        public double? Rating { get; set; }
        public string Describe { get; set; }
        public int? Status { get; set; }

        public virtual BakeryType IdtypeNavigation { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
