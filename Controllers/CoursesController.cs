using CorsoNetCore.Models.Services.Service;
using Microsoft.AspNetCore.Mvc;

namespace CorsoNetCore.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICoursesService _courcesBL;

        public CoursesController(ICoursesService courcesBL)
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
