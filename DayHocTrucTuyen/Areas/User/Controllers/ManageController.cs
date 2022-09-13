using DayHocTrucTuyen.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DayHocTrucTuyen.Areas.User.Controllers
{
    [Area(nameof(User))]
    [Route("User/[controller]/[action]")]
    [Authorize]
    public class ManageController : Controller
    {
        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();
        public IActionResult Index()
        {
            var user = db.NguoiDungs.FirstOrDefault(x => x.MaNd == User.Claims.First().Value);
            return View(user);
        }
    }
}
