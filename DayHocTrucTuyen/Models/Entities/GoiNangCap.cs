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

        public string getHieuLuc()
        {
            if (this.HieuLuc < 12) return this.HieuLuc + " tháng";

            var phandu = this.HieuLuc % 12;
            if (phandu != 0) return this.HieuLuc / 12 + "," + phandu + " năm";
            else return this.HieuLuc / 12 + " năm";
        }
    }
}
