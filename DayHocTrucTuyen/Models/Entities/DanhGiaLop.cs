using System;
using System.Collections.Generic;

namespace DayHocTrucTuyen.Models.Entities
{
    public partial class DanhGiaLop
    {
        public string MaDg { get; set; } = null!;
        public string? MaNd { get; set; }
        public string? MaLop { get; set; }
        public int MucDo { get; set; }
        public string? GhiChu { get; set; }
        public DateTime? ThoiGian { get; set; }

        public virtual LopHoc? MaLopNavigation { get; set; }
        public virtual NguoiDung? MaNdNavigation { get; set; }

        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();

        public string setMa()
        {
            var ma = "";
            do
            {
                ma = StringGenerator.Alphabet(15);
            } while (db.DanhGiaLops.FirstOrDefault(x => x.MaDg == ma) != null);

            return ma;
        }
    }
}
