﻿using System;
using System.Collections.Generic;

namespace DayHocTrucTuyen.Models.Entities
{
    public partial class PheDuyet
    {
        public string MaNd { get; set; } = null!;
        public DateTime NgayDangKy { get; set; }
        public bool TrangThai { get; set; }
        public string GhiChu { get; set; } = null!;

        public virtual NguoiDung MaNdNavigation { get; set; } = null!;
    }
}
