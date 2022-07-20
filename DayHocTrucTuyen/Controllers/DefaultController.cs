using DayHocTrucTuyen.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DayHocTrucTuyen.Controllers
{
    public class DefaultController : Controller
    {
        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();

        //Trang chủ hệ thống
        [AllowAnonymous]
        public IActionResult Index()
        {
            ViewBag.Courses = db.LopHocs.Count();
            ViewBag.GV = db.NguoiDungs.Where(x => x.MaLoai == "03").Count();
            ViewBag.HS = db.NguoiDungs.Where(x => x.MaLoai == "04").Count();
            ViewBag.TV = db.NguoiDungs.Count();
            ViewData["Room"] = db.LopHocs.OrderByDescending(x => x.NgayTao).ToList();
            return View();
        }

        //Trang danh sách lớp học
        [AllowAnonymous]
        public IActionResult Courses()
        {
            List<LopHoc> room = db.LopHocs.OrderByDescending(x => x.NgayTao).ToList();
            return View(room);
        }

        //Trang liên hệ
        [AllowAnonymous]
        public IActionResult Contact()
        {
            return View();
        }
    }
}
