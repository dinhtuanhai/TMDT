using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVCClient.Models;
using Newtonsoft.Json.Linq;

namespace MVCClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TMDTContext _context;

        public HomeController(ILogger<HomeController> logger, TMDTContext context)
        {
            _logger = logger;
            _context = context;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> YourOrders(string id, DateTime dateSearch)
        {
            IEnumerable<Orders> yourOrders = await _context.Orders.Where(x => x.IdBuyer == id).ToListAsync();
            string dateSearchToString = dateSearch.ToString("dd/MM/yyyy");
            if(!dateSearchToString.Equals("01/01/0001"))
            {
                yourOrders = yourOrders.Where(x => x.CreateDate.Value.ToString("dd/MM/yyyy") == dateSearch.ToString("dd/MM/yyyy"));
            }
            return View(yourOrders);
        }
        [Authorize]
        public async Task<IActionResult> YourOrderDetail(int id)
        {
            IEnumerable<OrderDetail> yourOrderDetail = await _context.OrderDetail.Where(x => x.Idorder == id).ToListAsync();
            return View(yourOrderDetail);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var error = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
            _logger.LogError($"An error ocurred at RequestId {error.RequestId}");

            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [Authorize]
        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
        [Authorize]
        public IActionResult Login()
        {
            return View("Index");
        }
    }
}
