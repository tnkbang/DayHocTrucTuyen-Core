using DayHocTrucTuyen.Areas.Admin.Models;
using DayHocTrucTuyen.Areas.Courses.Controllers;
using DayHocTrucTuyen.Areas.User.Controllers;
using DayHocTrucTuyen.Models;
using DayHocTrucTuyen.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DayHocTrucTuyen.Controllers
{
    public class DefaultController : Controller
    {
        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Sendmail()
        {
            EmailController emailController = new EmailController();

            string newPass = StringGenerator.Alphabet(8);
            string body = emailController.getRePass("Băng", newPass);

            var isSend = emailController.Send("trieukhanhbang123@gmail.com", "Thông báo bảo mật", body);
            //var isSend = emailController.SendWithFile("trieukhanhbang123@gmail.com", "Test mail", "Test gửi mail !", "fpv-gnj-laj-001-1554969988_hinh-nen-4k-tuyet-dep-cho-may-tinh-08.png");

            if (isSend) return Json(new { tt = true });
            return Json(new { tt = false });
        }

        //Trang chủ hệ thống
        [AllowAnonymous]
        [Route("{id?}", Name = "ShotLink"), Route("/default/index", Name = "default")]
        public IActionResult Index(string? id)
        {
            //Chuyển hướng khi có nhập ký tự
            if (!String.IsNullOrEmpty(id))
            {
                var user = db.NguoiDungs.FirstOrDefault(x => x.MaNd == id);
                var lop = db.LopHocs.FirstOrDefault(x => x.MaLop == id);

                if (user == null && lop == null) return NotFound();
                if(user != null) return Redirect("/profile/info/" + user.MaNd);
                if (lop != null) return Redirect("/courses/room/detail/" + lop.MaLop);
            }

            //Lấy mã người dùng khi có đăng nhập
            var maNd = "U000000";
            if (User.Identity.IsAuthenticated)
            {
                maNd = User.Claims.First().Value;
            }

            ViewBag.Courses = db.LopHocs.Count();
            ViewBag.GV = db.NguoiDungs.Where(x => x.MaLoai == "03").Count();
            ViewBag.HS = db.NguoiDungs.Where(x => x.MaLoai == "04").Count();
            ViewBag.TV = db.NguoiDungs.Count();

            //Lấy gợi ý 9 khóa học phổ biến
            SuggestController suggest = new SuggestController();
            ViewData["Room"] = suggest.getRoom(maNd).Take(9).ToList();
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

        //Trang nhận thưởng người mới
        [Authorize]
        [Route("Gift")]
        public IActionResult Gift()
        {
            var maNd = User.Claims.First().Value;
            ViController vinguoidung = new ViController();
            if (vinguoidung.hasVi(maNd))
            {
                var nd = db.NguoiDungs.FirstOrDefault(x => x.MaNd == maNd);
                if(nd != null) return View(new { tt = false, ten = nd.Ten });
            }

            int money = new Random().Next(100000, 200000);
            ViewBag.Money = money.ToString("n0");

            vinguoidung.setNew(maNd, 0);
            vinguoidung.setThayDoiSoDu(maNd, true, money, "Nhận tiền từ quà tặng.");

            return View(new { tt = true, ten = "" });
        }
    }
}
