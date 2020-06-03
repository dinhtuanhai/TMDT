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
    [Authorize(Roles = "Administrators, Managers")]
    public class ManageBakeryTypeController : Controller
    {
        private readonly IBakeryService _service;
        private readonly IAuthorizationService _authorizationService;

        public ManageBakeryTypeController(IBakeryService service, IAuthorizationService authorizationService)
        {
            _service = service;
            _authorizationService = authorizationService;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            IEnumerable<BakeryType> listbakeryType = await _service.GetTypes();
            
            if(!String.IsNullOrEmpty(searchString))
            {
                listbakeryType = listbakeryType.Where(x => x.Name.Contains(searchString)).ToList();
            }
            IndexBakeryTypeViewModel iBTVM = new IndexBakeryTypeViewModel()
            {
                bakeryTypes = listbakeryType,
                searchString = searchString
            };

            return View(iBTVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BakeryType bakeryType)
        {
            if (ModelState.IsValid)
            {
                bakeryType.Status = true;
                //var isAuthorize = await _authorizationService.AuthorizeAsync(User, bakeryType, ProductOperations.Create);
                //if (!isAuthorize.Succeeded)
                //{
                //    return Forbid();
                //}

                await _service.CreateType(bakeryType);

                return RedirectToAction(nameof(Index));
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var type = await _service.GetType(id);

            return View(type);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BakeryType edittype)
        {
            if (ModelState.IsValid)
            {
                var type = await _service.GetType(id);

                if (type == null)
                {
                    return NotFound();
                }

                var isAuthorize = await _authorizationService.AuthorizeAsync(User, type, ProductOperations.Update);
                if (!isAuthorize.Succeeded)
                {
                    return Forbid();
                }

                await _service.UpdateType(id, edittype);

                return RedirectToAction(nameof(Index));
            }

            return View();
        }
        public async Task<IActionResult> Delete(int id)
        {
            var type = await _service.GetType(id);

            //var isAuthorize = await _authorizationService.AuthorizeAsync(User, type, ProductOperations.Update);
            //if (!isAuthorize.Succeeded)
            //{
            //    return Forbid();
            //}
            type.Status = false;
            await _service.UpdateType(id, type);

            return RedirectToAction(nameof(Index));
        }
    }
}