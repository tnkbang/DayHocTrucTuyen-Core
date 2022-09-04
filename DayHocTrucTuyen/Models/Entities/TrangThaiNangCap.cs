using System;
using System.Collections.Generic;

namespace DayHocTrucTuyen.Models.Entities
{
    public partial class TrangThaiNangCap
    {
        public string NguoiDung { get; set; } = null!;
        public int MaGoi { get; set; }
        public DateTime NgayDangKy { get; set; }

        public virtual GoiNangCap MaGoiNavigation { get; set; } = null!;
        public virtual NguoiDung NguoiDungNavigation { get; set; } = null!;
    }
}
