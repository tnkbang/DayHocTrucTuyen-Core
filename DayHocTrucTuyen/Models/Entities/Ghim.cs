using System;
using System.Collections.Generic;

namespace DayHocTrucTuyen.Models.Entities
{
    public partial class Ghim
    {
        public string MaBai { get; set; } = null!;
        public DateTime ThoiGian { get; set; }

        public virtual BaiDang MaBaiNavigation { get; set; } = null!;
    }
}
