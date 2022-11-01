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
        [Route("{id?}", Name = "ShotLink"), Route("/Default/Index", Name = "Default")]
        public IActionResult Index(string? id)
        {
            //Chuyển hướng khi có nhập ký tự
            if (!String.IsNullOrEmpty(id))
            {
                var user = db.NguoiDungs.FirstOrDefault(x => x.MaNd == id);
                var lop = db.LopHocs.FirstOrDefault(x => x.MaLop == id);

                if(user != null) return Redirect("/Profile/Info/" + user.MaNd);
                if (lop != null) return Redirect("/Courses/Room/Detail/" + lop.MaLop);
            }

            ViewBag.Courses = db.LopHocs.Count();
            ViewBag.GV = db.NguoiDungs.Where(x => x.MaLoai == "03").Count();
            ViewBag.HS = db.NguoiDungs.Where(x => x.MaLoai == "04").Count();
            ViewBag.TV = db.NguoiDungs.Count();
            ViewData["Room"] = db.LopHocs.OrderByDescending(x => x.NgayTao).ToList();
            return View();
        }

        //Trang danh sách lớp học
        [AllowAnonymous]
        public IActionResult Courses(string q)
        {
            List<LopHoc> room = db.LopHocs.OrderByDescending(x => x.NgayTao).ToList();

            if (!String.IsNullOrEmpty(q))
            {
                room = room.Where(x => x.TenLop.ToLower().Contains(q.ToLower())).ToList();
            }

            ViewBag.Search = q;

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
