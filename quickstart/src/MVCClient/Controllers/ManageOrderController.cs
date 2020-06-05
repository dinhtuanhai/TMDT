using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
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
        public async Task<IActionResult> Index(string searchString, DateTime dateSearch)
        {
            var listorder = await _orderrepository.GetAll();
            string dateSearchToString = dateSearch.ToString("dd/MM/yyyy");
            if (!String.IsNullOrEmpty(searchString))
            {
                listorder = listorder.Where(x => x.Phone == searchString
                                              || x.Email.Contains(searchString)
                                              || x.FirstName.ToLower().Contains(searchString.ToLower())
                                              || x.LastName.ToLower().Contains(searchString.ToLower())).ToList();
            }
            if(!dateSearchToString.Equals("01/01/0001"))
            {
                listorder = listorder.Where(x => x.CreateDate.Value.ToString("dd/MM/yyyy") == dateSearch.ToString("dd/MM/yyyy"));
            }

            return View(listorder);
        }

        public async Task<IActionResult> Detail(int id)
        {
            IEnumerable<OrderDetail> orderDetails = await _context.OrderDetail.Where(x => x.Idorder == id).ToListAsync();
            return View(orderDetails);
        }

        public async Task<JsonResult> DeliveryOrder(int id)
        {
            try
            {
                Orders orders = await _orderrepository.GetBy(id);
                orders.Status = 1;

                //Trừ số lượng bánh khi giao hàng
                _orderrepository.UpdateBakeryQuantity(orders);
                
                _context.Orders.Update(orders);
                _context.SaveChanges();

                return Json(new
                {
                    status = true
                });
            }
            catch
            {
                return Json(new
                {
                    status = false
                });
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Orders orders = await _orderrepository.GetBy(id);
                _context.Orders.Remove(orders);
                _context.SaveChanges();

                return RedirectToAction("Index","ManageOrder");
            }
            catch
            {
                return View("Error");
            }
        }
    }
}