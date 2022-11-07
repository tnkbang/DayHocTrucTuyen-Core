using System;
using System.Collections.Generic;

namespace DayHocTrucTuyen.Models.Entities
{
    public partial class BaoCao
    {
        public string MaBaoCao { get; set; } = null!;
        public string? MaNd { get; set; }
        public string ChiMuc { get; set; } = null!;
        public string NoiDung { get; set; } = null!;
        public string? GhiChu { get; set; }
        public DateTime ThoiGian { get; set; }

        public virtual NguoiDung? MaNdNavigation { get; set; }

        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();

        public string setMa()
        {
            var ma = "";
            do
            {
                ma = StringGenerator.Alphabet(10);
            } while (db.BaoCaos.FirstOrDefault(x => x.MaBaoCao == ma) != null);

            return ma;
        }
        public NguoiDung getOwner()
        {
            var temp = db.NguoiDungs.FirstOrDefault(x => x.MaNd == this.MaNd);
            NguoiDung user = new NguoiDung();

            if (temp != null)
            {
                user.MaNd = temp.MaNd;
                user.HoLot = temp.HoLot;
                user.Ten = temp.Ten;
                user.ImgAvt = temp.ImgAvt;
                user.ImgBg = temp.ImgBg;
            }

            return user;
        }
        public LopHoc getRoom()
        {
            var maLop = this.ChiMuc.Substring(0, 11);
            var lp = db.LopHocs.FirstOrDefault(x => x.MaLop == maLop);
            return lp ?? new LopHoc();
        }
    }
}
