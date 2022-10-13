using System;
using System.Collections.Generic;

namespace DayHocTrucTuyen.Models.Entities
{
    public partial class ThongBao
    {
        public string MaTb { get; set; } = null!;
        public string MaNd { get; set; } = null!;
        public string TieuDe { get; set; } = null!;
        public string? NoiDung { get; set; }
        public DateTime ThoiGian { get; set; }
        public bool TrangThai { get; set; }
        public string? LienKet { get; set; }

        public virtual NguoiDung MaNdNavigation { get; set; } = null!;

        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();

        public string setMa(string maND)
        {
            var tb = db.ThongBaos.Where(x => x.MaNd == maND);
            if (tb.Count() == 0) return "00001";

            string ma = Convert.ToString(100000 + tb.Count() + 1).Substring(1);
            return ma;
        }
        public List<ThongBao> getAllThongBao()
        {
            var tb = db.ThongBaos.Where(x => x.MaNd == this.MaNd).OrderByDescending(x => x.ThoiGian).ToList();
            return tb;
        }
        public List<ThongBao> getThongBaoChuaXem()
        {
            var tb = db.ThongBaos.Where(x => x.MaNd == this.MaNd && x.TrangThai == false).OrderByDescending(x => x.ThoiGian).ToList();
            return tb;
        }
        public List<ThongBao> getThongBaoDaXem()
        {
            var tb = db.ThongBaos.Where(x => x.MaNd == this.MaNd && x.TrangThai == true).OrderByDescending(x => x.ThoiGian).ToList();
            return tb;
        }
        public int getSLThongBao()
        {
            return db.ThongBaos.Where(x => x.MaNd == this.MaNd).Count();
        }
        public int getSLThongBaoChuaXem()
        {
            return db.ThongBaos.Where(x => x.MaNd == this.MaNd && x.TrangThai == false).Count();
        }
        public int getSLThongBaoDaXem()
        {
            return db.ThongBaos.Where(x => x.MaNd == this.MaNd && x.TrangThai == true).Count();
        }
    }
}
