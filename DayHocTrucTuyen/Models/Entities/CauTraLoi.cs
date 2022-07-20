using System;
using System.Collections.Generic;

namespace DayHocTrucTuyen.Models.Entities
{
    public partial class CauTraLoi
    {
        public int Stt { get; set; }
        public string MaPhong { get; set; } = null!;
        public string MaNd { get; set; } = null!;
        public int LanThu { get; set; }
        public string DapAn { get; set; } = null!;

        public virtual CauHoiThi CauHoiThi { get; set; } = null!;
        public virtual NguoiDung MaNdNavigation { get; set; } = null!;
    }
}
