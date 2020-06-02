using Data.Interfaces;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Bo
{
    public class OrderBo : IOrder
    {
        private readonly TMDTContext _context;
        private readonly ShoppingCart _shoppingCart;
        public OrderBo(TMDTContext context, ShoppingCart shoppingCart)
        {
            _context = context;
            _shoppingCart = shoppingCart;
        }
        public void CreateOrder(Orders orders)
        {
            _context.Orders.Add(orders);
            _context.SaveChanges();
            var shoppingCartItems = _shoppingCart.GetShoppingCartItem();
            foreach(var item in shoppingCartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Quantity = item.Amount,
                    Idbakery = (int)item.Idbakery,
                    Idorder = orders.Id,
                    Total = item.Amount * item.IdbakeryNavigation.Price
                };
                _context.OrderDetail.Add(orderDetail);
            }
            _context.SaveChanges();
        }
    }
}
