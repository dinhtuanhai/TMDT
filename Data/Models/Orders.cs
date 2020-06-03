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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public decimal OrderTotal { get; set; }
        public int? Idcustomer { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Note { get; set; }
        public string IdBuyer { get; set; }

        public bool IsPaid { get; set; }
        public int? Status { get; set; } //Tình trạng đơn hàng: chưa vận chuyển/đã vận chuyển

        public virtual Customer IdcustomerNavigation { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
