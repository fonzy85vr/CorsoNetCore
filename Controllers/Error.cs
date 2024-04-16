using Microsoft.AspNetCore.Mvc;

namespace CorsoNetCore.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Errore";
            return View();
        }
    }
}