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
        public virtual DbSet<CauHoiBaoMat> CauHoiBaoMats { get; set; } = null!;
        public virtual DbSet<CauHoiThi> CauHoiThis { get; set; } = null!;
        public virtual DbSet<CauTraLoi> CauTraLois { get; set; } = null!;
        public virtual DbSet<DanhGiaLop> DanhGiaLops { get; set; } = null!;
        public virtual DbSet<Ghim> Ghims { get; set; } = null!;
        public virtual DbSet<HocSinhThuocLop> HocSinhThuocLops { get; set; } = null!;
        public virtual DbSet<LoaiNd> LoaiNds { get; set; } = null!;
        public virtual DbSet<LopHoc> LopHocs { get; set; } = null!;
        public virtual DbSet<LopThuocTag> LopThuocTags { get; set; } = null!;
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; } = null!;
        public virtual DbSet<PhieuDanhGium> PhieuDanhGia { get; set; } = null!;
        public virtual DbSet<PhongThi> PhongThis { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;
        public virtual DbSet<ThichTrang> ThichTrangs { get; set; } = null!;
        public virtual DbSet<ThoiGianLamBai> ThoiGianLamBais { get; set; } = null!;
        public virtual DbSet<ThongBao> ThongBaos { get; set; } = null!;
        public virtual DbSet<TinNhan> TinNhans { get; set; } = null!;
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
                    .HasName("PK__BaiDang__3A5539EFE80750E9");

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
                    .HasConstraintName("FK__BaiDang__Ma_Lop__5070F446");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.BaiDangs)
                    .HasForeignKey(d => d.MaNd)
                    .HasConstraintName("FK__BaiDang__Ma_ND__5165187F");
            });

            modelBuilder.Entity<BaoCao>(entity =>
            {
                entity.HasKey(e => e.MaBaoCao)
                    .HasName("PK__BaoCao__5FC87B6E595511FE");

                entity.ToTable("BaoCao");

                entity.Property(e => e.MaBaoCao)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Ma_Bao_Cao")
                    .IsFixedLength();

                entity.Property(e => e.ChiMuc)
                    .HasMaxLength(23)
                    .IsUnicode(false)
                    .HasColumnName("Chi_Muc")
                    .IsFixedLength();

                entity.Property(e => e.LoaiBaoCao)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("Loai_Bao_Cao")
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

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.BaoCaos)
                    .HasForeignKey(d => d.MaNd)
                    .HasConstraintName("FK__BaoCao__Ma_ND__36B12243");
            });

            modelBuilder.Entity<BiCamThi>(entity =>
            {
                entity.HasKey(e => new { e.MaNd, e.MaPhong })
                    .HasName("PK__BiCamThi__015410DD4B36B805");

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
                    .HasConstraintName("FK__BiCamThi__Ma_ND__619B8048");

                entity.HasOne(d => d.MaPhongNavigation)
                    .WithMany(p => p.BiCamThis)
                    .HasForeignKey(d => d.MaPhong)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BiCamThi__Ma_Pho__628FA481");
            });

            modelBuilder.Entity<BinhLuan>(entity =>
            {
                entity.HasKey(e => new { e.MaBai, e.MaNd, e.ThoiGian })
                    .HasName("PK__BinhLuan__4176DE910951BA8C");

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
                    .HasConstraintName("FK__BinhLuan__Ma_Bai__571DF1D5");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.BinhLuans)
                    .HasForeignKey(d => d.MaNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BinhLuan__Ma_ND__5812160E");
            });

            modelBuilder.Entity<CamXuc>(entity =>
            {
                entity.HasKey(e => new { e.MaNd, e.MaBai })
                    .HasName("PK__CamXuc__CDC7DE28C8F5377F");

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
                    .HasConstraintName("FK__CamXuc__Ma_Bai__5BE2A6F2");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.CamXucs)
                    .HasForeignKey(d => d.MaNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CamXuc__Ma_ND__5AEE82B9");
            });

            modelBuilder.Entity<CauHoiBaoMat>(entity =>
            {
                entity.HasKey(e => new { e.Stt, e.MaPhong })
                    .HasName("PK__CauHoiBa__E5282BFBDD1C5F22");

                entity.ToTable("CauHoiBaoMat");

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
                    .WithMany(p => p.CauHoiBaoMats)
                    .HasForeignKey(d => d.MaPhong)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CauHoiBao__Ma_Ph__6FE99F9F");
            });

            modelBuilder.Entity<CauHoiThi>(entity =>
            {
                entity.HasKey(e => new { e.Stt, e.MaPhong })
                    .HasName("PK__CauHoiTh__E5282BFB9CC6A069");

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
                    .HasConstraintName("FK__CauHoiThi__Ma_Ph__656C112C");
            });

            modelBuilder.Entity<CauTraLoi>(entity =>
            {
                entity.HasKey(e => new { e.Stt, e.MaPhong, e.MaNd, e.LanThu })
                    .HasName("PK__CauTraLo__1FE83402C71341A2");

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
                    .HasConstraintName("FK__CauTraLoi__Ma_ND__68487DD7");

                entity.HasOne(d => d.CauHoiThi)
                    .WithMany(p => p.CauTraLois)
                    .HasForeignKey(d => new { d.Stt, d.MaPhong })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CauTraLoi__693CA210");
            });

            modelBuilder.Entity<DanhGiaLop>(entity =>
            {
                entity.HasKey(e => e.MaDg)
                    .HasName("PK__DanhGiaL__2E67451C1F57BD29");

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
                    .HasConstraintName("FK__DanhGiaLo__Ma_Lo__49C3F6B7");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.DanhGiaLops)
                    .HasForeignKey(d => d.MaNd)
                    .HasConstraintName("FK__DanhGiaLo__Ma_ND__48CFD27E");
            });

            modelBuilder.Entity<Ghim>(entity =>
            {
                entity.HasKey(e => e.MaBai)
                    .HasName("PK__Ghim__3A5539EF4F5B6531");

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
                    .HasConstraintName("FK__Ghim__Ma_Bai__5441852A");
            });

            modelBuilder.Entity<HocSinhThuocLop>(entity =>
            {
                entity.HasKey(e => new { e.MaNd, e.MaLop })
                    .HasName("PK__HocSinhT__E2596BF5DDBC5059");

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
                    .HasConstraintName("FK__HocSinhTh__Ma_Lo__4D94879B");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.HocSinhThuocLops)
                    .HasForeignKey(d => d.MaNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HocSinhTh__Ma_ND__4CA06362");
            });

            modelBuilder.Entity<LoaiNd>(entity =>
            {
                entity.HasKey(e => e.MaLoai)
                    .HasName("PK__LoaiND__586312F90E51D9B7");

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
                    .HasName("PK__LopHoc__C3BE643DC82208F6");

                entity.ToTable("LopHoc");

                entity.HasIndex(e => e.BiDanh, "UQ__LopHoc__7F28B28BAAB77BA4")
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

                entity.Property(e => e.ImgBia)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Img_Bia");

                entity.Property(e => e.MaNd)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Ma_ND")
                    .IsFixedLength();

                entity.Property(e => e.MatKhau)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Mat_Khau");

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
                    .HasConstraintName("FK__LopHoc__Ma_ND__403A8C7D");
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
                    .HasConstraintName("FK__LopThuocT__Ma_Lo__45F365D3");

                entity.HasOne(d => d.MaTagNavigation)
                    .WithMany(p => p.LopThuocTags)
                    .HasForeignKey(d => d.MaTag)
                    .HasConstraintName("FK__LopThuocT__Ma_Ta__44FF419A");
            });

            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.HasKey(e => e.MaNd)
                    .HasName("PK__NguoiDun__2E628DB63670D38F");

                entity.ToTable("NguoiDung");

                entity.HasIndex(e => e.BiDanh, "UQ__NguoiDun__7F28B28B85A76850")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__NguoiDun__A9D105343E52E0A3")
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
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Img_Avt");

                entity.Property(e => e.ImgBg)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Img_BG");

                entity.Property(e => e.ImgNhanDien)
                    .HasMaxLength(20)
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
                    .HasConstraintName("FK__NguoiDung__Ma_Lo__286302EC");
            });

            modelBuilder.Entity<PhieuDanhGium>(entity =>
            {
                entity.HasKey(e => e.MaPhieu)
                    .HasName("PK__PhieuDan__1568CAA459DDA0B0");

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
                    .WithMany(p => p.PhieuDanhGia)
                    .HasForeignKey(d => d.MaNd)
                    .HasConstraintName("FK__PhieuDanh__Ma_ND__3C69FB99");
            });

            modelBuilder.Entity<PhongThi>(entity =>
            {
                entity.HasKey(e => e.MaPhong)
                    .HasName("PK__PhongThi__F369D6B38F07FA98");

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

                entity.Property(e => e.XacThuc).HasColumnName("Xac_Thuc");

                entity.Property(e => e.XemLai).HasColumnName("Xem_Lai");

                entity.HasOne(d => d.MaLopNavigation)
                    .WithMany(p => p.PhongThis)
                    .HasForeignKey(d => d.MaLop)
                    .HasConstraintName("FK__PhongThi__Ma_Lop__5EBF139D");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => e.MaTag)
                    .HasName("PK__Tag__C1AE337A30E18AD8");

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
                    .HasName("PK__ThichTra__2E62A20F9C45B215");

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
                    .HasConstraintName("FK__ThichTran__Nguoi__2F10007B");

                entity.HasOne(d => d.NguoiThichNavigation)
                    .WithMany(p => p.ThichTrangNguoiThichNavigations)
                    .HasForeignKey(d => d.NguoiThich)
                    .HasConstraintName("FK__ThichTran__Nguoi__300424B4");
            });

            modelBuilder.Entity<ThoiGianLamBai>(entity =>
            {
                entity.HasKey(e => new { e.MaNd, e.MaPhong, e.LanThu })
                    .HasName("PK__ThoiGian__DFB3C7993099B58C");

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
                    .HasConstraintName("FK__ThoiGianL__Ma_ND__6C190EBB");

                entity.HasOne(d => d.MaPhongNavigation)
                    .WithMany(p => p.ThoiGianLamBais)
                    .HasForeignKey(d => d.MaPhong)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ThoiGianL__Ma_Ph__6D0D32F4");
            });

            modelBuilder.Entity<ThongBao>(entity =>
            {
                entity.HasKey(e => new { e.MaTb, e.MaNd })
                    .HasName("PK__ThongBao__5C84D3AED7DB261D");

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
                    .HasConstraintName("FK__ThongBao__Ma_ND__398D8EEE");
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
                    .HasConstraintName("FK__TinNhan__Nguoi_G__2B3F6F97");

                entity.HasOne(d => d.NguoiNhanNavigation)
                    .WithMany(p => p.TinNhanNguoiNhanNavigations)
                    .HasForeignKey(d => d.NguoiNhan)
                    .HasConstraintName("FK__TinNhan__Nguoi_N__2C3393D0");
            });

            modelBuilder.Entity<XemTrang>(entity =>
            {
                entity.HasKey(e => e.MaXt)
                    .HasName("PK__XemTrang__2E62DAEE0241E2E6");

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
                    .HasConstraintName("FK__XemTrang__Nguoi___32E0915F");

                entity.HasOne(d => d.NguoiXemNavigation)
                    .WithMany(p => p.XemTrangNguoiXemNavigations)
                    .HasForeignKey(d => d.NguoiXem)
                    .HasConstraintName("FK__XemTrang__Nguoi___33D4B598");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
