﻿using System;
using System.Collections.Generic;

namespace DayHocTrucTuyen.Models.Entities
{
    public partial class PhieuBinhChon
    {
        public string MaPhieu { get; set; } = null!;
        public string MaNd { get; set; } = null!;
        public string ChucNang { get; set; } = null!;
        public int MucDo { get; set; }
        public string? GhiChu { get; set; }

        public virtual NguoiDung MaNdNavigation { get; set; } = null!;
    }
}
