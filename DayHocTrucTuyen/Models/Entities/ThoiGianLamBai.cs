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
    }
}
