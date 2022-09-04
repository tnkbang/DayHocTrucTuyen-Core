using System;
using System.Collections.Generic;

namespace DayHocTrucTuyen.Models.Entities
{
    public partial class LichSuGiaoDich
    {
        public string MaNd { get; set; } = null!;
        public DateTime ThoiGian { get; set; }
        public bool ThuVao { get; set; }
        public double SoTien { get; set; }
        public double SoDu { get; set; }
        public string GhiChu { get; set; } = null!;

        public virtual NguoiDung MaNdNavigation { get; set; } = null!;
    }
}
