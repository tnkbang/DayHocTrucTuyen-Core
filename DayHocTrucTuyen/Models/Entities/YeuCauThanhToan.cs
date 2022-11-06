using System;
using System.Collections.Generic;

namespace DayHocTrucTuyen.Models.Entities
{
    public partial class YeuCauThanhToan
    {
        public string MaNd { get; set; } = null!;
        public string LoaiThanhToan { get; set; } = null!;
        public string? SoTaiKhoan { get; set; }
        public double SoTien { get; set; }
        public int TrangThai { get; set; }
        public DateTime ThoiGian { get; set; }
        public string? GhiChu { get; set; }

        public virtual NguoiDung MaNdNavigation { get; set; } = null!;

        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();

        public NguoiDung getNguoiDung()
        {
            var nd = db.NguoiDungs.FirstOrDefault(x => x.MaNd == this.MaNd);
            return nd ?? new NguoiDung();
        }
    }
}
