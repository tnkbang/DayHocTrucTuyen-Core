using System;
using System.Collections.Generic;

namespace DayHocTrucTuyen.Models.Entities
{
    public partial class BinhLuan
    {
        public string MaBai { get; set; } = null!;
        public string MaNd { get; set; } = null!;
        public DateTime ThoiGian { get; set; }
        public string? NoiDung { get; set; }
        public string? DinhKem { get; set; }

        public virtual BaiDang MaBaiNavigation { get; set; } = null!;
        public virtual NguoiDung MaNdNavigation { get; set; } = null!;
    }
}
