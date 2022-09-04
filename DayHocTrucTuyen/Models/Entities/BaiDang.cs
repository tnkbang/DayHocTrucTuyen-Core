using System;
using System.Collections.Generic;

namespace DayHocTrucTuyen.Models.Entities
{
    public partial class BaiDang
    {
        public BaiDang()
        {
            BinhLuans = new HashSet<BinhLuan>();
            CamXucs = new HashSet<CamXuc>();
        }

        public string MaBai { get; set; } = null!;
        public string MaLop { get; set; } = null!;
        public string MaNd { get; set; } = null!;
        public DateTime ThoiGian { get; set; }
        public string? NoiDung { get; set; }
        public string? DinhKem { get; set; }
        public bool TrangThai { get; set; }

        public virtual LopHoc MaLopNavigation { get; set; } = null!;
        public virtual NguoiDung MaNdNavigation { get; set; } = null!;
        public virtual Ghim Ghim { get; set; } = null!;
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }
        public virtual ICollection<CamXuc> CamXucs { get; set; }

        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();
        public string setMa(string maLop)
        {
            var last = (from b in db.BaiDangs
                        where b.MaLop == maLop
                        orderby b.MaBai descending
                        select b).FirstOrDefault();
            if (last == null)
            {
                return maLop + "-001";
            }
            int temp = int.Parse(Convert.ToString(last.MaBai).Substring(12));
            string ma = maLop + "-" + Convert.ToString(1000 + temp + 1).Substring(1);
            return ma;
        }
        public NguoiDung getOwner()
        {
            BaiDang post = db.BaiDangs.FirstOrDefault(x => x.MaBai == this.MaBai);
            NguoiDung temp = db.NguoiDungs.FirstOrDefault(x => x.MaNd == post.MaNd);
            NguoiDung user = new NguoiDung();

            user.MaNd = temp.MaNd;
            user.HoLot = temp.HoLot;
            user.Ten = temp.Ten;
            user.ImgAvt = temp.ImgAvt;
            user.ImgBg = temp.ImgBg;

            return user;
        }
        public bool isOwner(string maUser)
        {
            var own = db.BaiDangs.FirstOrDefault(x => x.MaNd == maUser && x.MaBai == this.MaBai);
            if (own != null) { return true; }

            return false;
        }
        public int getSLCamXuc()
        {
            var sl = db.CamXucs.Where(x => x.MaBai == this.MaBai).Count();
            return sl;
        }
        public List<NguoiDung> getMemsCamXuc()
        {
            var mem = from n in db.NguoiDungs
                      join u in db.CamXucs on n.MaNd equals u.MaNd
                      where u.MaBai == this.MaBai
                      orderby u.ThoiGian descending
                      select n;
            foreach (var m in mem)
            {
                m.Sdt = null;
                m.MatKhau = "";
                m.Email = "";
                m.ImgNhanDien = null;
            }
            return mem.ToList();
        }
        public int getSLBinhLuan()
        {
            var sl = db.BinhLuans.Where(x => x.MaBai == this.MaBai).Count();
            return sl;
        }
        public NguoiDung? getTTMember(string maND)
        {
            var user = db.NguoiDungs.FirstOrDefault(x => x.MaNd == maND);
            if (user != null)
            {
                user.Sdt = null;
                user.MatKhau = "";
                user.Email = "";
                user.ImgNhanDien = null;
            }

            return user;
        }
        public List<BinhLuan> getNDBinhLuan()
        {
            var bl = db.BinhLuans.Where(x => x.MaBai == this.MaBai);

            return bl.ToList();
        }
        public bool liked(string maND)
        {
            var liked = db.CamXucs.FirstOrDefault(x => x.MaNd == maND && x.MaBai == this.MaBai);
            if (liked == null) { return false; }
            return true;
        }
        public bool isGhim()
        {
            var ghim = db.Ghims.FirstOrDefault(x => x.MaBai == this.MaBai);
            if (ghim == null) { return false; }
            return true;
        }
    }
}
