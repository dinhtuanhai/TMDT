using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCClient.Authorization;
using MVCClient.Models;
using MVCClient.Services;

namespace MVCClient.Controllers
{
    [AllowAnonymous]
    public class MenuController : Controller
    {
        private readonly IBakeryService _service;
        private readonly IAuthorizationService _authorizationService;
        public MenuController(IBakeryService service, IAuthorizationService authorizationService)
        {
            _service = service;
            _authorizationService = authorizationService;
        }
        
        public async Task<IActionResult> Index(string bakeryType, string searchString)
        {
            var menu = await _service.GetCatalog(bakeryType, searchString);

            var indexViewModel = new IndexViewModel()
            {
                AllBakeryTypes = menu.AllBakeryTypes,
                Bakeries = menu.Bakeries,
                BakeriesSale = menu.Bakeries.Where(x => x.Discount > 0).ToList(),
                BakeriesNew = menu.Bakeries.Where(x => (DateTime.Now - x.CreateDate).Days < 30).ToList()
            };
            return View(indexViewModel);
        }
        public async Task<IActionResult> Detail(int id)
        {
            Bakery bakery = await _service.GetBakery(id);
            return View(bakery);
        }

    }
}