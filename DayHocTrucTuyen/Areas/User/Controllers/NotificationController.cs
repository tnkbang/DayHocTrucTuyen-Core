using DayHocTrucTuyen.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DayHocTrucTuyen.Areas.User.Controllers
{
    [Area(nameof(User))]
    [Route("User/[controller]/[action]/{id?}")]
    [Authorize]
    public class NotificationController : Controller
    {
        // GET: User/Notification
        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();

        //Tạo thông báo mới
        public void setThongBao(string maND, string tieude, string noidung, string lienket)
        {
            ThongBao newTB = new ThongBao();
            newTB.MaTb = newTB.setMa(maND);
            newTB.MaNd = maND;
            newTB.TieuDe = tieude;
            newTB.NoiDung = noidung;
            newTB.ThoiGian = DateTime.Now;
            newTB.TrangThai = false;
            newTB.LienKet = String.IsNullOrEmpty(lienket) ? null : lienket;

            db.ThongBaos.Add(newTB);
            db.SaveChanges();
        }

        //Trang xem thông báo
        public IActionResult Detail()
        {
            ThongBao tb = new ThongBao();
            tb.MaNd = User.Claims.First().Value;
            return View(tb);
        }

        //Đã xem thông báo
        [HttpPost]
        public IActionResult setDaXemThongBao(string maTB, string maND)
        {
            var tb = db.ThongBaos.FirstOrDefault(x => x.MaTb == maTB && x.MaNd == maND);
            if (tb == null) Json(new { tt = false });

            tb.TrangThai = true;
            db.SaveChanges();

            return Json(new { tt = true, link = tb.LienKet });
        }

        //Đã xem tất cả thông báo
        [HttpPost]
        public IActionResult setXemTatCaThongBao(string maND)
        {
            var tb = db.ThongBaos.Where(x => x.MaNd == maND && x.TrangThai == false);
            if (tb == null) Json(new { tt = false });

            foreach (var t in tb)
            {
                t.TrangThai = true;
            }
            db.SaveChanges();

            return Json(new { tt = true });
        }
    }
}
