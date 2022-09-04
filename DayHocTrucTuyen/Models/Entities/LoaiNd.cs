using System;
using System.Collections.Generic;

namespace DayHocTrucTuyen.Models.Entities
{
    public partial class LoaiNd
    {
        public LoaiNd()
        {
            NguoiDungs = new HashSet<NguoiDung>();
        }

        public string MaLoai { get; set; } = null!;
        public string TenLoai { get; set; } = null!;

        public virtual ICollection<NguoiDung> NguoiDungs { get; set; }
    }
}
