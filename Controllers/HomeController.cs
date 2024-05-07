using CorsoNetCore.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CorsoNetCore.Controllers
{
    public class HomeController : Controller
    {
        [ResponseCache(Duration = 60)]
        public IActionResult Index(PaginationModel model)
        {
            return View(model);
        }
    }
}