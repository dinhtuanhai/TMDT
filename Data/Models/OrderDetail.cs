using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class OrderDetail
    {
        public int Idorder { get; set; }
        public int Idbakery { get; set; }
        public int? Quantity { get; set; }
        public long? Total { get; set; }
        public int? Status { get; set; }

        public virtual Bakery IdbakeryNavigation { get; set; }
        public virtual Orders IdorderNavigation { get; set; }
    }
}
