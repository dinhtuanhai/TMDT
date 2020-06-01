using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using MVCClient.Models;
using MVCClient.Services;

namespace MVCClient.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IBakeryService _service;
        private readonly ShoppingCart _shoppingcart;
        public ShoppingCartController(IBakeryService service, ShoppingCart shoppingCart)
        {
            _service = service;
            _shoppingcart = shoppingCart;
        }
        public ViewResult Index()
        {
            var items = _shoppingcart.GetShoppingCartItem();
            _shoppingcart.ShoppingCartItem = items;

            var scVM = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingcart,
                ShoppingCartTotal = _shoppingcart.GetShoppingCartTotal()
            };

            return View(scVM);
        }

        public async Task<RedirectToActionResult> AddToShoppingCart(int id, int amount)
        {
            Bakery selectedBakery = await _service.GetBakery(id);
            if(selectedBakery != null)
            {
                _shoppingcart.AddToCart(selectedBakery, amount);
            }

            return RedirectToAction("Index");
        }

        public async Task<RedirectToActionResult> RemoveFromShoppingCart(int id)
        {
            var selectedBakery = await _service.GetBakery(id);
            if (selectedBakery != null)
            {
                _shoppingcart.RemoveFromCart(selectedBakery);
            }

            return RedirectToAction("Index");
        }
    }
}