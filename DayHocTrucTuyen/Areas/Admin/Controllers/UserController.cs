using DayHocTrucTuyen.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DayHocTrucTuyen.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Route("Admin/[controller]/[action]")]
    [Authorize(Roles = "01")]
    public class UserController : Controller
    {
        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();

        public IActionResult List()
        {
            var mems = db.NguoiDungs.Where(x => x.MaLoai != "01").ToList();
            return View(mems);
        }
    }
}
