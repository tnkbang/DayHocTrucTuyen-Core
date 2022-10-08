using DayHocTrucTuyen.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DayHocTrucTuyen.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        // GET: Profile
        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();

        //Trang thông tin người dùng
        public IActionResult Info(string id)
        {
            NguoiDung userMa = db.NguoiDungs.FirstOrDefault(x => x.MaNd == id);
            NguoiDung userBiDanh = db.NguoiDungs.FirstOrDefault(x => x.BiDanh == id);
            if (userMa == null && userBiDanh == null || User == null)
            {
                //Response.StatusCode = 404;
                return NotFound();
            }
            if (userMa == null)
            {
                setXemTrang(userBiDanh.MaNd, User.Claims.First().Value);
                return View(userBiDanh);
            }
            setXemTrang(userMa.MaNd, User.Claims.First().Value);
            return View(userMa);
        }

        //Kiểm tra người dùng có đủ thông tin chưa
        [HttpPost]
        public IActionResult checkInfo()
        {
            var user = db.NguoiDungs.FirstOrDefault(x => x.MaNd == User.Claims.First().Value);
            if(String.IsNullOrEmpty(user.Sdt) || user.NgaySinh == null || user.GioiTinh == null)
            {
                return Json(new { tt = false });
            }

            return Json(new { tt = true });
        }

        //Cập nhật thông tin cá nhân
        public IActionResult Update()
        {
            var user = db.NguoiDungs.FirstOrDefault(x => x.MaNd == User.Claims.First().Value);
            return View(user);
        }

        //Set cập nhật thông tin
        [HttpPost]
        public async Task<IActionResult> setUpdate(string hl, string ten, DateTime ns, int gt, string sdt, string mt, string bd, IFormFile avt)
        {
            var user = db.NguoiDungs.FirstOrDefault(x => x.MaNd == User.Claims.First().Value);

            user.HoLot = hl;
            user.Ten = ten;
            user.NgaySinh = ns;
            user.GioiTinh = gt;
            user.Sdt = sdt;
            user.MoTa = mt;
            user.BiDanh = bd;

            //Xử lý avt
            if (avt != null)
            {
                //Khai báo đường dẫn lưu file
                var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot\\Content\\Img\\userAvt\\");
                bool basePathExists = Directory.Exists(basePath);

                //Nếu thư mục không có thì tạo mới
                if (!basePathExists) Directory.CreateDirectory(basePath);

                string file_extension = Path.GetFileName(avt.FileName).Substring(Path.GetFileName(avt.FileName).LastIndexOf('.'));
                var fileName = "avt-" + user.MaNd + "-" + DateTime.Now.Millisecond + file_extension;
                var filePath = Path.Combine(basePath, fileName);

                //Xóa file cũ khỏi server
                if (!String.IsNullOrEmpty(user.ImgAvt) && System.IO.File.Exists(Path.Combine(basePath, user.ImgAvt)))
                {
                    System.IO.File.Delete(basePath + user.ImgAvt);
                }

                //Thêm file vào server và cập nhật vào csdl
                if (fileName != null && !System.IO.File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await avt.CopyToAsync(stream);
                    }

                    user.ImgAvt = fileName;
                }
            }

            db.SaveChanges();

            return Json(new { tt = true });
        }

        //Đăng ký giáo viên
        [HttpPost]
        public IActionResult regisTeacher()
        {
            //Kiểm tra nếu tài khoản không phải là học sinh thì không được đăng ký làm giáo viên
            var user = db.NguoiDungs.FirstOrDefault(x => x.MaNd == User.Claims.First().Value);
            if (user.MaLoai != "03") return Json(new { tt = false, mess = "Tài khoản của bạn không thể đăng làm giáo viên." });

            //Kiểm tra nếu tài khoản đang chờ phê duyệt thì không thể tiếp tục đăng ký
            var gv = db.PheDuyets.FirstOrDefault(x => x.MaNd == User.Claims.First().Value);
            if (gv != null) return Json(new { tt = false, mess = "Bạn đã đăng ký và đang chờ phê duyệt." });

            PheDuyet newPD = new PheDuyet();
            newPD.MaNd = User.Claims.First().Value;
            newPD.NgayDangKy = DateTime.Now;
            newPD.TrangThai = true;

            db.PheDuyets.Add(newPD);
            db.SaveChanges();

            return Json(new { tt = true, mess = "Yêu cầu của bạn đang chờ xét duyệt." });
        }

        //Lấy lý do bị từ chối phê duyệt
        [HttpPost]
        public IActionResult viewReason()
        {
            var pd = db.PheDuyets.FirstOrDefault(x => x.MaNd == User.Claims.First().Value);
            if (pd == null) return Json(new { tt = false });

            return Json(new { tt = true, mess = pd.GhiChu });
        }

        //Hủy yêu cầu lên giáo viên
        [HttpPost]
        public IActionResult cancelTeacher()
        {
            var pd = db.PheDuyets.FirstOrDefault(x => x.MaNd == User.Claims.First().Value);
            if(pd != null)
            {
                db.PheDuyets.Remove(pd);
                db.SaveChanges();
            }
            return Json(new { tt = true });
        }

        //Xem trạng thái nâng cấp lên giáo viên
        [HttpPost]
        public IActionResult getStateTeacher()
        {
            var user = db.NguoiDungs.FirstOrDefault(x => x.MaNd == User.Claims.First().Value);
            var pd = db.PheDuyets.FirstOrDefault(x => x.MaNd == user.MaNd);

            if(pd != null)
            {
                if (pd.TrangThai) return Json(new { mess = "waiting" });
                else return Json(new { mess = "refuse" });
            }
            if (user.MaLoai == "03" && pd == null) return Json(new { mess = "none" });

            return Json(new { mess = "not allow" });
        }

        //Hàm set xem trang
        public void setXemTrang(string nd, string nx)
        {
            if (nd == nx) return;
            XemTrang xt = db.XemTrangs.Where(x => x.NguoiDung == nd && x.NguoiXem == nx).OrderByDescending(x => x.MaXt).FirstOrDefault();
            if (xt != null)
            {
                TimeSpan minTime = new TimeSpan(24, 0, 0);
                if (DateTime.Now - xt.ThoiGian > minTime)
                {
                    XemTrang newXT = new XemTrang();
                    newXT.MaXt = newXT.setMa(nd);
                    newXT.NguoiDung = nd;
                    newXT.NguoiXem = nx;
                    newXT.ThoiGian = DateTime.Now;

                    db.XemTrangs.Add(newXT);
                    db.SaveChanges();
                }
            }
            else
            {
                XemTrang newXT = new XemTrang();
                newXT.MaXt = newXT.setMa(nd);
                newXT.NguoiDung = nd;
                newXT.NguoiXem = nx;
                newXT.ThoiGian = DateTime.Now;

                db.XemTrangs.Add(newXT);
                db.SaveChanges();
            }
        }
        //Hàm set thích trang
        public IActionResult setThichTrang(string nd, string nt)
        {
            if (nd == nt) return Json(new { tt = false });
            ThichTrang yt = db.ThichTrangs.Where(x => x.NguoiDung == nd && x.NguoiThich == nt).OrderByDescending(x => x.MaYt).FirstOrDefault();
            if (yt != null)
            {
                TimeSpan minTime = new TimeSpan(24, 0, 0);
                if (DateTime.Now - yt.ThoiGian > minTime)
                {
                    ThichTrang newYT = new ThichTrang();
                    newYT.MaYt = newYT.setMa(nd);
                    newYT.NguoiDung = nd;
                    newYT.NguoiThich = nt;
                    newYT.ThoiGian = DateTime.Now;

                    db.ThichTrangs.Add(newYT);
                    db.SaveChanges();
                }
            }
            else
            {
                ThichTrang newYT = new ThichTrang();
                newYT.MaYt = newYT.setMa(nd);
                newYT.NguoiDung = nd;
                newYT.NguoiThich = nt;
                newYT.ThoiGian = DateTime.Now;

                db.ThichTrangs.Add(newYT);
                db.SaveChanges();
            }
            return Json(new { tt = true });
        }
    }
}
