using CorsoNetCore.Models.Services.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace CorsoNetCore.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICoursesBL _courcesBL;

        public CoursesController(ICoursesBL courcesBL)
        {
            _courcesBL = courcesBL;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Elenco dei corsi";
            var courses = _courcesBL.GetCourses();
            return View(courses);
        }

        public IActionResult Detail(string id)
        {
            return View();
        }
    }
}
