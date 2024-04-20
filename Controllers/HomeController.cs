using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CorsoNetCore.Controllers
{
    public class HomeController : Controller
    {
        [ResponseCache(Duration = 60)]
        public IActionResult Index()
        {
            return View();
        }
    }
}