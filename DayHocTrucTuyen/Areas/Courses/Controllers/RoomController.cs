using DayHocTrucTuyen.Areas.User.Controllers;
using DayHocTrucTuyen.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DayHocTrucTuyen.Areas.Courses.Controllers
{
    [Area(nameof(Courses))]
    [Route("Courses/[controller]/[action]/{id?}")]
    [Authorize]
    public class RoomController : Controller
    {
        // GET: Courses/Room
        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();

        //Danh sách các lớp học của người dùng
        public IActionResult List(string q)
        {
            string maUser = User.Claims.First().Value;
            List<LopHoc> roomOfUser = db.LopHocs.Where(x => x.MaNd == maUser).OrderByDescending(x => x.NgayTao).ToList();
            ViewData["Tag"] = db.Tags.ToList();
            ViewBag.Search = q;
            return View(roomOfUser);
        }

        //Giao diện chính lớp học
        public IActionResult Detail(string id)
        {
            LopHoc room = db.LopHocs.FirstOrDefault(x => x.MaLop == id);
            LopHoc roomBD = db.LopHocs.FirstOrDefault(x => x.BiDanh == id);
            if (id == null || room == null && roomBD == null)
            {
                return NotFound();
            }
            if (roomBD != null)
            {
                ViewData["Post"] = roomBD.getAllPost();
                return View(roomBD);
            }
            ViewData["Post"] = room.getAllPost();
            return View(room);
        }

        //Trang chỉnh sửa lớp học
        public IActionResult editRoom(string id)
        {
            string maUser = User.Claims.First().Value;
            LopHoc room = db.LopHocs.FirstOrDefault(x => x.MaLop == id && x.MaNd == maUser);
            if (room == null || id == null)
            {
                return NotFound();
            }
            ViewData["Tag"] = db.Tags.ToList();
            return View(room);
        }

        //Thêm mới lớp học
        [HttpPost]
        public async Task<IActionResult> createRoom(string tl, string bd, string mt, string tag, IFormFile img)
        {
            var maUser = User.Claims.First().Value;
            LopHoc newLop = new LopHoc();

            newLop.MaLop = newLop.setMa();
            newLop.MaNd = maUser;
            newLop.NgayTao = DateTime.Now;
            newLop.TenLop = tl;
            newLop.GiaTien = 0;
            newLop.TrangThai = true;

            if (img != null)
            {
                //Khai báo đường dẫn lưu file
                var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot\\Content\\Img\\roomCover\\");
                bool basePathExists = Directory.Exists(basePath);

                //Nếu thư mục không có thì tạo mới
                if (!basePathExists) Directory.CreateDirectory(basePath);

                string file_extension = Path.GetFileName(img.FileName).Substring(Path.GetFileName(img.FileName).LastIndexOf('.'));
                var fileName = newLop.MaLop + "-" + DateTime.Now.Millisecond + file_extension;
                var filePath = Path.Combine(basePath, fileName);

                //Thêm file vào server và cập nhật vào csdl
                if (fileName != null && !System.IO.File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await img.CopyToAsync(stream);
                    }

                    newLop.ImgBia = fileName;
                }
            }

            if (!String.IsNullOrEmpty(bd))
            {
                LopHoc checkBD = db.LopHocs.FirstOrDefault(x => x.BiDanh == bd);
                if (checkBD != null)
                {
                    return Json(new { tt = false, mess = "Bí danh đã tồn tại !" });
                }
                newLop.BiDanh = bd;
            }

            if (!String.IsNullOrEmpty(mt)) { newLop.MoTa = mt; }

            db.LopHocs.Add(newLop);

            //Thêm phần tử vào table LopThuocTag
            if (tag != "")
            {
                string[] roomTag = new string[] { "" };
                roomTag = tag.Split(',');

                for (var i = 0; i < roomTag.Length; i++)
                {
                    LopThuocTag newLTT = new LopThuocTag();
                    newLTT.MaLop = newLop.MaLop;
                    newLTT.MaTag = roomTag[i];
                    db.LopThuocTags.Add(newLTT);
                }
            }

            db.SaveChanges();

            return Json(new { tt = true, room = newLop.MaLop });
        }

        //Cập nhật thông tin lớp học
        [HttpPost]
        public async Task<IActionResult> editRoom(string ml, string tl, string bd, int gt, string mt, string tag, IFormFile img)
        {
            var maUser = User.Claims.First().Value;
            var update = db.LopHocs.FirstOrDefault(x => x.MaLop == ml && x.MaNd == maUser);

            if (update == null)
            {
                return Json(new { tt = false });
            }

            update.TenLop = tl;
            update.GiaTien = gt;

            if (!String.IsNullOrEmpty(bd))
            {
                LopHoc checkBD = db.LopHocs.FirstOrDefault(x => x.BiDanh == bd);
                LopHoc bidanhThis = db.LopHocs.FirstOrDefault(x => x.MaLop == update.MaLop && x.BiDanh == bd);
                if (checkBD != null && bidanhThis == null)
                {
                    return Json(new { tt = false, mess = "Bí danh đã tồn tại !" });
                }
                update.BiDanh = bd;
            }
            else { update.BiDanh = null; }

            if (!String.IsNullOrEmpty(mt)) { update.MoTa = mt; }
            else { update.MoTa = null; }

            if (img != null)
            {
                //Khai báo đường dẫn lưu file
                var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot\\Content\\Img\\roomCover\\");
                bool basePathExists = Directory.Exists(basePath);

                string file_extension = Path.GetFileName(img.FileName).Substring(Path.GetFileName(img.FileName).LastIndexOf('.'));
                var fileName = update.MaLop + "-" + DateTime.Now.Millisecond + file_extension;
                var filePath = Path.Combine(basePath, fileName);

                //Xóa file cũ khỏi server
                if (!String.IsNullOrEmpty(update.ImgBia) && System.IO.File.Exists(Path.Combine(basePath, update.ImgBia)))
                {
                    System.IO.File.Delete(basePath + update.ImgBia);
                }

                //Thêm file vào server và cập nhật vào csdl
                if (fileName != null && !System.IO.File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await img.CopyToAsync(stream);
                    }

                    update.ImgBia = fileName;
                }
            }

            //Thêm phần tử vào table LopThuocTag
            if (tag != "")
            {
                //Xóa hết tag cũ của lớp
                var delLTT = db.LopThuocTags.Where(x => x.MaLop == update.MaLop);
                db.LopThuocTags.RemoveRange(delLTT);

                string[] roomTag = new string[] { "" };
                roomTag = tag.Split(',');

                for (var i = 0; i < roomTag.Length; i++)
                {
                    LopThuocTag newLTT = new LopThuocTag();
                    newLTT.MaLop = update.MaLop;
                    newLTT.MaTag = roomTag[i];
                    db.LopThuocTags.Add(newLTT);
                }
            }

            db.SaveChanges();

            return Json(new { tt = true, room = update.MaLop });
        }

        //Thêm mới thành viên cho lớp
        [HttpPost]
        public IActionResult setJoinRoom(string maLop)
        {
            var maUser = User.Claims.First().Value;
            var lp = db.LopHocs.FirstOrDefault(x => x.MaLop == maLop);
            if(lp != null)
            {
                //Nếu giá tiền của lớp lớn hơn 0 thì tiến hành kiểm tra và thanh toán
                if(lp.GiaTien > 0)
                {
                    ViController vinguoidung = new ViController();

                    //Nếu số tiền có trong ví nhỏ hơn số tiền cần thanh toán thì trả về false
                    if (vinguoidung.getSoDu(maUser) < lp.GiaTien)
                        return Json(new { tt = false });

                    //Thực hiện thanh toán
                    vinguoidung.setThayDoiSoDu(maUser, false, lp.GiaTien, "Phí tham gia lớp: " + lp.TenLop);

                    //Thực hiện thêm tiền vào tài khoản giáo viên
                    //Có khấu trừ 10% phí duy trì
                    var gv = db.NguoiDungs.FirstOrDefault(x => x.MaNd == lp.MaNd);
                    if(gv != null)
                    {
                        //Nếu giáo viên chưa có ví thì tạo ví mới cho giáo viên
                        if (!vinguoidung.hasVi(gv.MaNd))
                            vinguoidung.setNew(gv.MaNd, 0);

                        //Tiến hành thêm tiền cho giáo viên, có khấu trừ 10% phí duy trì
                        var tempSoTien = lp.GiaTien - lp.GiaTien * 10 / 100;
                        vinguoidung.setThayDoiSoDu(gv.MaNd, true, tempSoTien, "Thu từ học sinh tham gia lớp: " + lp.TenLop);
                    }
                }

                //Thêm người dùng vào lớp
                HocSinhThuocLop hs = new HocSinhThuocLop();
                hs.MaNd = maUser;
                hs.MaLop = maLop;
                hs.NgayThamGia = DateTime.Now;

                db.HocSinhThuocLops.Add(hs);
                db.SaveChanges();
            }

            return Json(new { tt = true });
        }

        //Lấy tên tất cả các lớp học
        [HttpPost]
        [AllowAnonymous]
        public IActionResult getAllCourses()
        {
            List<string> name = new List<string>();
            foreach(var i in db.LopHocs.ToList())
            {
                name.Add(i.TenLop);
            }

            return Json(new { tt = true, lst = name });
        }
    }
}
