using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace MVCClient.Controllers
{
    public class StripeController : Controller
    {
        public IActionResult Index(Orders order)
        {
            return View(order);
        }

        public IActionResult Charge(string stripeEmail, string stripeToken, int id, long total)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();

            var customer = customers.Create(new CustomerCreateOptions { 
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = charges.Create(new ChargeCreateOptions { 
                Amount = total,
                Description = "OrderId: " + id,
                Currency = "vnd",
                Customer = customer.Id,
                ReceiptEmail = stripeEmail
            });

            if(charge.Status == "succeeded")
            {
                string BalanceTransactionId = charge.BalanceTransactionId;
                ViewBag.Message = "Thanks for your order";
            }


            return View();
        }
    }
}