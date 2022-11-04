using DayHocTrucTuyen.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DayHocTrucTuyen.Areas.User.Controllers
{
    [Area(nameof(User))]
    [Route("User/[controller]/[action]/{id?}")]
    [Authorize]
    public class ViController : Controller
    {
        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();

        //Tạo ví mới
        public void setNew(string maNd, double sotien)
        {
            ViNguoiDung newVi = new ViNguoiDung();
            newVi.MaNd = maNd;
            newVi.NgayMo = DateTime.Now;
            newVi.SoDu = sotien;
            newVi.TrangThai = true;

            db.ViNguoiDungs.Add(newVi);
            db.SaveChanges();
        }

        //Kiểm tra đã có ví hay chưa
        public bool hasVi(string maNd)
        {
            var vi = db.ViNguoiDungs.FirstOrDefault(x => x.MaNd == maNd);
            return vi != null;
        }

        //Kiểm tra số dư
        public double getSoDu(string maNd)
        {
            var vi = db.ViNguoiDungs.FirstOrDefault(x => x.MaNd == maNd);
            if (vi != null && vi.TrangThai) return vi.SoDu;
            return 0;
        }

        //Kiểm tra ví hoạt động hay không, Nếu không có ví thì tương đương ví không hoạt động
        public bool getTrangThai(string maNd)
        {
            var vi = db.ViNguoiDungs.FirstOrDefault(x => x.MaNd == maNd);
            if (vi != null && vi.TrangThai) return true;
            return false;
        }

        //Thêm bớt số tiền trong ví
        public bool setThayDoiSoDu(string maNd, bool congthem, double sotien, string ghichu)
        {
            var vi = db.ViNguoiDungs.FirstOrDefault(x => x.MaNd == maNd);
            if(vi != null)
            {
                //Nếu cộng thêm thì tăng số tiền trong ví
                if (congthem) vi.SoDu += sotien;
                else
                {
                    //Nếu số dư lớn hơn số tiền thanh toán thì tiến hành trừ tiền
                    //Ngược lại không thể trừ
                    if (vi.SoDu > sotien)
                    {
                        vi.SoDu -= sotien;
                    }
                    else return false;
                }

                //Lưu lịch sử giao dịch
                LichSuGiaoDich ls = new LichSuGiaoDich();
                ls.MaNd = maNd;
                ls.ThoiGian = DateTime.Now;
                ls.ThuVao = congthem;
                ls.SoTien = sotien;
                ls.SoDu = vi.SoDu;
                ls.GhiChu = ghichu;
                db.LichSuGiaoDiches.Add(ls);

                //Lưu lại khi hoàn tất
                db.SaveChanges();
            }
            return true;
        }

        //Yêu cầu rút tiền
        [HttpPost]
        public IActionResult ycRutTien(string loai, string? stk, double sotien)
        {
            var maNd = User.Claims.First().Value;

            if(string.IsNullOrEmpty(loai)) return Json(new { tt = false, mess = "Thiếu loại thanh toán!" });
            if (sotien > getSoDu(maNd)) return Json(new { tt = false, mess = "Số tiền trong tài khoản không đủ!" });

            //Thực hiện trừ tiền từ ví của người dùng
            bool thaotacvi = setThayDoiSoDu(maNd, false, sotien, "Thực hiện rút tiền về tài khoản " + loai);
            if (!thaotacvi) return Json(new { tt = false, mess = "Số tiền trong tài khoản không đủ!" });

            //Tạo mới yêu cầu thanh toán
            YeuCauThanhToan newTT = new YeuCauThanhToan();
            newTT.MaNd = maNd;
            newTT.LoaiThanhToan = loai;
            newTT.SoTaiKhoan = stk ?? null;
            newTT.SoTien = sotien;
            newTT.TrangThai = 1;
            newTT.ThoiGian = DateTime.Now;
            db.YeuCauThanhToans.Add(newTT);

            db.SaveChanges();

            return Json(new { tt = true });
        }

        //Kiểm tra số dư trước khi yêu cầu thanh toán
        [HttpGet]
        public IActionResult checkSoDu()
        {
            var sodu = getSoDu(User.Claims.First().Value);
            return Json(new { sodu });
        }
    }
}
