using System;
using System.Collections.Generic;

namespace DayHocTrucTuyen.Models.Entities
{
    public partial class Tag
    {
        public Tag()
        {
            LopThuocTags = new HashSet<LopThuocTag>();
        }

        public string MaTag { get; set; } = null!;
        public string TenTag { get; set; } = null!;

        public virtual ICollection<LopThuocTag> LopThuocTags { get; set; }
    }
}
