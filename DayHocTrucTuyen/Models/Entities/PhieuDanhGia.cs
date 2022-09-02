using System;
using System.Collections.Generic;

namespace DayHocTrucTuyen.Models.Entities
{
    public partial class PhieuDanhGia
    {
        public string MaPhieu { get; set; } = null!;
        public string? MaNd { get; set; }
        public string ChucNang { get; set; } = null!;
        public int MucDo { get; set; }
        public string? GhiChu { get; set; }

        public virtual NguoiDung? MaNdNavigation { get; set; }
    }
}
