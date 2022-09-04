using System;
using System.Collections.Generic;

namespace DayHocTrucTuyen.Models.Entities
{
    public partial class XemTrang
    {
        public string MaXt { get; set; } = null!;
        public string NguoiDung { get; set; } = null!;
        public string NguoiXem { get; set; } = null!;
        public DateTime ThoiGian { get; set; }

        public virtual NguoiDung NguoiDungNavigation { get; set; } = null!;
        public virtual NguoiDung NguoiXemNavigation { get; set; } = null!;

        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();
        public string setMa(string nd)
        {
            XemTrang xt = db.XemTrangs.Where(x => x.NguoiDung == nd).OrderByDescending(x => x.MaXt).FirstOrDefault();
            if (xt == null)
            {
                return nd + "_0000001";
            }
            int temp = int.Parse(Convert.ToString(xt.MaXt).Substring(8));
            string ma_xt = nd + "_" + Convert.ToString(10000000 + temp + 1).Substring(1);
            return ma_xt;
        }
    }
}
