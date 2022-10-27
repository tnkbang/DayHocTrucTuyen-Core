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
            } while (temp.MaBaoCao != null);

            return ma;
        }
        public NguoiDung getOwner()
        {
            NguoiDung temp = db.NguoiDungs.FirstOrDefault(x => x.MaNd == this.MaNd);
            NguoiDung user = new NguoiDung();

            user.MaNd = temp.MaNd;
            user.HoLot = temp.HoLot;
            user.Ten = temp.Ten;
            user.ImgAvt = temp.ImgAvt;
            user.ImgBg = temp.ImgBg;

            return user;
        }
        public LopHoc getRoom()
        {
            var maLop = this.ChiMuc.Substring(0, 11);
            var lp = db.LopHocs.FirstOrDefault(x => x.MaLop == maLop);
            return lp;
        }
    }
}
