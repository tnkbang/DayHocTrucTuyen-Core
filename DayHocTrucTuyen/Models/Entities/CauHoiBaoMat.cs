using System;
using System.Collections.Generic;

namespace DayHocTrucTuyen.Models.Entities
{
    public partial class CauHoiBaoMat
    {
        public int Stt { get; set; }
        public string MaPhong { get; set; } = null!;
        public string CauHoi { get; set; } = null!;
        public string LoiGiai { get; set; } = null!;
        public string DapAn { get; set; } = null!;

        public virtual PhongThi MaPhongNavigation { get; set; } = null!;
    }
}
