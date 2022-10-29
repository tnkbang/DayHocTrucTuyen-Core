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
    }
}
