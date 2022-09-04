using System;
using System.Collections.Generic;

namespace DayHocTrucTuyen.Models.Entities
{
    public partial class GoiNangCap
    {
        public GoiNangCap()
        {
            TrangThaiNangCaps = new HashSet<TrangThaiNangCap>();
        }

        public int MaGoi { get; set; }
        public string TenGoi { get; set; } = null!;
        public double GiaTien { get; set; }
        public int HieuLuc { get; set; }
        public string? MoTa { get; set; }

        public virtual ICollection<TrangThaiNangCap> TrangThaiNangCaps { get; set; }
    }
}
