using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DayHocTrucTuyen.Models.Entities
{
    public partial class NguoiDung
    {
        public NguoiDung()
        {
            BaiDangs = new HashSet<BaiDang>();
            BaoCaos = new HashSet<BaoCao>();
            BiCamThis = new HashSet<BiCamThi>();
            BinhLuans = new HashSet<BinhLuan>();
            CamXucs = new HashSet<CamXuc>();
            CauTraLois = new HashSet<CauTraLoi>();
            DanhGiaLops = new HashSet<DanhGiaLop>();
            HocSinhThuocLops = new HashSet<HocSinhThuocLop>();
            LichSuGiaoDiches = new HashSet<LichSuGiaoDich>();
            LopHocs = new HashSet<LopHoc>();
            PhieuBinhChons = new HashSet<PhieuBinhChon>();
            ThichTrangNguoiDungNavigations = new HashSet<ThichTrang>();
            ThichTrangNguoiThichNavigations = new HashSet<ThichTrang>();
            ThoiGianLamBais = new HashSet<ThoiGianLamBai>();
            ThongBaos = new HashSet<ThongBao>();
            TinNhanNguoiGuiNavigations = new HashSet<TinNhan>();
            TinNhanNguoiNhanNavigations = new HashSet<TinNhan>();
            XemTrangNguoiDungNavigations = new HashSet<XemTrang>();
            XemTrangNguoiXemNavigations = new HashSet<XemTrang>();
        }

        public string MaNd { get; set; } = null!;
        public string MaLoai { get; set; } = null!;
        public string? HoLot { get; set; }
        public string Ten { get; set; } = null!;
        public DateTime? NgaySinh { get; set; }
        public int? GioiTinh { get; set; }
        public string? Sdt { get; set; }
        public string Email { get; set; } = null!;
        public string MatKhau { get; set; } = null!;
        public string? ImgAvt { get; set; }
        public string? ImgBg { get; set; }
        public string? ImgNhanDien { get; set; }
        public bool TrangThai { get; set; }
        public string? MoTa { get; set; }
        public DateTime NgayTao { get; set; }
        public string? BiDanh { get; set; }

        public virtual LoaiNd MaLoaiNavigation { get; set; } = null!;
        public virtual PheDuyet PheDuyet { get; set; } = null!;
        public virtual TrangThaiNangCap TrangThaiNangCap { get; set; } = null!;
        public virtual ViNguoiDung ViNguoiDung { get; set; } = null!;
        public virtual ICollection<BaiDang> BaiDangs { get; set; }
        public virtual ICollection<BaoCao> BaoCaos { get; set; }
        public virtual ICollection<BiCamThi> BiCamThis { get; set; }
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }
        public virtual ICollection<CamXuc> CamXucs { get; set; }
        public virtual ICollection<CauTraLoi> CauTraLois { get; set; }
        public virtual ICollection<DanhGiaLop> DanhGiaLops { get; set; }
        public virtual ICollection<HocSinhThuocLop> HocSinhThuocLops { get; set; }
        public virtual ICollection<LichSuGiaoDich> LichSuGiaoDiches { get; set; }
        public virtual ICollection<LopHoc> LopHocs { get; set; }
        public virtual ICollection<PhieuBinhChon> PhieuBinhChons { get; set; }
        public virtual ICollection<ThichTrang> ThichTrangNguoiDungNavigations { get; set; }
        public virtual ICollection<ThichTrang> ThichTrangNguoiThichNavigations { get; set; }
        public virtual ICollection<ThoiGianLamBai> ThoiGianLamBais { get; set; }
        public virtual ICollection<ThongBao> ThongBaos { get; set; }
        public virtual ICollection<TinNhan> TinNhanNguoiGuiNavigations { get; set; }
        public virtual ICollection<TinNhan> TinNhanNguoiNhanNavigations { get; set; }
        public virtual ICollection<XemTrang> XemTrangNguoiDungNavigations { get; set; }
        public virtual ICollection<XemTrang> XemTrangNguoiXemNavigations { get; set; }

        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();

        public NguoiDung(string ma_ND)
        {
            this.MaNd = ma_ND;
        }

        public string setMaUser()
        {
            NguoiDung nd = db.NguoiDungs.OrderByDescending(x => x.MaNd).FirstOrDefault();
            if (nd == null)
            {
                return "U000001";
            }
            int temp = int.Parse(Convert.ToString(nd.MaNd).Substring(1));
            string ma_user = "U" + Convert.ToString(1000000 + temp + 1).Substring(1);
            return ma_user;
        }

        public DateTime setNgayTao()
        {
            return DateTime.Now;
        }

        public string mahoaMatKhau(string pass)
        {
            MD5 mh = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(pass);
            byte[] hash = mh.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
        public string getImageAvt()
        {
            var nd = db.NguoiDungs.FirstOrDefault(x => x.MaNd == this.MaNd);
            if (String.IsNullOrEmpty(nd.ImgAvt)) return "/Content/Img/userAvt/avt-default.png";
            return "/Content/Img/userAvt/" + nd.ImgAvt;
        }

        public string getTenLoai()
        {
            var loai = db.LoaiNds.FirstOrDefault(x => x.MaLoai == this.MaLoai);
            if (loai.MaLoai.Equals("01")) return "Quản trị viên";

            return loai.TenLoai;
        }

        public string getFullName()
        {
            var user = db.NguoiDungs.FirstOrDefault(x => x.MaNd == this.MaNd);
            return user.HoLot + " " + user.Ten;
        }

        public string getName()
        {
            var user = db.NguoiDungs.FirstOrDefault(x => x.MaNd == this.MaNd);
            return user.Ten;
        }

        public string getImageBG()
        {
            var nd = db.NguoiDungs.FirstOrDefault(x => x.MaNd == this.MaNd);
            if (nd.ImgBg == null) return "/Content/Img/userBG/bg-default.jpg";
            if (nd.ImgAvt.ToLower().StartsWith("http")) return nd.ImgBg;
            return "/Content/Img/userBG/" + nd.ImgBg;
        }

        public int getTuoi(DateTime birthDate)
        {
            DateTime n = DateTime.Now;
            int age = n.Year - birthDate.Year;
            if (n.Month < birthDate.Month || (n.Month == birthDate.Month && n.Day < birthDate.Day))
                age--;
            return age;
        }
        public int getYeuThich()
        {
            return db.ThichTrangs.Where(x => x.NguoiDung == this.MaNd).Count();
        }
        public int getYeuThichTheoTuan()
        {
            return db.ThichTrangs.Where(x => x.NguoiDung == this.MaNd && x.ThoiGian.AddDays(7) >= DateTime.Now).Count();
        }
        public int getXemTrang()
        {
            return db.XemTrangs.Where(x => x.NguoiDung == this.MaNd).Count();
        }
        public int getXemTrangTheoTuan()
        {
            return db.XemTrangs.Where(x => x.NguoiDung == this.MaNd && x.ThoiGian.AddDays(7) >= DateTime.Now).Count();
        }
        public bool liked(string liker)
        {
            var liked = db.ThichTrangs.FirstOrDefault(x => x.NguoiDung == this.MaNd && x.NguoiThich == liker);
            if (liked == null) return false;
            return true;
        }
        public int getJoinRoom()
        {
            return db.HocSinhThuocLops.Where(x => x.MaNd == this.MaNd).Count();
        }
        public int getOwnRoom()
        {
            return db.LopHocs.Where(x => x.MaNd == this.MaNd).Count();
        }
        public int getPost()
        {
            return db.BaiDangs.Where(x => x.MaNd == this.MaNd).Count();
        }
        public int getComment()
        {
            return db.BinhLuans.Where(x => x.MaNd == this.MaNd).Count();
        }
        public int getReact()
        {
            return db.CamXucs.Where(x => x.MaNd == this.MaNd).Count();
        }
        public List<LopHoc> getListJoin()
        {
            var room = from c in db.LopHocs
                       join hs in db.HocSinhThuocLops on c.MaLop equals hs.MaLop
                       where hs.MaNd == this.MaNd
                       orderby hs.NgayThamGia descending
                       select c;
            return room.ToList();
        }

        public bool isUpgrade()
        {
            var upgrade = db.TrangThaiNangCaps.FirstOrDefault(x => x.MaNd == this.MaNd);
            if (upgrade != null)
            {
                var pak = db.GoiNangCaps.FirstOrDefault(x => x.MaGoi == upgrade.MaGoi);
                if (upgrade.NgayDangKy.AddDays(pak.HieuLuc * 30) > DateTime.Now)
                {
                    return true;
                }
            }
            return false;
        }

        public double getSoDu()
        {
            var vi = db.ViNguoiDungs.FirstOrDefault(x => x.MaNd == this.MaNd);
            if (vi != null)
            {
                return vi.SoDu;
            }

            return 0;
        }
        public List<NguoiDung> lstMemView()
        {
            List<NguoiDung> user = (from u in db.NguoiDungs
                                    join xt in db.XemTrangs on u.MaNd equals xt.NguoiXem
                                    where xt.NguoiDung == this.MaNd
                                    select u).ToList();
            return user.DistinctBy(x => x.MaNd).ToList();
        }
        public List<NguoiDung> lstMemLike()
        {
            List<NguoiDung> user = (from u in db.NguoiDungs
                                    join yt in db.ThichTrangs on u.MaNd equals yt.NguoiThich
                                    where yt.NguoiDung == this.MaNd
                                    select u).ToList();
            return user.DistinctBy(x => x.MaNd).ToList();
        }
    }
}
