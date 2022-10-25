using Microsoft.AspNetCore.Mvc;

namespace DayHocTrucTuyen.Controllers
{
    public class AccessController : Controller
    {
        // GET: Access
        [Route("Access/Denied", Name = "Access/Denied"), Route("error/403", Name = "error/403")]
        public IActionResult Denied()
        {
            Response.StatusCode = 403;
            return View();
        }

        [Route("error/404")]
        public IActionResult PageNotFound()
        {
            Response.StatusCode = 404;
            return View();
        }
    }
}
