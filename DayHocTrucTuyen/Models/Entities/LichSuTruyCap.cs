using System;
using System.Collections.Generic;

namespace DayHocTrucTuyen.Models.Entities
{
    public partial class LichSuTruyCap
    {
        public DateTime ThoiGian { get; set; }
        public string? MaNd { get; set; }
        public string? MaLop { get; set; }

        public virtual LopHoc? MaLopNavigation { get; set; }
        public virtual NguoiDung? MaNdNavigation { get; set; }
    }
}
