using System;
using System.Collections.Generic;

namespace DayHocTrucTuyen.Models.Entities
{
    public partial class LopThuocTag
    {
        public int Id { get; set; }
        public string? MaTag { get; set; }
        public string? MaLop { get; set; }

        public virtual LopHoc? MaLopNavigation { get; set; }
        public virtual Tag? MaTagNavigation { get; set; }
    }
}
