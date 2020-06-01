using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public int? Idbakery { get; set; }
        public string IdShoppingCart { get; set; }
        public virtual Bakery IdbakeryNavigation { get; set; }
        public virtual ShoppingCart IdShoppingCartNavigation { get; set; }
    }
}
