using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Data.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace MVCClient.Controllers
{
    public class StripeController : Controller
    {
        private readonly IOrder _orderRepository;
        private readonly ShoppingCart _shoppingCart;
        public StripeController(IOrder orderRepositoty, ShoppingCart shoppingCart)
        {
            _orderRepository = orderRepositoty;
            _shoppingCart = shoppingCart;
        }
        public IActionResult Index(Orders order)
        {
            return View(order);
        }

        public IActionResult PayWithCard(string stripeEmail, string stripeToken, Orders orders)
        {
            if (User.Identity.IsAuthenticated)
            {
                orders.IdBuyer = User.FindFirstValue("sub");
            }
            _orderRepository.CreateOrder(orders);
            
            var customers = new CustomerService();
            var charges = new ChargeService();

            var customer = customers.Create(new CustomerCreateOptions { 
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = charges.Create(new ChargeCreateOptions { 
                Amount = (long)orders.OrderTotal,
                Description = "OrderId: " + orders.Id,
                Currency = "vnd",
                Customer = customer.Id,
                ReceiptEmail = stripeEmail
            });

            if(charge.Status == "succeeded")
            {
                string BalanceTransactionId = charge.BalanceTransactionId;
                _orderRepository.Paid(orders);
                _shoppingCart.ClearCard();
                ViewBag.Message = "Thanks for your order";
            }
            return View();
        }

        public IActionResult COD(Orders orders)
        {
            if(User.Identity.IsAuthenticated)
            {
                orders.IdBuyer = User.FindFirstValue("sub");
            }

            _orderRepository.CreateOrder(orders);
            _shoppingCart.ClearCard();

            ViewBag.Message = "Thanks for your order";
            return View();
        }
    }
}