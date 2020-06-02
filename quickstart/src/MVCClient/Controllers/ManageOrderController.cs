using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace MVCClient.Controllers
{
    public class ManageOrderController : Controller
    {
        private readonly IOrder _orderrepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly TMDTContext _context;
        public ManageOrderController(IOrder orderrepository, IAuthorizationService authorizationService, TMDTContext context)
        {
            _orderrepository = orderrepository;
            _authorizationService = authorizationService;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var listorder = await _orderrepository.GetAll();
            return View(listorder);
        }

        public async Task<IActionResult> Detail(int id)
        {
            IEnumerable<OrderDetail> orderDetails = await _context.OrderDetail.Where(x => x.Idorder == id).ToListAsync();
            return View(orderDetails);
        }
    }
}