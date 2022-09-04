using System;
using System.Collections.Generic;

namespace DayHocTrucTuyen.Models.Entities
{
    public partial class BaoCao
    {
        public string MaBaoCao { get; set; } = null!;
        public string MaNd { get; set; } = null!;
        public string ChiMuc { get; set; } = null!;
        public string NoiDung { get; set; } = null!;
        public string? GhiChu { get; set; }
        public DateTime ThoiGian { get; set; }

        public virtual NguoiDung MaNdNavigation { get; set; } = null!;

        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();

        public string setMa()
        {
            var ma = "";
            BaoCao temp;

            do
            {
                ma = StringGenerator.Alphabet(10);
                temp = db.BaoCaos.FirstOrDefault(x => x.MaBaoCao == ma);
            } while (temp != null);

            return ma;
        }
    }
}
