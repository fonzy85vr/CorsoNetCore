using CorsoNetCore.Models.Services.ApplicationLayer;
using CorsoNetCore.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CorsoNetCore.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICoursesService _courcesBL;

        public CoursesController(ICachedCourseService courcesBL)
        {
            _courcesBL = courcesBL;
        }

        public async Task<IActionResult> Index(PaginationModel model)
        {
            ViewData["Title"] = "Elenco dei corsi";
            var courses = await _courcesBL.Search(model);
            return View(courses);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var course = await _courcesBL.GetDetail(id);

            if(course != null){
                ViewData["Title"] = course.Title;
            }
            
            return View(course);
        }
    }
}
