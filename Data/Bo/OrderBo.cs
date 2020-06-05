using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Bo
{
    public class OrderBo : Repository<Orders>, IOrder
    {
        private readonly TMDTContext _context;
        private readonly ShoppingCart _shoppingCart;
        public OrderBo(TMDTContext context, ShoppingCart shoppingCart) : base(context)
        {
            _context = context;
            _shoppingCart = shoppingCart;
        }
        public void CreateOrder(Orders orders)
        {
            orders.CreateDate = DateTime.Now;
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

        public void Paid(Orders orders)
        {
            orders.IsPaid = true;
            _context.SaveChanges();
        }

        public void UpdateBakeryQuantity(Orders orders)
        {
            List<OrderDetail> listorder =  _context.OrderDetail.Where(x => x.Idorder == orders.Id).ToList();
            foreach(var item in listorder)
            {
                Bakery bakery = _context.Bakery.FirstOrDefault(entry => entry.Id == item.Idbakery);
                if (bakery != null)
                {
                    _context.Entry(bakery).State = EntityState.Detached;
                    bakery.Quantity = bakery.Quantity - item.Quantity;
                }
                _context.Entry(bakery).State = EntityState.Modified;
            }
            
            _context.SaveChanges();
        }
    }
}
