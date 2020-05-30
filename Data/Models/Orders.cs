using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Orders
    {
        public Orders()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public int? Idcustomer { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Note { get; set; }
        public int? Status { get; set; }

        public virtual Customer IdcustomerNavigation { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
