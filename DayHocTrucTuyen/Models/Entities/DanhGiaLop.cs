using System;
using System.Collections.Generic;

namespace DayHocTrucTuyen.Models.Entities
{
    public partial class DanhGiaLop
    {
        public string MaDg { get; set; } = null!;
        public string? MaNd { get; set; }
        public string? MaLop { get; set; }
        public int MucDo { get; set; }
        public string? GhiChu { get; set; }
        public DateTime? ThoiGian { get; set; }

        public virtual LopHoc? MaLopNavigation { get; set; }
        public virtual NguoiDung? MaNdNavigation { get; set; }
    }
}
