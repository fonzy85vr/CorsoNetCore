using Microsoft.AspNetCore.Mvc;

namespace CorsoNetCore.Customizations.ViewComponents
{
    public class PaginationBar : ViewComponent{
        public IViewComponentResult Invoke(){
            return View();
        }
    }
}