using DayHocTrucTuyen.Areas.Admin.Models;
using DayHocTrucTuyen.Areas.User.Controllers;
using DayHocTrucTuyen.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DayHocTrucTuyen.Areas.Courses.Controllers
{
    [Area(nameof(Courses))]
    [Route("courses/[controller]/[action]/{id?}")]
    [Authorize]
    public class ExamController : Controller
    {
        // GET: Courses/Exam
        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();

        //Quản lý phòng thi
        [Authorize(Roles = "01,02")]
        public IActionResult Manage(string id)
        {
            var pt = db.PhongThis.FirstOrDefault(x => x.MaPhong == id);
            if (id == null || pt == null)
            {
                return NotFound();
            }
            return View(pt);
        }
        //Danh sách bài thi của lớp
        [Authorize(Roles = "01,02")]
        public IActionResult List(string id, string q)
        {
            var lp = db.LopHocs.FirstOrDefault(x => x.MaLop == id);
            if (id == null || lp == null)
            {
                return NotFound();
            }
            var pt = db.PhongThis.Where(x => x.MaLop == lp.MaLop).OrderByDescending(x => x.NgayTao).ToList();
            ViewBag.MaLop = lp.MaLop;
            ViewBag.TenLop = lp.TenLop;
            ViewBag.Search = q;
            return View(pt);
        }
        //Trang xác nhận trước khi thi
        public IActionResult preExam(string id)
        {
            var pt = db.PhongThis.FirstOrDefault(x => x.MaPhong == id);
            if (id == null || pt == null)
            {
                return NotFound();
            }

            //Nếu chủ lớp nhấn vào thì chuyển trang quản lý phòng thi
            if(User.Claims.First().Value.Equals(pt.getMaOwner()))
                return Redirect("~/courses/exam/manage/" + pt.MaPhong);

            //Nếu học sinh không thuộc lớp nhấn vào thì chuyển trang xem lớp
            var mem = db.HocSinhThuocLops.FirstOrDefault(x => x.MaNd == User.Claims.First().Value && x.MaLop == pt.MaLop);
            if (mem == null)
            {
                return Redirect("~/courses/room/detail/" + pt.MaLop);
            }
            return View(pt);
        }

        //Trang thực hiện thi
        public IActionResult workExam(string id, int re)
        {
            if (id == null || re == 0) return NotFound();

            var work = db.ThoiGianLamBais.FirstOrDefault(x => x.MaNd == User.Claims.First().Value && x.MaPhong == id && x.LanThu == re);
            if (work == null) return NotFound();

            var pt = db.PhongThis.FirstOrDefault(x => x.MaPhong == work.MaPhong);

            if(pt != null)
            {
                ViewBag.LanThu = work.LanThu;

                TimeSpan thoigianthi = new TimeSpan(0, pt.ThoiLuong / 60, pt.ThoiLuong % 60, 0);
                DateTime thoiluong = work.BatDau;
                if (thoiluong.Add(thoigianthi) < DateTime.Now || work.KetThuc != null)
                {
                    return Redirect("~/courses/exam/preexam/" + pt.MaPhong);
                }

                ViewBag.ThoiGianThi = work.BatDau.Add(thoigianthi).ToString("yyyy-MM-dd HH:mm:ss");
                return View(pt);
            }

            return NotFound();
        }

        //Trang xem lại bài thi
        public IActionResult viewExam(string id, int re)
        {
            var pt = db.PhongThis.FirstOrDefault(x => x.MaPhong == id);
            if (id == null || pt == null)
            {
                return NotFound();
            }

            //Nếu bài thi không được xem lại
            if (!pt.XemLai) return Redirect("~/courses/exam/preexam/" + pt.MaPhong);

            var work = db.ThoiGianLamBais.FirstOrDefault(x => x.MaNd == User.Claims.First().Value && x.MaPhong == id && x.LanThu == re);
            if (work == null) return NotFound();
            ViewBag.LanThu = work.LanThu;

            return View(pt);
        }

        //Tạo mới phòng thi
        [HttpPost]
        public IActionResult createExam(string malop, string ten, int thoiluong, string mo, string dong, int lanthu, string matkhau, bool xemlai)
        {
            var lop = db.LopHocs.FirstOrDefault(x => x.MaLop == malop);
            if (lop == null) return Json(new { tt = false });

            PhongThi newPT = new PhongThi();
            newPT.MaPhong = newPT.setMa(lop.MaLop);
            newPT.MaLop = lop.MaLop;
            newPT.TenPhong = ten;
            newPT.NgayTao = DateTime.Now;
            if (matkhau != null || matkhau != "null")
            {
                newPT.MatKhau = matkhau;
            }
            newPT.NgayMo = DateTime.Parse(mo);
            newPT.NgayDong = DateTime.Parse(dong);
            newPT.LuotThi = lanthu;
            newPT.XemLai = xemlai;
            newPT.ThoiLuong = thoiluong;

            db.PhongThis.Add(newPT);
            db.SaveChanges();

            //Gửi thông báo đến tất cả thành viên thuộc lớp
            NotificationController notification = new NotificationController();
            var thanhvienlop = db.HocSinhThuocLops.Where(x => x.MaLop == lop.MaLop);
            foreach (var tvl in thanhvienlop)
            {
                notification.setThongBao(tvl.MaNd, "Bài thi mới", "exam", "Từ lớp: " + lop.TenLop, "courses/room/detail/" + lop.MaLop);
            }

            return Json(new { tt = true, exam = newPT.MaPhong });
        }
        //Chỉnh sửa phòng thi
        [HttpPost]
        public IActionResult editExam(string maphong, string ten, int thoiluong, string mo, string dong, int lanthu, string matkhau, bool xemlai)
        {
            var phongthi = db.PhongThis.FirstOrDefault(x => x.MaPhong == maphong);
            if (phongthi == null) return Json(new { tt = false });

            phongthi.TenPhong = ten;
            if (matkhau != null || matkhau != "null")
            {
                phongthi.MatKhau = matkhau;
            }
            phongthi.NgayMo = DateTime.Parse(mo);
            phongthi.NgayDong = DateTime.Parse(dong);
            phongthi.LuotThi = lanthu;
            phongthi.XemLai = xemlai;
            phongthi.ThoiLuong = thoiluong;

            db.SaveChanges();

            return Json(new { tt = true });
        }
        //Tạo mới câu hỏi
        [HttpPost]
        public IActionResult createQuest(string maphong, string cauhoi, string da1, string da2, string da3, string da4, string loigiai)
        {
            var phongthi = db.PhongThis.FirstOrDefault(x => x.MaPhong == maphong);
            if (phongthi == null) return Json(new { tt = false });

            CauHoiThi newQuest = new CauHoiThi();
            newQuest.Stt = phongthi.getSLCauHoi() + 1;
            newQuest.MaPhong = phongthi.MaPhong;
            newQuest.CauHoi = cauhoi;
            newQuest.LoiGiai = loigiai;
            newQuest.DapAn = da1 + '\\' + da2 + '\\' + da3 + '\\' + da4;

            db.CauHoiThis.Add(newQuest);
            db.SaveChanges();

            var json = new
            {
                STT = newQuest.Stt,
                Ma_Phong = newQuest.MaPhong,
                Cau_Hoi = newQuest.CauHoi,
                Loi_Giai = newQuest.getDapAnDung(newQuest.DapAn, newQuest.LoiGiai),
                Dap_An_1 = da1,
                Dap_An_2 = da2,
                Dap_An_3 = da3,
                Dap_An_4 = da4,
                Multi_Ans = newQuest.isMultiAns(newQuest.LoiGiai)
            };

            return Json(new { tt = true, cauhoi = json });
        }
        //Gán câu hỏi cần chỉnh sửa
        [HttpPost]
        public IActionResult getQuestEdit(int stt, string maphong)
        {
            var quest = db.CauHoiThis.FirstOrDefault(x => x.Stt == stt && x.MaPhong == maphong);
            if (quest == null) return Json(new { tt = false });

            string[] dapan = new string[] { "" };
            dapan = quest.DapAn.Split('\\');

            var json = new
            {
                STT = quest.Stt,
                Ma_Phong = quest.MaPhong,
                Cau_Hoi = quest.CauHoi,
                Loi_Giai = quest.getDapAnDungAsInt(quest.DapAn, quest.LoiGiai),
                Dap_An_1 = dapan[0],
                Dap_An_2 = dapan[1],
                Dap_An_3 = dapan[2],
                Dap_An_4 = dapan[3],
                Multi_Ans = quest.isMultiAns(quest.LoiGiai)
            };

            return Json(new { tt = true, cauhoi = json });
        }
        //Chỉnh sửa câu hỏi
        [HttpPost]
        public IActionResult editQuest(int stt, string maphong, string cauhoi, string da1, string da2, string da3, string da4, string loigiai)
        {
            var updateQuest = db.CauHoiThis.FirstOrDefault(x => x.Stt == stt && x.MaPhong == maphong);
            if (updateQuest == null) return Json(new { tt = false });

            updateQuest.CauHoi = cauhoi;
            updateQuest.LoiGiai = loigiai;
            updateQuest.DapAn = da1 + '\\' + da2 + '\\' + da3 + '\\' + da4;

            db.SaveChanges();

            var json = new
            {
                STT = updateQuest.Stt,
                Ma_Phong = updateQuest.MaPhong,
                Cau_Hoi = updateQuest.CauHoi,
                Loi_Giai = updateQuest.getDapAnDung(updateQuest.DapAn, updateQuest.LoiGiai),
                Dap_An_1 = da1,
                Dap_An_2 = da2,
                Dap_An_3 = da3,
                Dap_An_4 = da4,
                Multi_Ans = updateQuest.isMultiAns(updateQuest.LoiGiai)
            };

            return Json(new { tt = true, cauhoi = json });
        }

        //Kiểm tra xem phòng thi có mật khẩu hay không
        [HttpPost]
        public IActionResult ktMatKhau(string maphong)
        {
            var pt = db.PhongThis.FirstOrDefault(x => x.MaPhong == maphong);
            if (pt != null && pt.MatKhau != null) return Json(new { tt = true });

            return Json(new { tt = false });
        }

        //Chuẩn bị phòng thi
        [HttpPost]
        public IActionResult setWorkExam(string maphong, string matkhau)
        {
            var pt = db.PhongThis.FirstOrDefault(x => x.MaPhong == maphong && x.MatKhau == matkhau);
            if (pt == null) return Json(new { tt = false, mess = "Mật khẩu chưa chính xác!" });

            var nd = db.NguoiDungs.FirstOrDefault(x => x.MaNd == User.Claims.First().Value);
            if(nd != null && nd.hasCamThi(pt.MaPhong)) return Json(new { tt = false, mess = "Bạn đã bị cấm thi đối với bài thi này!" });

            ThoiGianLamBai working = new ThoiGianLamBai();
            working.MaNd = User.Claims.First().Value;
            working.MaPhong = pt.MaPhong;
            working.LanThu = pt.getSLThi(User.Claims.First().Value) + 1;
            working.BatDau = DateTime.Now;

            db.ThoiGianLamBais.Add(working);
            db.SaveChanges();

            var json = new
            {
                Ma_Phong = working.MaPhong,
                Lan_Thu = working.LanThu
            };

            return Json(new { tt = true, work = json });
        }

        //Gán đáp án thi
        [HttpPost]
        public IActionResult setDapAnThi(int stt, string maphong, int lanthu, string dapan)
        {
            var pt = db.PhongThis.FirstOrDefault(x => x.MaPhong == maphong);
            if (pt == null) return Json(new { tt = false });

            var tl = db.CauTraLois.FirstOrDefault(x => x.Stt == stt && x.MaPhong == pt.MaPhong && x.MaNd == User.Claims.First().Value && x.LanThu == lanthu);
            if (tl == null)
            {
                CauTraLoi newTL = new CauTraLoi();
                newTL.Stt = stt;
                newTL.MaPhong = pt.MaPhong;
                newTL.MaNd = User.Claims.First().Value;
                newTL.LanThu = lanthu;
                newTL.DapAn = dapan;

                db.CauTraLois.Add(newTL);
            }
            else
            {
                if (String.IsNullOrEmpty(dapan)) db.CauTraLois.Remove(tl);
                else tl.DapAn = dapan;
            }

            db.SaveChanges();

            return Json(new { tt = true });
        }
        //Kết thúc bài thi
        [HttpPost]
        public IActionResult setEndExam(string maphong, int lanthu)
        {
            var pt = db.PhongThis.FirstOrDefault(x => x.MaPhong == maphong);
            if (pt == null) return Json(new { tt = false });

            var bt = db.ThoiGianLamBais.FirstOrDefault(x => x.MaNd == User.Claims.First().Value && x.MaPhong == pt.MaPhong && x.LanThu == lanthu);

            if (bt != null)
            {
                bt.KetThuc = DateTime.Now;
                db.SaveChanges();
            }

            return Json(new { tt = true, id = pt.MaPhong });
        }

        //Cấm thi hoặc bỏ cấm thi người dùng
        [HttpPost]
        public IActionResult setBanned(string maNd, string maPhong)
        {
            var ban = db.BiCamThis.FirstOrDefault(x => x.MaNd == maNd && x.MaPhong == maPhong);
            bool trangthai = true;

            if(ban != null)
            {
                db.BiCamThis.Remove(ban);
                trangthai = false;
            }
            else
            {
                BiCamThi newBan = new BiCamThi();
                newBan.MaNd = maNd;
                newBan.MaPhong = maPhong;

                db.BiCamThis.Add(newBan);
                trangthai = true;
            }
            db.SaveChanges();

            return Json(new { tt = trangthai });
        }

        //Xem danh sách điểm người dùng đã làm bài
        [HttpGet]
        public IActionResult getPointExam(string examcode, string? search, string? sort, string? order, int? offset, int? limit)
        {
            var lst = db.NguoiDungs.Where(x => x.TrangThai);

            //Nếu tìm kiếm không rỗng thì xử lý tìm kiếm mã, họ tên,....
            if (!string.IsNullOrEmpty(search))
            {
                lst = lst.Where(s => string.Concat(s.HoLot, " ", s.Ten).Contains(search)
                                || s.MaNd.Contains(search));
            }

            //Xử lý sắp xếp
            if (!string.IsNullOrEmpty(sort) && !string.IsNullOrEmpty(order))
            {
                switch (sort)
                {
                    case "maNd":
                        if (order.Equals("asc"))
                        {
                            lst = lst.OrderBy(x => x.MaNd);
                        }
                        else
                        {
                            lst = lst.OrderByDescending(x => x.MaNd);
                        }
                        break;
                    case "hoTen":
                        if (order.Equals("asc"))
                        {
                            lst = lst.OrderBy(x => x.Ten).ThenBy(x => x.HoLot);
                        }
                        else
                        {
                            lst = lst.OrderByDescending(x => x.Ten).ThenByDescending(x => x.HoLot);
                        }
                        break;
                    case "gioiTinh":
                        if (order.Equals("asc"))
                        {
                            lst = lst.OrderBy(x => x.GioiTinh);
                        }
                        else
                        {
                            lst = lst.OrderByDescending(x => x.GioiTinh);
                        }
                        break;
                }
            }

            List<dynamic> lstResult = new List<dynamic>();
            var pt = db.PhongThis.FirstOrDefault(x => x.MaPhong == examcode);
            if (pt != null)
            {
                foreach (var item in lst.ToList())
                {
                    var temp = new
                    {
                        maNd = item.MaNd,
                        imgAvt = item.getImageAvt(),
                        hoTen = item.HoLot + " " + item.Ten,
                        gioiTinh = item.GioiTinh == 1 ? "Nam" : item.GioiTinh == 2 ? "Nữ" : item.GioiTinh == 3 ? "Thứ 3" : null,
                        sdt = item.Sdt,
                        biDanh = item.BiDanh == item.MaNd ? null : item.BiDanh,
                        luotThi = pt.getSLThi(item.MaNd),
                        diemCaoNhat = pt.getMaxPoint(item.MaNd)
                    };
                    lstResult.Add(temp);
                }
            }

            //Xử lý sắp xếp theo điểm thi
            if (!string.IsNullOrEmpty(sort) && !string.IsNullOrEmpty(order) && sort.Equals("diemCaoNhat"))
            {
                if (order.Equals("asc"))
                {
                    lstResult = lstResult.OrderBy(x => x.diemCaoNhat).ToList();
                }
                else
                {
                    lstResult = lstResult.OrderByDescending(x => x.diemCaoNhat).ToList();
                }
            }

            //Các tham số của phân trang như sau:
            //      đầu tiên là danh sách truyền vào phân trang
            //      tham số thứ 2 là vị trí phân trang
            //      tham số cuối là số lượng trang
            var result = PaginatedList<dynamic>.Create(lstResult, offset ?? 0, limit ?? 10);

            return Json(new { total = lst.ToList().Count, totalNotFiltered = lst.ToList().Count, rows = result });
        }
    }
}
