using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVCClient.Controllers
{
    public class ManageAccountController : Controller
    {
        [Authorize(Roles = "Administrators")]
        public IActionResult Index()
        {
            return View();
        }
    }
}