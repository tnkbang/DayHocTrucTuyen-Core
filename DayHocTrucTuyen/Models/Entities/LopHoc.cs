using System;
using System.Collections.Generic;
using System.Text;

namespace DayHocTrucTuyen.Models.Entities
{
    public partial class LopHoc
    {
        public LopHoc()
        {
            BaiDangs = new HashSet<BaiDang>();
            DanhGiaLops = new HashSet<DanhGiaLop>();
            HocSinhThuocLops = new HashSet<HocSinhThuocLop>();
            LopThuocTags = new HashSet<LopThuocTag>();
            PhongThis = new HashSet<PhongThi>();
        }

        public string MaLop { get; set; } = null!;
        public string MaNd { get; set; } = null!;
        public DateTime NgayTao { get; set; }
        public string TenLop { get; set; } = null!;
        public string? BiDanh { get; set; }
        public double GiaTien { get; set; }
        public string? MoTa { get; set; }
        public bool TrangThai { get; set; }
        public string? ImgBia { get; set; }

        public virtual NguoiDung MaNdNavigation { get; set; } = null!;
        public virtual ICollection<BaiDang> BaiDangs { get; set; }
        public virtual ICollection<DanhGiaLop> DanhGiaLops { get; set; }
        public virtual ICollection<HocSinhThuocLop> HocSinhThuocLops { get; set; }
        public virtual ICollection<LopThuocTag> LopThuocTags { get; set; }
        public virtual ICollection<PhongThi> PhongThis { get; set; }

        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();
        private string randomString()
        {
            StringBuilder sb = new StringBuilder();
            char c;
            Random rand = new Random();
            for (int i = 0; i < 11; i++)
            {
                if (i == 3 || i == 7)
                {
                    sb.Append('-');
                }
                else
                {
                    c = Convert.ToChar(Convert.ToInt32(rand.Next(65, 87)));
                    sb.Append(c);
                }
            }
            return sb.ToString().ToLower();
        }
        public string setMa()
        {
            var ma = "";
            LopHoc temp;

            do
            {
                ma = randomString();
                temp = db.LopHocs.FirstOrDefault(x => x.MaLop == ma);
            } while (temp.MaNd != null);
            return ma;
        }
        public string getImage()
        {
            if (this.ImgBia == null) return "/Content/Img/roomCover/cover-default.jpg";
            return "/Content/Img/roomCover/" + this.ImgBia;
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
        public int getSLPost()
        {
            var sl = db.BaiDangs.Where(x => x.MaLop == this.MaLop).Count();
            return sl;
        }
        public List<BaiDang> getAllPost()
        {
            List<BaiDang> lst = new List<BaiDang>();
            var ghim = from p in db.BaiDangs
                       join g in db.Ghims on p.MaBai equals g.MaBai
                       where p.MaLop == this.MaLop
                       orderby g.ThoiGian descending
                       select p;
            lst.AddRange(ghim);
            var notghim = db.BaiDangs.Where(x => x.MaLop == this.MaLop).Except(ghim).OrderByDescending(x => x.ThoiGian);
            lst.AddRange(notghim);

            return lst;
        }
        public List<PhongThi> getAllPhongThi()
        {
            return db.PhongThis.Where(x => x.MaLop == this.MaLop).OrderByDescending(x => x.NgayTao).ToList();
        }
        public int getSLCamXuc()
        {
            var post = db.BaiDangs.Where(x => x.MaLop == this.MaLop).ToList();
            var count = 0;
            foreach (var p in post)
            {
                var a = db.CamXucs.Where(x => x.MaBai == p.MaBai).Count();
                count += a;
            }
            return count;
        }
        public int getMembers()
        {
            var sl = db.HocSinhThuocLops.Where(x => x.MaLop == this.MaLop).Count();
            return sl;
        }
        public List<Tag> getTag()
        {
            List<Tag> tags = (from t in db.Tags
                              join ltt in db.LopThuocTags on t.MaTag equals ltt.MaTag
                              where ltt.MaLop == this.MaLop
                              select t).ToList();
            return tags;
        }
        public bool isTag(string maTag)
        {
            var tags = db.LopThuocTags.FirstOrDefault(x => x.MaLop == this.MaLop && x.MaTag == maTag);
            if (tags != null) return true;
            return false;
        }
        public List<NguoiDung> listMembers()
        {
            var mem = from n in db.NguoiDungs
                      join hs in db.HocSinhThuocLops on n.MaNd equals hs.MaNd
                      where hs.MaLop == this.MaLop
                      orderby n.Ten
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
        public bool isOwner(string maUser)
        {
            var own = db.LopHocs.FirstOrDefault(x => x.MaNd == maUser && x.MaLop == this.MaLop);
            if (own != null) { return true; }

            return false;
        }
        public bool isMember(string maUser)
        {
            var mem = db.HocSinhThuocLops.FirstOrDefault(x => x.MaNd == maUser && x.MaLop == this.MaLop);
            if (mem != null) { return true; }

            return false;
        }
        public List<LopHoc> searchLopHoc(string maND, string tenlop)
        {
            List<LopHoc> lst = new List<LopHoc>();
            var thuoclop = from lh in db.LopHocs
                           join hstl in db.HocSinhThuocLops on lh.MaLop equals hstl.MaLop
                           where hstl.MaNd == maND && lh.TenLop.Contains(tenlop)
                           orderby lh.NgayTao descending
                           select lh;
            var sohuu = db.LopHocs.Where(x => x.MaNd == maND && x.TenLop.Contains(tenlop)).OrderByDescending(x => x.NgayTao);
            lst.AddRange(sohuu);
            lst.AddRange(thuoclop);

            return lst;
        }
    }
}
