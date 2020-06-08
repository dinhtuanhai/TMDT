using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Bakery
    {
        public Bakery()
        {
            OrderDetail = new HashSet<OrderDetail>();
            ShoppingCartItem = new HashSet<ShoppingCartItem>();
        }

        public int Id { get; set; }
        public int? Idtype { get; set; }
        public string Name { get; set; }
        public long? Price { get; set; }
        public double? Rating { get; set; }
        public string Describe { get; set; }
        public int? Quantity { get; set; }
        public int? Status { get; set; }
        public byte[] Image { get; set; }
        public int Discount { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual BakeryType IdtypeNavigation { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
        public virtual ICollection<ShoppingCartItem> ShoppingCartItem { get; set; }
    }
}
