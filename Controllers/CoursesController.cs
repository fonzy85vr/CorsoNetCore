using System.Diagnostics.Eventing.Reader;
using CorsoNetCore.Models.Services.ApplicationLayer;
using CorsoNetCore.Models.Services.Repository;
using CorsoNetCore.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CorsoNetCore.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICoursesService _courcesBL;
        private readonly IPaymentGateway _paymentGateway;

        public CoursesController(ICachedCourseService courcesBL, IPaymentGateway paymentGateway)
        {
            _courcesBL = courcesBL;
            _paymentGateway = paymentGateway;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(PaginationModel model)
        {
            ViewData["Title"] = "Elenco dei corsi";
            var courses = await _courcesBL.Search(model);
            return View(courses);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Detail(int id)
        {
            var course = await _courcesBL.GetDetail(id);

            if (course != null)
            {
                ViewData["Title"] = course.Title;
            }

            return View(course);
        }

        public async Task<IActionResult> Subscribe(int id)
        {
            //var result = await _courcesBL.Subscribe(id);
            var result = await _paymentGateway.GetPaymentUrl();

            return Redirect(result);
        }

        public async Task<IActionResult> ConfirmPayment(string token, string payerID) {
            var result = await _paymentGateway.Confirm(token);

            return RedirectToAction("Index");
        }
    }
}
