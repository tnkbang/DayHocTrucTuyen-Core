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

        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();

        public bool hasVisit(string maNd, string maLop)
        {
            var ls = db.LichSuTruyCaps.FirstOrDefault(x => x.MaNd == maNd && x.MaLop == maLop && x.ThoiGian.AddHours(1) > DateTime.Now);
            return ls != null;
        }
    }
}
