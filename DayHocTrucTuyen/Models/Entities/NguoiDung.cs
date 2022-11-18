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
            LichSuTruyCaps = new HashSet<LichSuTruyCap>();
            LopHocs = new HashSet<LopHoc>();
            ThichTrangNguoiDungNavigations = new HashSet<ThichTrang>();
            ThichTrangNguoiThichNavigations = new HashSet<ThichTrang>();
            ThoiGianLamBais = new HashSet<ThoiGianLamBai>();
            ThongBaos = new HashSet<ThongBao>();
            TinNhanNguoiGuiNavigations = new HashSet<TinNhan>();
            TinNhanNguoiNhanNavigations = new HashSet<TinNhan>();
            XemTrangNguoiDungNavigations = new HashSet<XemTrang>();
            XemTrangNguoiXemNavigations = new HashSet<XemTrang>();
            YeuCauThanhToans = new HashSet<YeuCauThanhToan>();
        }

        public string MaNd { get; set; } = null!;
        public string? MaLoai { get; set; }
        public string? HoLot { get; set; }
        public string Ten { get; set; } = null!;
        public DateTime? NgaySinh { get; set; }
        public int? GioiTinh { get; set; }
        public string? Sdt { get; set; }
        public string Email { get; set; } = null!;
        public string MatKhau { get; set; } = null!;
        public string? ImgAvt { get; set; }
        public string? ImgBg { get; set; }
        public bool TrangThai { get; set; }
        public string? MoTa { get; set; }
        public DateTime NgayTao { get; set; }
        public string? BiDanh { get; set; }

        public virtual LoaiNd? MaLoaiNavigation { get; set; }
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
        public virtual ICollection<LichSuTruyCap> LichSuTruyCaps { get; set; }
        public virtual ICollection<LopHoc> LopHocs { get; set; }
        public virtual ICollection<ThichTrang> ThichTrangNguoiDungNavigations { get; set; }
        public virtual ICollection<ThichTrang> ThichTrangNguoiThichNavigations { get; set; }
        public virtual ICollection<ThoiGianLamBai> ThoiGianLamBais { get; set; }
        public virtual ICollection<ThongBao> ThongBaos { get; set; }
        public virtual ICollection<TinNhan> TinNhanNguoiGuiNavigations { get; set; }
        public virtual ICollection<TinNhan> TinNhanNguoiNhanNavigations { get; set; }
        public virtual ICollection<XemTrang> XemTrangNguoiDungNavigations { get; set; }
        public virtual ICollection<XemTrang> XemTrangNguoiXemNavigations { get; set; }
        public virtual ICollection<YeuCauThanhToan> YeuCauThanhToans { get; set; }

        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();

        public NguoiDung getNguoiDung(string ma)
        {
            var nd = db.NguoiDungs.FirstOrDefault(x => x.MaNd == ma);
            return nd ?? new NguoiDung();
        }

        public string setMaUser()
        {
            var nd = db.NguoiDungs.OrderByDescending(x => x.MaNd).FirstOrDefault();
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
            if (String.IsNullOrEmpty(this.ImgAvt)) return "/Content/Img/userAvt/avt-default.png";
            return "/Content/Img/userAvt/" + this.ImgAvt;
        }

        public string getTenLoai()
        {
            var loai = db.LoaiNds.FirstOrDefault(x => x.MaLoai == this.MaLoai);
            if (loai != null)
            {
                if (loai.MaLoai.Equals("01")) return "Quản trị viên";
                return loai.TenLoai;
            }
            return "Không xác định";
        }

        public string getFullName()
        {
            return this.HoLot + " " + this.Ten;
        }

        public string getName()
        {
            return this.Ten;
        }

        public string getImageBG()
        {
            if (this.ImgBg == null) return "/Content/Img/userBG/bg-default.jpg";
            if (this.ImgBg.ToLower().StartsWith("http")) return this.ImgBg;
            return "/Content/Img/userBG/" + this.ImgBg;
        }

        public int getTuoi()
        {
            DateTime n = DateTime.Now;
            DateTime birthDate = this.NgaySinh ?? DateTime.Now;
            int age = n.Year - birthDate.Year;
            if (n.Month < birthDate.Month || (n.Month == birthDate.Month && n.Day < birthDate.Day))
                age--;
            return age;
        }
        public string getGioiTinh()
        {
            if (this.GioiTinh == 1) return "Nam";
            if (this.GioiTinh == 2) return "Nữ";
            if (this.GioiTinh == 3) return "Khác";
            return "Chưa chọn giới tính";
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
                if (pak != null && upgrade.NgayDangKy.AddDays(pak.HieuLuc * 30) > DateTime.Now)
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
        public bool hasPheDuyet()
        {
            var user = db.PheDuyets.FirstOrDefault(x => x.MaNd == this.MaNd);
            if (user != null) return true;
            return false;
        }

        public bool disPheDuyet()
        {
            var user = db.PheDuyets.FirstOrDefault(x => x.MaNd == this.MaNd);
            if (user != null && !user.TrangThai) return true;
            return false;
        }

        public string getIntro()
        {
            if (this.MaLoai == "01") return "Người dùng đảm nhiệm vai trò quản lý toàn hệ thống. Có quyền quyết định cao nhất đối với tất cả thành viên của hệ thống.";
            if (this.MaLoai == "02") return "Lực lượng nòng cốt của hệ thống. Có thể tạo ra các bài thi và khóa học, từ đó thu về lợi nhuận cho bản thân và hệ thống.";
            return "Người dùng cơ bản của hệ thống, không có nhiều quyền lực đặc biệt nhưng là nguồn nhân tố chính giúp duy trì sự hoạt động của hệ thống.";
        }

        public bool hasCamThi(string maPhong)
        {
            var pt = db.BiCamThis.FirstOrDefault(x => x.MaNd == this.MaNd && x.MaPhong == maPhong);
            if (pt != null) return true;
            return false;
        }
    }
}
