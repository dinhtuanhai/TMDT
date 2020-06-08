using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCClient.Authorization;
using MVCClient.Models;
using MVCClient.Services;

namespace MVCClient.Controllers
{
    [Authorize(Roles = "Administrators, Managers")]
    public class ManageBakeryController : Controller
    {
        private readonly IBakeryService _service;
        private readonly IAuthorizationService _authorizationService;
        public ManageBakeryController(IBakeryService service, IAuthorizationService authorizationService)
        {
            _service = service;
            _authorizationService = authorizationService;
        }
        public async Task<IActionResult> Index(string TypeSearch, string searchString)
        {
            var menu = await _service.GetCatalog(TypeSearch, searchString);
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
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.listType = await _service.GetTypes();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Bakery bakery, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                bakery.Status = 1;
                bakery.CreateDate = DateTime.Now;
                await UpdateImage(bakery, Image);

                var isAuthorize = await _authorizationService.AuthorizeAsync(User, bakery, ProductOperations.Create);
                if (!isAuthorize.Succeeded)
                {
                    return Forbid();
                }

                await _service.CreateBakery(bakery);

                return RedirectToAction(nameof(Index));
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var bakery = await _service.GetBakery(id);
            ViewBag.listType = await _service.GetTypes();

            return View(bakery);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Bakery editBakery, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                var bakery = await _service.GetBakery(id);

                if (bakery == null)
                {
                    return NotFound();
                }

                var isAuthorize = await _authorizationService.AuthorizeAsync(User, bakery, ProductOperations.Update);
                if (!isAuthorize.Succeeded)
                {
                    return Forbid();
                }

                if(Image != null)
                {
                    await UpdateImage(editBakery, Image);
                }
                else
                {
                    editBakery.Image = bakery.Image;
                }

                await _service.UpdateBakery(id, editBakery);

                return RedirectToAction(nameof(Index));
            }

            return View();
        }
        public async Task<IActionResult> Delete(int id)
        {
            var bakery = await _service.GetBakery(id);

            var isAuthorize = await _authorizationService.AuthorizeAsync(User, bakery, ProductOperations.Update);
            if (!isAuthorize.Succeeded)
            {
                return Forbid();
            }
            bakery.Status = 0;
            await _service.UpdateBakery(id, bakery);

            return RedirectToAction(nameof(Index));
        }

        private async Task UpdateImage(Bakery bakery, IFormFile Image)
        {
            if (Image != null)
            {
                using (var stream = new MemoryStream())
                {
                    await Image.CopyToAsync(stream);
                    bakery.Image = stream.ToArray();
                }
            }
        }
    }
}