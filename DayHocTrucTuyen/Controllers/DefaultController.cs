using DayHocTrucTuyen.Areas.Courses.Controllers;
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
            var maNd = User.Claims.First().Value;
            SuggestController sugesst = new SuggestController();

            //Lấy lớp học từ db và lớp học được đề xuất
            List<LopHoc> room = db.LopHocs.OrderByDescending(x => x.NgayTao).ToList();
            var result = sugesst.getRoom(maNd);

            //Gộp 2 danh sách đề xuất là lớp toàn hệ thống lại, lấy duy nhất giá trị
            result.AddRange(room);
            result = result.DistinctBy(x => x.MaLop).ToList();

            //Xử lý tìm kiếm
            if (!String.IsNullOrEmpty(q))
            {
                result = result.Where(x => x.TenLop.ToLower().Contains(q.ToLower())).ToList();
            }

            ViewBag.Search = q;

            return View(result);
        }

        //Trang liên hệ
        [AllowAnonymous]
        public IActionResult Contact()
        {
            return View();
        }
    }
}
