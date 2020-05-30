using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCClient.Models;
using MVCClient.Services;

namespace MVCClient.Controllers
{
    public class MenuController : Controller
    {
        private readonly IBakeryService _service;
        private readonly IAuthorizationService _authorizationService;
        public MenuController(IBakeryService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var menu = await _service.GetCatalog();
            var indexViewModel = new IndexViewModel()
            {
                AllBakeryTypes = menu.AllBakeryTypes,
                Bakeries = menu.Bakeries

            };
            return View(indexViewModel);
        }
        public async Task<IActionResult> Detail(int id)
        {
            Bakery bakery = await _service.GetBakery(id);
            return View(bakery);
        }
        public async Task<JsonResult> ShowListBakery(string tabtype)
        {
            try
            {
                var listbakery = await _service.GetListBakery(tabtype);
                List<Bakery> listdata = new List<Bakery>();
                foreach(Bakery item in listbakery)
                {
                    Bakery bakery = new Bakery();
                    bakery.Id = item.Id;
                    bakery.Name = item.Name;
                    bakery.Price = item.Price;
                    bakery.Describe = item.Describe;
                    bakery.Idtype = item.Idtype;
                    listdata.Add(bakery);
                }
                return Json(new
                {
                    status = true,
                    data = listdata
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
    }
}