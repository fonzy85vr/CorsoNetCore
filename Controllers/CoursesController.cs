using Microsoft.AspNetCore.Mvc;

namespace CorsoNetCore.Controllers {
    public class CoursesController : Controller {
        public IActionResult Index() {
            return View();
        }

        public IActionResult Detail(string id) {
            return View();
        }
    }
}
