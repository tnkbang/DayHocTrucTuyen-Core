using System;
using System.Collections.Generic;

namespace DayHocTrucTuyen.Models.Entities
{
    public partial class ViNguoiDung
    {
        public string MaNd { get; set; } = null!;
        public double SoDu { get; set; }
        public DateTime NgayMo { get; set; }
        public bool TrangThai { get; set; }

        public virtual NguoiDung MaNdNavigation { get; set; } = null!;
    }
}
