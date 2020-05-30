using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCClient.Models;

namespace MVCClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var error = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
            _logger.LogError($"An error ocurred at RequestId {error.RequestId}");

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
