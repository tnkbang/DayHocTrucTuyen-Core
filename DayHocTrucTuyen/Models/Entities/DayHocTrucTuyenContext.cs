using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DayHocTrucTuyen.Models.Entities
{
    public partial class DayHocTrucTuyenContext : DbContext
    {
        public DayHocTrucTuyenContext()
        {
        }

        public DayHocTrucTuyenContext(DbContextOptions<DayHocTrucTuyenContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BaiDang> BaiDangs { get; set; } = null!;
        public virtual DbSet<BaoCao> BaoCaos { get; set; } = null!;
        public virtual DbSet<BiCamThi> BiCamThis { get; set; } = null!;
        public virtual DbSet<BinhLuan> BinhLuans { get; set; } = null!;
        public virtual DbSet<CamXuc> CamXucs { get; set; } = null!;
        public virtual DbSet<CauHoiThi> CauHoiThis { get; set; } = null!;
        public virtual DbSet<CauTraLoi> CauTraLois { get; set; } = null!;
        public virtual DbSet<DanhGiaLop> DanhGiaLops { get; set; } = null!;
        public virtual DbSet<Ghim> Ghims { get; set; } = null!;
        public virtual DbSet<GoiNangCap> GoiNangCaps { get; set; } = null!;
        public virtual DbSet<HocSinhThuocLop> HocSinhThuocLops { get; set; } = null!;
        public virtual DbSet<LichSuGiaoDich> LichSuGiaoDiches { get; set; } = null!;
        public virtual DbSet<LoaiNd> LoaiNds { get; set; } = null!;
        public virtual DbSet<LopHoc> LopHocs { get; set; } = null!;
        public virtual DbSet<LopThuocTag> LopThuocTags { get; set; } = null!;
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; } = null!;
        public virtual DbSet<PhieuBinhChon> PhieuBinhChons { get; set; } = null!;
        public virtual DbSet<PhongThi> PhongThis { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;
        public virtual DbSet<ThichTrang> ThichTrangs { get; set; } = null!;
        public virtual DbSet<ThoiGianLamBai> ThoiGianLamBais { get; set; } = null!;
        public virtual DbSet<ThongBao> ThongBaos { get; set; } = null!;
        public virtual DbSet<TinNhan> TinNhans { get; set; } = null!;
        public virtual DbSet<TrangThaiNangCap> TrangThaiNangCaps { get; set; } = null!;
        public virtual DbSet<ViNguoiDung> ViNguoiDungs { get; set; } = null!;
        public virtual DbSet<XemTrang> XemTrangs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = WebApplication.CreateBuilder();
                optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DayHocTrucTuyen"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaiDang>(entity =>
            {
                entity.HasKey(e => e.MaBai)
                    .HasName("PK__BaiDang__3A5539EFB14C8D15");

                entity.ToTable("BaiDang");

                entity.Property(e => e.MaBai)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Ma_Bai")
                    .IsFixedLength();

                entity.Property(e => e.DinhKem)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("Dinh_Kem");

                entity.Property(e => e.MaLop)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("Ma_Lop")
                    .IsFixedLength();

                entity.Property(e => e.MaNd)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Ma_ND")
                    .IsFixedLength();

                entity.Property(e => e.NoiDung)
                    .HasMaxLength(500)
                    .HasColumnName("Noi_Dung");

                entity.Property(e => e.ThoiGian)
                    .HasColumnType("datetime")
                    .HasColumnName("Thoi_Gian");

                entity.Property(e => e.TrangThai).HasColumnName("Trang_Thai");

                entity.HasOne(d => d.MaLopNavigation)
                    .WithMany(p => p.BaiDangs)
                    .HasForeignKey(d => d.MaLop)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BaiDang__Ma_Lop__5BE2A6F2");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.BaiDangs)
                    .HasForeignKey(d => d.MaNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BaiDang__Ma_ND__5CD6CB2B");
            });

            modelBuilder.Entity<BaoCao>(entity =>
            {
                entity.HasKey(e => e.MaBaoCao)
                    .HasName("PK__BaoCao__5FC87B6ECAD206D1");

                entity.ToTable("BaoCao");

                entity.Property(e => e.MaBaoCao)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Ma_Bao_Cao")
                    .IsFixedLength();

                entity.Property(e => e.ChiMuc)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Chi_Muc");

                entity.Property(e => e.GhiChu)
                    .HasMaxLength(500)
                    .HasColumnName("Ghi_Chu");

                entity.Property(e => e.MaNd)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Ma_ND")
                    .IsFixedLength();

                entity.Property(e => e.NoiDung)
                    .HasMaxLength(150)
                    .HasColumnName("Noi_Dung");

                entity.Property(e => e.ThoiGian)
                    .HasColumnType("datetime")
                    .HasColumnName("Thoi_Gian");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.BaoCaos)
                    .HasForeignKey(d => d.MaNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BaoCao__Ma_ND__4222D4EF");
            });

            modelBuilder.Entity<BiCamThi>(entity =>
            {
                entity.HasKey(e => new { e.MaNd, e.MaPhong })
                    .HasName("PK__BiCamThi__015410DDCD85D8ED");

                entity.ToTable("BiCamThi");

                entity.Property(e => e.MaNd)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Ma_ND")
                    .IsFixedLength();

                entity.Property(e => e.MaPhong)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Ma_Phong")
                    .IsFixedLength();

                entity.Property(e => e.LyDo)
                    .HasMaxLength(100)
                    .HasColumnName("Ly_Do");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.BiCamThis)
                    .HasForeignKey(d => d.MaNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BiCamThi__Ma_ND__6D0D32F4");

                entity.HasOne(d => d.MaPhongNavigation)
                    .WithMany(p => p.BiCamThis)
                    .HasForeignKey(d => d.MaPhong)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BiCamThi__Ma_Pho__6E01572D");
            });

            modelBuilder.Entity<BinhLuan>(entity =>
            {
                entity.HasKey(e => new { e.MaBai, e.MaNd, e.ThoiGian })
                    .HasName("PK__BinhLuan__4176DE91F2079FD0");

                entity.ToTable("BinhLuan");

                entity.Property(e => e.MaBai)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Ma_Bai")
                    .IsFixedLength();

                entity.Property(e => e.MaNd)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Ma_ND")
                    .IsFixedLength();

                entity.Property(e => e.ThoiGian)
                    .HasColumnType("datetime")
                    .HasColumnName("Thoi_Gian");

                entity.Property(e => e.DinhKem)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("Dinh_Kem");

                entity.Property(e => e.NoiDung)
                    .HasMaxLength(500)
                    .HasColumnName("Noi_Dung");

                entity.HasOne(d => d.MaBaiNavigation)
                    .WithMany(p => p.BinhLuans)
                    .HasForeignKey(d => d.MaBai)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BinhLuan__Ma_Bai__628FA481");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.BinhLuans)
                    .HasForeignKey(d => d.MaNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BinhLuan__Ma_ND__6383C8BA");
            });

            modelBuilder.Entity<CamXuc>(entity =>
            {
                entity.HasKey(e => new { e.MaNd, e.MaBai })
                    .HasName("PK__CamXuc__CDC7DE286EFB90B7");

                entity.ToTable("CamXuc");

                entity.Property(e => e.MaNd)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Ma_ND")
                    .IsFixedLength();

                entity.Property(e => e.MaBai)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Ma_Bai")
                    .IsFixedLength();

                entity.Property(e => e.ThoiGian)
                    .HasColumnType("datetime")
                    .HasColumnName("Thoi_Gian");

                entity.HasOne(d => d.MaBaiNavigation)
                    .WithMany(p => p.CamXucs)
                    .HasForeignKey(d => d.MaBai)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CamXuc__Ma_Bai__6754599E");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.CamXucs)
                    .HasForeignKey(d => d.MaNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CamXuc__Ma_ND__66603565");
            });

            modelBuilder.Entity<CauHoiThi>(entity =>
            {
                entity.HasKey(e => new { e.Stt, e.MaPhong })
                    .HasName("PK__CauHoiTh__E5282BFB098E6BB9");

                entity.ToTable("CauHoiThi");

                entity.Property(e => e.Stt).HasColumnName("STT");

                entity.Property(e => e.MaPhong)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Ma_Phong")
                    .IsFixedLength();

                entity.Property(e => e.CauHoi)
                    .HasMaxLength(500)
                    .HasColumnName("Cau_Hoi");

                entity.Property(e => e.DapAn)
                    .HasMaxLength(500)
                    .HasColumnName("Dap_An");

                entity.Property(e => e.LoiGiai)
                    .HasMaxLength(100)
                    .HasColumnName("Loi_Giai");

                entity.HasOne(d => d.MaPhongNavigation)
                    .WithMany(p => p.CauHoiThis)
                    .HasForeignKey(d => d.MaPhong)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CauHoiThi__Ma_Ph__70DDC3D8");
            });

            modelBuilder.Entity<CauTraLoi>(entity =>
            {
                entity.HasKey(e => new { e.Stt, e.MaPhong, e.MaNd, e.LanThu })
                    .HasName("PK__CauTraLo__1FE83402F3E8FE19");

                entity.ToTable("CauTraLoi");

                entity.Property(e => e.Stt).HasColumnName("STT");

                entity.Property(e => e.MaPhong)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Ma_Phong")
                    .IsFixedLength();

                entity.Property(e => e.MaNd)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Ma_ND")
                    .IsFixedLength();

                entity.Property(e => e.LanThu).HasColumnName("Lan_Thu");

                entity.Property(e => e.DapAn)
                    .HasMaxLength(500)
                    .HasColumnName("Dap_An");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.CauTraLois)
                    .HasForeignKey(d => d.MaNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CauTraLoi__Ma_ND__73BA3083");

                entity.HasOne(d => d.CauHoiThi)
                    .WithMany(p => p.CauTraLois)
                    .HasForeignKey(d => new { d.Stt, d.MaPhong })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CauTraLoi__74AE54BC");
            });

            modelBuilder.Entity<DanhGiaLop>(entity =>
            {
                entity.HasKey(e => e.MaDg)
                    .HasName("PK__DanhGiaL__2E67451CB849D4CE");

                entity.ToTable("DanhGiaLop");

                entity.Property(e => e.MaDg)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Ma_DG")
                    .IsFixedLength();

                entity.Property(e => e.GhiChu)
                    .HasMaxLength(500)
                    .HasColumnName("Ghi_Chu");

                entity.Property(e => e.MaLop)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("Ma_Lop")
                    .IsFixedLength();

                entity.Property(e => e.MaNd)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Ma_ND")
                    .IsFixedLength();

                entity.Property(e => e.MucDo).HasColumnName("Muc_Do");

                entity.Property(e => e.ThoiGian)
                    .HasColumnType("datetime")
                    .HasColumnName("Thoi_Gian");

                entity.HasOne(d => d.MaLopNavigation)
                    .WithMany(p => p.DanhGiaLops)
                    .HasForeignKey(d => d.MaLop)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DanhGiaLo__Ma_Lo__5535A963");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.DanhGiaLops)
                    .HasForeignKey(d => d.MaNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DanhGiaLo__Ma_ND__5441852A");
            });

            modelBuilder.Entity<Ghim>(entity =>
            {
                entity.HasKey(e => e.MaBai)
                    .HasName("PK__Ghim__3A5539EF44C0FCB4");

                entity.ToTable("Ghim");

                entity.Property(e => e.MaBai)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Ma_Bai")
                    .IsFixedLength();

                entity.Property(e => e.ThoiGian)
                    .HasColumnType("datetime")
                    .HasColumnName("Thoi_Gian");

                entity.HasOne(d => d.MaBaiNavigation)
                    .WithOne(p => p.Ghim)
                    .HasForeignKey<Ghim>(d => d.MaBai)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ghim__Ma_Bai__5FB337D6");
            });

            modelBuilder.Entity<GoiNangCap>(entity =>
            {
                entity.HasKey(e => e.MaGoi)
                    .HasName("PK__GoiNangC__3D0F9148C1636935");

                entity.ToTable("GoiNangCap");

                entity.Property(e => e.MaGoi)
                    .ValueGeneratedNever()
                    .HasColumnName("Ma_Goi");

                entity.Property(e => e.GiaTien).HasColumnName("Gia_Tien");

                entity.Property(e => e.HieuLuc).HasColumnName("Hieu_Luc");

                entity.Property(e => e.MoTa)
                    .HasMaxLength(100)
                    .HasColumnName("Mo_Ta");

                entity.Property(e => e.TenGoi)
                    .HasMaxLength(10)
                    .HasColumnName("Ten_Goi");
            });

            modelBuilder.Entity<HocSinhThuocLop>(entity =>
            {
                entity.HasKey(e => new { e.MaNd, e.MaLop })
                    .HasName("PK__HocSinhT__E2596BF51FB99C9B");

                entity.ToTable("HocSinhThuocLop");

                entity.Property(e => e.MaNd)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Ma_ND")
                    .IsFixedLength();

                entity.Property(e => e.MaLop)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("Ma_Lop")
                    .IsFixedLength();

                entity.Property(e => e.NgayThamGia)
                    .HasColumnType("datetime")
                    .HasColumnName("Ngay_Tham_Gia");

                entity.HasOne(d => d.MaLopNavigation)
                    .WithMany(p => p.HocSinhThuocLops)
                    .HasForeignKey(d => d.MaLop)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HocSinhTh__Ma_Lo__59063A47");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.HocSinhThuocLops)
                    .HasForeignKey(d => d.MaNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HocSinhTh__Ma_ND__5812160E");
            });

            modelBuilder.Entity<LichSuGiaoDich>(entity =>
            {
                entity.HasKey(e => new { e.MaNd, e.ThoiGian })
                    .HasName("PK__LichSuGi__B23E77E67701E777");

                entity.ToTable("LichSuGiaoDich");

                entity.Property(e => e.MaNd)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Ma_ND")
                    .IsFixedLength();

                entity.Property(e => e.ThoiGian)
                    .HasColumnType("datetime")
                    .HasColumnName("Thoi_Gian");

                entity.Property(e => e.GhiChu)
                    .HasMaxLength(200)
                    .HasColumnName("Ghi_Chu");

                entity.Property(e => e.SoDu).HasColumnName("So_Du");

                entity.Property(e => e.SoTien).HasColumnName("So_Tien");

                entity.Property(e => e.ThuVao).HasColumnName("Thu_Vao");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.LichSuGiaoDiches)
                    .HasForeignKey(d => d.MaNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LichSuGia__Ma_ND__2E1BDC42");
            });

            modelBuilder.Entity<LoaiNd>(entity =>
            {
                entity.HasKey(e => e.MaLoai)
                    .HasName("PK__LoaiND__586312F9AEE2CC99");

                entity.ToTable("LoaiND");

                entity.Property(e => e.MaLoai)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("Ma_Loai")
                    .IsFixedLength();

                entity.Property(e => e.TenLoai)
                    .HasMaxLength(10)
                    .HasColumnName("Ten_Loai");
            });

            modelBuilder.Entity<LopHoc>(entity =>
            {
                entity.HasKey(e => e.MaLop)
                    .HasName("PK__LopHoc__C3BE643DFC53DDE3");

                entity.ToTable("LopHoc");

                entity.HasIndex(e => e.BiDanh, "UQ__LopHoc__7F28B28BA8164951")
                    .IsUnique();

                entity.Property(e => e.MaLop)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("Ma_Lop")
                    .IsFixedLength();

                entity.Property(e => e.BiDanh)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Bi_Danh");

                entity.Property(e => e.GiaTien).HasColumnName("Gia_Tien");

                entity.Property(e => e.ImgBia)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Img_Bia");

                entity.Property(e => e.MaNd)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Ma_ND")
                    .IsFixedLength();

                entity.Property(e => e.MoTa)
                    .HasMaxLength(300)
                    .HasColumnName("Mo_Ta");

                entity.Property(e => e.NgayTao)
                    .HasColumnType("datetime")
                    .HasColumnName("Ngay_Tao");

                entity.Property(e => e.TenLop)
                    .HasMaxLength(40)
                    .HasColumnName("Ten_Lop");

                entity.Property(e => e.TrangThai).HasColumnName("Trang_Thai");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.LopHocs)
                    .HasForeignKey(d => d.MaNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LopHoc__Ma_ND__4BAC3F29");
            });

            modelBuilder.Entity<LopThuocTag>(entity =>
            {
                entity.ToTable("LopThuocTag");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MaLop)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("Ma_Lop")
                    .IsFixedLength();

                entity.Property(e => e.MaTag)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("Ma_Tag")
                    .IsFixedLength();

                entity.HasOne(d => d.MaLopNavigation)
                    .WithMany(p => p.LopThuocTags)
                    .HasForeignKey(d => d.MaLop)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LopThuocT__Ma_Lo__5165187F");

                entity.HasOne(d => d.MaTagNavigation)
                    .WithMany(p => p.LopThuocTags)
                    .HasForeignKey(d => d.MaTag)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LopThuocT__Ma_Ta__5070F446");
            });

            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.HasKey(e => e.MaNd)
                    .HasName("PK__NguoiDun__2E628DB6DBFA4C73");

                entity.ToTable("NguoiDung");

                entity.HasIndex(e => e.BiDanh, "UQ__NguoiDun__7F28B28B545FEC03")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__NguoiDun__A9D10534754D074A")
                    .IsUnique();

                entity.Property(e => e.MaNd)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Ma_ND")
                    .IsFixedLength();

                entity.Property(e => e.BiDanh)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Bi_Danh");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GioiTinh).HasColumnName("Gioi_Tinh");

                entity.Property(e => e.HoLot)
                    .HasMaxLength(20)
                    .HasColumnName("Ho_Lot");

                entity.Property(e => e.ImgAvt)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Img_Avt");

                entity.Property(e => e.ImgBg)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Img_BG");

                entity.Property(e => e.ImgNhanDien)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Img_Nhan_Dien");

                entity.Property(e => e.MaLoai)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("Ma_Loai")
                    .IsFixedLength();

                entity.Property(e => e.MatKhau)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("Mat_Khau");

                entity.Property(e => e.MoTa)
                    .HasMaxLength(300)
                    .HasColumnName("Mo_Ta");

                entity.Property(e => e.NgaySinh)
                    .HasColumnType("datetime")
                    .HasColumnName("Ngay_Sinh");

                entity.Property(e => e.NgayTao)
                    .HasColumnType("datetime")
                    .HasColumnName("Ngay_Tao");

                entity.Property(e => e.Sdt)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("SDT");

                entity.Property(e => e.Ten).HasMaxLength(7);

                entity.Property(e => e.TrangThai).HasColumnName("Trang_Thai");

                entity.HasOne(d => d.MaLoaiNavigation)
                    .WithMany(p => p.NguoiDungs)
                    .HasForeignKey(d => d.MaLoai)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__NguoiDung__Ma_Lo__286302EC");
            });

            modelBuilder.Entity<PhieuBinhChon>(entity =>
            {
                entity.HasKey(e => e.MaPhieu)
                    .HasName("PK__PhieuBin__1568CAA4135BD573");

                entity.ToTable("PhieuBinhChon");

                entity.Property(e => e.MaPhieu)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("Ma_Phieu")
                    .IsFixedLength();

                entity.Property(e => e.ChucNang)
                    .HasMaxLength(30)
                    .HasColumnName("Chuc_Nang");

                entity.Property(e => e.GhiChu)
                    .HasMaxLength(500)
                    .HasColumnName("Ghi_Chu");

                entity.Property(e => e.MaNd)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Ma_ND")
                    .IsFixedLength();

                entity.Property(e => e.MucDo).HasColumnName("Muc_Do");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.PhieuBinhChons)
                    .HasForeignKey(d => d.MaNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PhieuBinh__Ma_ND__47DBAE45");
            });

            modelBuilder.Entity<PhongThi>(entity =>
            {
                entity.HasKey(e => e.MaPhong)
                    .HasName("PK__PhongThi__F369D6B38B8F76F4");

                entity.ToTable("PhongThi");

                entity.Property(e => e.MaPhong)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Ma_Phong")
                    .IsFixedLength();

                entity.Property(e => e.LuotThi).HasColumnName("Luot_Thi");

                entity.Property(e => e.MaLop)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("Ma_Lop")
                    .IsFixedLength();

                entity.Property(e => e.MatKhau)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Mat_Khau");

                entity.Property(e => e.NgayDong)
                    .HasColumnType("datetime")
                    .HasColumnName("Ngay_Dong");

                entity.Property(e => e.NgayMo)
                    .HasColumnType("datetime")
                    .HasColumnName("Ngay_Mo");

                entity.Property(e => e.NgayTao)
                    .HasColumnType("datetime")
                    .HasColumnName("Ngay_Tao");

                entity.Property(e => e.TenPhong)
                    .HasMaxLength(50)
                    .HasColumnName("Ten_Phong");

                entity.Property(e => e.ThoiLuong).HasColumnName("Thoi_Luong");

                entity.Property(e => e.XemLai).HasColumnName("Xem_Lai");

                entity.HasOne(d => d.MaLopNavigation)
                    .WithMany(p => p.PhongThis)
                    .HasForeignKey(d => d.MaLop)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PhongThi__Ma_Lop__6A30C649");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => e.MaTag)
                    .HasName("PK__Tag__C1AE337AA99FFDB3");

                entity.ToTable("Tag");

                entity.Property(e => e.MaTag)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("Ma_Tag")
                    .IsFixedLength();

                entity.Property(e => e.TenTag)
                    .HasMaxLength(30)
                    .HasColumnName("Ten_Tag");
            });

            modelBuilder.Entity<ThichTrang>(entity =>
            {
                entity.HasKey(e => e.MaYt)
                    .HasName("PK__ThichTra__2E62A20F0055BD0E");

                entity.ToTable("ThichTrang");

                entity.Property(e => e.MaYt)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Ma_YT")
                    .IsFixedLength();

                entity.Property(e => e.NguoiDung)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Nguoi_Dung")
                    .IsFixedLength();

                entity.Property(e => e.NguoiThich)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Nguoi_Thich")
                    .IsFixedLength();

                entity.Property(e => e.ThoiGian)
                    .HasColumnType("datetime")
                    .HasColumnName("Thoi_Gian");

                entity.HasOne(d => d.NguoiDungNavigation)
                    .WithMany(p => p.ThichTrangNguoiDungNavigations)
                    .HasForeignKey(d => d.NguoiDung)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ThichTran__Nguoi__34C8D9D1");

                entity.HasOne(d => d.NguoiThichNavigation)
                    .WithMany(p => p.ThichTrangNguoiThichNavigations)
                    .HasForeignKey(d => d.NguoiThich)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ThichTran__Nguoi__35BCFE0A");
            });

            modelBuilder.Entity<ThoiGianLamBai>(entity =>
            {
                entity.HasKey(e => new { e.MaNd, e.MaPhong, e.LanThu })
                    .HasName("PK__ThoiGian__DFB3C7991C96C7EE");

                entity.ToTable("ThoiGianLamBai");

                entity.Property(e => e.MaNd)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Ma_ND")
                    .IsFixedLength();

                entity.Property(e => e.MaPhong)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Ma_Phong")
                    .IsFixedLength();

                entity.Property(e => e.LanThu).HasColumnName("Lan_Thu");

                entity.Property(e => e.BatDau)
                    .HasColumnType("datetime")
                    .HasColumnName("Bat_Dau");

                entity.Property(e => e.KetThuc)
                    .HasColumnType("datetime")
                    .HasColumnName("Ket_Thuc");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.ThoiGianLamBais)
                    .HasForeignKey(d => d.MaNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ThoiGianL__Ma_ND__778AC167");

                entity.HasOne(d => d.MaPhongNavigation)
                    .WithMany(p => p.ThoiGianLamBais)
                    .HasForeignKey(d => d.MaPhong)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ThoiGianL__Ma_Ph__787EE5A0");
            });

            modelBuilder.Entity<ThongBao>(entity =>
            {
                entity.HasKey(e => new { e.MaTb, e.MaNd })
                    .HasName("PK__ThongBao__5C84D3AED6F0204F");

                entity.ToTable("ThongBao");

                entity.Property(e => e.MaTb)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("Ma_TB")
                    .IsFixedLength();

                entity.Property(e => e.MaNd)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Ma_ND")
                    .IsFixedLength();

                entity.Property(e => e.LienKet)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Lien_Ket");

                entity.Property(e => e.NoiDung)
                    .HasMaxLength(500)
                    .HasColumnName("Noi_Dung");

                entity.Property(e => e.ThoiGian)
                    .HasColumnType("datetime")
                    .HasColumnName("Thoi_Gian");

                entity.Property(e => e.TieuDe)
                    .HasMaxLength(20)
                    .HasColumnName("Tieu_De");

                entity.Property(e => e.TrangThai).HasColumnName("Trang_Thai");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.ThongBaos)
                    .HasForeignKey(d => d.MaNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ThongBao__Ma_ND__44FF419A");
            });

            modelBuilder.Entity<TinNhan>(entity =>
            {
                entity.ToTable("TinNhan");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.NguoiGui)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Nguoi_Gui")
                    .IsFixedLength();

                entity.Property(e => e.NguoiNhan)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Nguoi_Nhan")
                    .IsFixedLength();

                entity.Property(e => e.NoiDung)
                    .HasMaxLength(500)
                    .HasColumnName("Noi_Dung");

                entity.Property(e => e.ThoiGian)
                    .HasColumnType("datetime")
                    .HasColumnName("Thoi_Gian");

                entity.Property(e => e.TrangThai).HasColumnName("Trang_Thai");

                entity.HasOne(d => d.NguoiGuiNavigation)
                    .WithMany(p => p.TinNhanNguoiGuiNavigations)
                    .HasForeignKey(d => d.NguoiGui)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TinNhan__Nguoi_G__30F848ED");

                entity.HasOne(d => d.NguoiNhanNavigation)
                    .WithMany(p => p.TinNhanNguoiNhanNavigations)
                    .HasForeignKey(d => d.NguoiNhan)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TinNhan__Nguoi_N__31EC6D26");
            });

            modelBuilder.Entity<TrangThaiNangCap>(entity =>
            {
                entity.HasKey(e => e.MaNd)
                    .HasName("PK__TrangTha__F5150F312CF119B9");

                entity.ToTable("TrangThaiNangCap");

                entity.Property(e => e.MaNd)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Ma_ND")
                    .IsFixedLength();

                entity.Property(e => e.MaGoi).HasColumnName("Ma_Goi");

                entity.Property(e => e.NgayDangKy)
                    .HasColumnType("datetime")
                    .HasColumnName("Ngay_Dang_Ky");

                entity.HasOne(d => d.MaGoiNavigation)
                    .WithMany(p => p.TrangThaiNangCaps)
                    .HasForeignKey(d => d.MaGoi)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TrangThai__Ma_Go__3F466844");

                entity.HasOne(d => d.NguoiDungNavigation)
                    .WithOne(p => p.TrangThaiNangCap)
                    .HasForeignKey<TrangThaiNangCap>(d => d.MaNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TrangThai__Nguoi__3E52440B");
            });

            modelBuilder.Entity<ViNguoiDung>(entity =>
            {
                entity.HasKey(e => e.MaNd)
                    .HasName("PK__ViNguoiD__2E628DB67F71D270");

                entity.ToTable("ViNguoiDung");

                entity.Property(e => e.MaNd)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Ma_ND")
                    .IsFixedLength();

                entity.Property(e => e.NgayMo)
                    .HasColumnType("datetime")
                    .HasColumnName("Ngay_Mo");

                entity.Property(e => e.SoDu).HasColumnName("So_Du");

                entity.Property(e => e.TrangThai).HasColumnName("Trang_Thai");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithOne(p => p.ViNguoiDung)
                    .HasForeignKey<ViNguoiDung>(d => d.MaNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ViNguoiDu__Ma_ND__2B3F6F97");
            });

            modelBuilder.Entity<XemTrang>(entity =>
            {
                entity.HasKey(e => e.MaXt)
                    .HasName("PK__XemTrang__2E62DAEEFCC1A6F0");

                entity.ToTable("XemTrang");

                entity.Property(e => e.MaXt)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Ma_XT")
                    .IsFixedLength();

                entity.Property(e => e.NguoiDung)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Nguoi_Dung")
                    .IsFixedLength();

                entity.Property(e => e.NguoiXem)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Nguoi_Xem")
                    .IsFixedLength();

                entity.Property(e => e.ThoiGian)
                    .HasColumnType("datetime")
                    .HasColumnName("Thoi_Gian");

                entity.HasOne(d => d.NguoiDungNavigation)
                    .WithMany(p => p.XemTrangNguoiDungNavigations)
                    .HasForeignKey(d => d.NguoiDung)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__XemTrang__Nguoi___38996AB5");

                entity.HasOne(d => d.NguoiXemNavigation)
                    .WithMany(p => p.XemTrangNguoiXemNavigations)
                    .HasForeignKey(d => d.NguoiXem)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__XemTrang__Nguoi___398D8EEE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
