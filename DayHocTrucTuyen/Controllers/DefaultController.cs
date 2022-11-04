using DayHocTrucTuyen.Areas.Admin.Models;
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
        public IActionResult Courses(string? q, int? offset, int? limit)
        {
            var maNd = "U000000";
            if (User.Identity.IsAuthenticated)
            {
                maNd = User.Claims.First().Value;
            }
            SuggestController sugesst = new SuggestController();

            //Lấy lớp học từ db và lớp học được đề xuất
            List<LopHoc> room = db.LopHocs.OrderByDescending(x => x.NgayTao).ToList();
            var lst = sugesst.getRoom(maNd);

            //Gộp 2 danh sách đề xuất là lớp toàn hệ thống lại, lấy duy nhất giá trị
            lst.AddRange(room);
            lst = lst.DistinctBy(x => x.MaLop).ToList();

            //Xử lý tìm kiếm
            if (!String.IsNullOrEmpty(q))
            {
                lst = lst.Where(x => x.TenLop.ToLower().Contains(q.ToLower())).ToList();
            }

            ViewBag.Search = q;

            if(offset == null || limit == null)
            {
                return View(new List<LopHoc>());
            }

            //Xử lý phân trang
            var temp = PaginatedList<LopHoc>.Create(lst, offset ?? 0, limit ?? 10);

            //Tạo list lớp học custom để trả về json
            List<dynamic> result = new List<dynamic>();
            foreach (var i in temp)
            {
                //Lấy tag của lớp gán vào list
                List<dynamic> tempLst = new List<dynamic>();
                var tag = i.getTag();
                foreach (var t in tag)
                {
                    var temptag = new
                    {
                        maTag = t.MaTag,
                        tenTag = t.TenTag
                    };
                    tempLst.Add(temptag);
                }

                //Custom lớp học
                var owner = i.getOwner();
                var item = new
                {
                    maLop = i.MaLop,
                    tenLop = i.TenLop,
                    loaiGiaTien = i.getTypeGiaTien(),
                    giaTien = i.getGiaTien(),
                    imgBg = i.getImage(),
                    tag = tempLst,
                    moTa = String.IsNullOrEmpty(i.MoTa) ? "Lớp chưa có mô tả!" : i.MoTa,
                    ownerMa = owner.MaNd,
                    ownerAvt = owner.getImageAvt(),
                    ownerTen = owner.Ten,
                    ownerHoTen = owner.getFullName(),
                    thanhVien = i.getMembers(),
                    camXuc = i.getSLCamXuc()
                };
                result.Add(item);
            }

            return Json(new { result });
        }

        //Trang liên hệ
        [AllowAnonymous]
        public IActionResult Contact()
        {
            return View();
        }
    }
}
