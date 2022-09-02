using System;
using System.Collections.Generic;

namespace DayHocTrucTuyen.Models.Entities
{
    public partial class BaoCao
    {
        public string MaBaoCao { get; set; } = null!;
        public string? MaNd { get; set; }
        public string ChiMuc { get; set; } = null!;
        public string? NoiDung { get; set; }
        public string GhiChu { get; set; } = null!;
        public DateTime ThoiGian { get; set; }

        public virtual NguoiDung? MaNdNavigation { get; set; }
    }
}
