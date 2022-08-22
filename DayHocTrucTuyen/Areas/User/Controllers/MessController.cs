﻿using DayHocTrucTuyen.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DayHocTrucTuyen.Areas.User.Controllers
{
    [Area(nameof(User))]
    [Route("User/[controller]/[action]")]
    [Authorize]
    public class MessController : Controller
    {
        // GET: User/Mess
        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();
        public IActionResult Index()
        {
            return View();
        }
        //Lấy tin nhắn từ người gửi cụ thể đến user session
        [HttpPost]
        public IActionResult getTinNhanTuUser(string maNG)
        {
            TinNhan tn = new TinNhan();
            NguoiDung Usend = tn.getUser(maNG);
            NguoiDung Ureceived = tn.getUser(User.Claims.First().Value);
            var usersend = new
            {
                Ma_ND = Usend.MaNd,
                Ma_Loai = Usend.MaLoai,
                Ho_Lot = Usend.HoLot,
                Ten = Usend.Ten,
                Img_Avt = Usend.getImageAvt()
            };
            var userreceived = new
            {
                Ma_ND = Ureceived.MaNd,
                Ma_Loai = Ureceived.MaLoai,
                Ho_Lot = Ureceived.HoLot,
                Ten = Ureceived.Ten,
                Img_Avt = Ureceived.getImageAvt()
            };
            List<dynamic> list = new List<dynamic>();
            foreach (var m in tn.getAllTinNhan(maNG, User.Claims.First().Value))
            {
                var temp = new
                {
                    ID = m.Id,
                    Nguoi_Gui = m.NguoiGui,
                    Nguoi_Nhan = m.NguoiNhan,
                    Thoi_Gian = m.ThoiGian.ToString("yyyy-MM-dd'T'HH:mm:ss"),
                    Noi_Dung = m.NoiDung,
                    Trang_Thai = m.TrangThai
                };
                list.Add(temp);
            }
            setXemTinNhan(User.Claims.First().Value, maNG);
            return Json(new { tt = true, USend = usersend, UReceived = userreceived, TinNhan = list });
        }

        //Gửi tin nhắn cho người khác
        [HttpPost]
        public IActionResult sendNewTinNhan(string maNN, string noidung)
        {
            if (maNN == "" || noidung == "") return Json(new { tt = false });

            TinNhan tn = new TinNhan();
            tn.Id = tn.setID();
            tn.NguoiGui = User.Claims.First().Value;
            tn.NguoiNhan = maNN;
            tn.ThoiGian = DateTime.Now;
            tn.NoiDung = noidung;
            tn.TrangThai = false;

            db.TinNhans.Add(tn);
            db.SaveChanges();

            NguoiDung nguoiDung = new NguoiDung();
            nguoiDung.MaNd = User.Claims.First().Value;

            return Json(new { tt = true, Img_Avt = nguoiDung.getImageAvt(), Noi_Dung = tn.NoiDung, Thoi_Gian = tn.ThoiGian.ToString("yyyy-MM-dd'T'HH:mm:ss") });
        }
        //Đã xem tất cả tin nhắn
        [HttpPost]
        public IActionResult setXemTatCaTinNhan(string maND)
        {
            var tn = db.TinNhans.Where(x => x.NguoiNhan == maND && x.TrangThai == false);
            if (tn == null) Json(new { tt = false });

            foreach (var t in tn)
            {
                t.TrangThai = true;
            }
            db.SaveChanges();

            return Json(new { tt = true });
        }
        public void setXemTinNhan(string maNN, string maNG)
        {
            var tn = db.TinNhans.Where(x => x.NguoiNhan == maNN && x.NguoiGui == maNG && x.TrangThai == false);
            if (tn == null) Json(new { tt = false });

            foreach (var t in tn)
            {
                t.TrangThai = true;
            }
            db.SaveChanges();
        }
    }
}