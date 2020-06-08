using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Models
{
    public class ShoppingCart
    {
        private readonly TMDTContext _context;
        public ShoppingCart()
        {
            ShoppingCartItem = new HashSet<ShoppingCartItem>();
        }
        public ShoppingCart(TMDTContext context)
        {
            _context = context;
        }
        public string Id { get; set; }
        public virtual ICollection<ShoppingCartItem> ShoppingCartItem { get; set; }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<TMDTContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);
            return new ShoppingCart(context) { Id = cartId };
        }

        public void AddToCart(Bakery bakery, int amount)
        {
            ShoppingCartItem shoppingCartItem = _context.ShoppingCartItem.SingleOrDefault(s => s.Idbakery == bakery.Id && s.IdShoppingCart.Equals(Id));
            ShoppingCart shoppingCart = _context.ShoppingCart.SingleOrDefault(x => x.Id == this.Id);
            
            if(shoppingCart == null)
            {
                _context.ShoppingCart.Add(this);
            }
            
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    IdShoppingCart = Id,
                    Idbakery = bakery.Id,
                    Amount = amount
                };
                _context.ShoppingCartItem.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount += amount;
            }
            _context.SaveChanges();
        }
        public int RemoveFromCart(Bakery bakery)
        {
            ShoppingCartItem shoppingCartItem = _context.ShoppingCartItem.SingleOrDefault(s => s.Idbakery == bakery.Id && s.IdShoppingCart.Equals(Id));
            var localAmount = 0;

            if(shoppingCartItem != null)
            {
                if(shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _context.ShoppingCartItem.Remove(shoppingCartItem);
                }
            }

            _context.SaveChanges();
            return localAmount;
        }

        public ICollection<ShoppingCartItem> GetShoppingCartItem()
        {
            return ShoppingCartItem ?? (ShoppingCartItem = _context.ShoppingCartItem.Where(x => x.IdShoppingCart == Id).Include(s => s.IdbakeryNavigation).ToList());
        }

        public void ClearCard()
        {
            var cartItems = _context.ShoppingCartItem.Where(c => c.IdShoppingCart == Id);
            _context.ShoppingCartItem.RemoveRange(cartItems);
            _context.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _context.ShoppingCartItem.Where(c => c.IdShoppingCart == Id).Select(c => (c.IdbakeryNavigation.Price - (c.IdbakeryNavigation.Price * c.IdbakeryNavigation.Discount / 100))  * c.Amount).Sum();
            return (decimal)total;
        }
    }
}
