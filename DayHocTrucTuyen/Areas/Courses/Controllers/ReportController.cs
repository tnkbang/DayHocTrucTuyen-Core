using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DayHocTrucTuyen.Areas.Courses.Controllers
{
    public class ReportController : Controller
    {
        [Area(nameof(Courses))]
        [Route("Courses/[controller]/[action]")]
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
