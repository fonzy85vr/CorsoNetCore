using Microsoft.AspNetCore.Mvc;

namespace CorsoNetCore.Controllers {
    public class CoursesController : Controller {
        public IActionResult Index() {
            return Content("Home corsi");
        }

        public IActionResult Detail(string id) {
            return Content($"Dettaglio corso {id}");
        }
    }
}
