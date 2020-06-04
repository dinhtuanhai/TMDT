using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Data.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCClient.Infrastructure;

namespace MVCClient.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrder _orderRepository;
        private readonly ShoppingCart _shoppingCart;

        public OrderController(IOrder orderRepository, ShoppingCart shoppingCart)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
        }

        public IActionResult Checkout()
        {

            if (User.Identity.IsAuthenticated)
            {
                Orders orders = CreateOrderForCurrentUser();

                return RedirectToAction("Index","Stripe",orders);
            }
            else
            {
                return View();
            }
            
        }

        [HttpPost]
        public IActionResult Checkout(Orders orders)
        {
            var items = _shoppingCart.GetShoppingCartItem();
            
            _shoppingCart.ShoppingCartItem = items;
            
            if(_shoppingCart.ShoppingCartItem.Count == 0)
            {
                ModelState.AddModelError("","Your cart is empty, add some bakeries first");
            }

            if (ModelState.IsValid)
            {
                var total = _shoppingCart.GetShoppingCartTotal();
                orders.OrderTotal = total;
                orders.CreateDate = DateTime.Now;

                return RedirectToAction("Index","Stripe", orders);
            }
            else
                return View();
            
        }

        public IActionResult CheckoutComplete()
        {
            ViewBag.Message = "Thanks for your order";
            return View();
        }

        private Orders CreateOrderForCurrentUser()
        {
            Orders orders = new Orders();
            orders.FirstName = User.FindFirstValue("given_name");
            orders.LastName = User.FindFirstValue("family_name");
            orders.Email = User.FindFirstValue("email"); //userclaim đang thiếu phone
            orders.Address = User.FindFirstValue("address");
            orders.Phone = User.FindFirstValue("phone_number");
            orders.OrderTotal = _shoppingCart.GetShoppingCartTotal();
            orders.IdBuyer = User.FindFirstValue("sub");

            return orders;
        }
    }
}