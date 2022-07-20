using System;
using System.Collections.Generic;

namespace DayHocTrucTuyen.Models.Entities
{
    public partial class HocSinhThuocLop
    {
        public string MaNd { get; set; } = null!;
        public string MaLop { get; set; } = null!;
        public DateTime? NgayThamGia { get; set; }

        public virtual LopHoc MaLopNavigation { get; set; } = null!;
        public virtual NguoiDung MaNdNavigation { get; set; } = null!;
    }
}
