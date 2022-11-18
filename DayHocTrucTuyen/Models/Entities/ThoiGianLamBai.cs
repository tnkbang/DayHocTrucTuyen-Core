using System;
using System.Collections.Generic;

namespace DayHocTrucTuyen.Models.Entities
{
    public partial class ThoiGianLamBai
    {
        public string MaNd { get; set; } = null!;
        public string MaPhong { get; set; } = null!;
        public int LanThu { get; set; }
        public DateTime BatDau { get; set; }
        public DateTime? KetThuc { get; set; }

        public virtual NguoiDung MaNdNavigation { get; set; } = null!;
        public virtual PhongThi MaPhongNavigation { get; set; } = null!;

        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();

        public string getTrangThaiLamBai()
        {
            var pt = db.PhongThis.FirstOrDefault(x => x.MaPhong == this.MaPhong);
            if (pt != null)
            {
                TimeSpan thoigianthi = new TimeSpan(0, pt.ThoiLuong / 60, pt.ThoiLuong % 60, 0);
                DateTime thoiluong = this.BatDau;
                if (thoiluong.Add(thoigianthi) < DateTime.Now)
                {
                    return "Đã quá thời gian làm bài";
                }
            }

            return "Chưa hoàn thành bài thi";
        }

        public bool hasLamBai()
        {
            var pt = db.PhongThis.FirstOrDefault(x => x.MaPhong == this.MaPhong);
            if (pt != null)
            {
                TimeSpan thoigianthi = new TimeSpan(0, pt.ThoiLuong / 60, pt.ThoiLuong % 60, 0);
                DateTime thoiluong = this.BatDau;
                if (thoiluong.Add(thoigianthi) > DateTime.Now)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
