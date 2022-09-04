using System;
using System.Collections.Generic;

namespace DayHocTrucTuyen.Models.Entities
{
    public partial class ThichTrang
    {
        public string MaYt { get; set; } = null!;
        public string NguoiDung { get; set; } = null!;
        public string NguoiThich { get; set; } = null!;
        public DateTime ThoiGian { get; set; }

        public virtual NguoiDung NguoiDungNavigation { get; set; } = null!;
        public virtual NguoiDung NguoiThichNavigation { get; set; } = null!;

        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();
        public string setMa(string nd)
        {
            ThichTrang yt = db.ThichTrangs.Where(x => x.NguoiDung == nd).OrderByDescending(x => x.MaYt).FirstOrDefault();
            if (yt == null)
            {
                return nd + "_0000001";
            }
            int temp = int.Parse(Convert.ToString(yt.MaYt).Substring(8));
            string ma_xt = nd + "_" + Convert.ToString(10000000 + temp + 1).Substring(1);
            return ma_xt;
        }
    }
}
