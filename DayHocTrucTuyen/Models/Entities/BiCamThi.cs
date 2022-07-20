using System;
using System.Collections.Generic;

namespace DayHocTrucTuyen.Models.Entities
{
    public partial class BiCamThi
    {
        public string MaNd { get; set; } = null!;
        public string MaPhong { get; set; } = null!;
        public string? LyDo { get; set; }

        public virtual NguoiDung MaNdNavigation { get; set; } = null!;
        public virtual PhongThi MaPhongNavigation { get; set; } = null!;
    }
}
