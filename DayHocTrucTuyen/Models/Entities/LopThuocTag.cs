using System;
using System.Collections.Generic;

namespace DayHocTrucTuyen.Models.Entities
{
    public partial class LopThuocTag
    {
        public int Id { get; set; }
        public string MaTag { get; set; } = null!;
        public string MaLop { get; set; } = null!;

        public virtual LopHoc MaLopNavigation { get; set; } = null!;
        public virtual Tag MaTagNavigation { get; set; } = null!;
    }
}
