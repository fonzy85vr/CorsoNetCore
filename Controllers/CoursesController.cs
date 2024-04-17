using CorsoNetCore.Models.Services.Service;
using CorsoNetCore.Models.ViewModel;
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

        public async Task<IActionResult> Index(PaginationModel model)
        {
            ViewData["Title"] = "Elenco dei corsi";
            var courses = await _courcesBL.Search(model);
            return View(courses);
        }

        public IActionResult Detail(string id)
        {
            return View();
        }
    }
}
