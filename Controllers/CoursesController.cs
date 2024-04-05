using CorsoNetCore.Models.Services.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace CorsoNetCore.Controllers {
    public class CoursesController : Controller {
        public IActionResult Index() {
            var courcesBl = new CourcesBL();
            var courses = courcesBl.GetCourses();
            return View(courses);
        }

        public IActionResult Detail(string id) {
            return View();
        }
    }
}
