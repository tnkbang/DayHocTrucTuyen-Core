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
        public virtual DbSet<LichSuTruyCap> LichSuTruyCaps { get; set; } = null!;
        public virtual DbSet<LoaiNd> LoaiNds { get; set; } = null!;
        public virtual DbSet<LopHoc> LopHocs { get; set; } = null!;
        public virtual DbSet<LopThuocTag> LopThuocTags { get; set; } = null!;
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; } = null!;
        public virtual DbSet<PheDuyet> PheDuyets { get; set; } = null!;
        public virtual DbSet<PhongThi> PhongThis { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;
        public virtual DbSet<ThichTrang> ThichTrangs { get; set; } = null!;
        public virtual DbSet<ThoiGianLamBai> ThoiGianLamBais { get; set; } = null!;
        public virtual DbSet<ThongBao> ThongBaos { get; set; } = null!;
        public virtual DbSet<TinNhan> TinNhans { get; set; } = null!;
        public virtual DbSet<TrangThaiNangCap> TrangThaiNangCaps { get; set; } = null!;
        public virtual DbSet<ViNguoiDung> ViNguoiDungs { get; set; } = null!;
        public virtual DbSet<XemTrang> XemTrangs { get; set; } = null!;
        public virtual DbSet<YeuCauThanhToan> YeuCauThanhToans { get; set; } = null!;

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
                    .HasName("PK__BaiDang__3A5539EFA9FD8ECC");

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
                    .HasConstraintName("FK__BaiDang__Ma_Lop__628FA481");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.BaiDangs)
                    .HasForeignKey(d => d.MaNd)
                    .HasConstraintName("FK__BaiDang__Ma_ND__6383C8BA");
            });

            modelBuilder.Entity<BaoCao>(entity =>
            {
                entity.HasKey(e => e.MaBaoCao)
                    .HasName("PK__BaoCao__5FC87B6E0AB07C38");

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
                    .HasConstraintName("FK__BaoCao__Ma_ND__47DBAE45");
            });

            modelBuilder.Entity<BiCamThi>(entity =>
            {
                entity.HasKey(e => new { e.MaNd, e.MaPhong })
                    .HasName("PK__BiCamThi__015410DD21F62A1F");

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
                    .HasConstraintName("FK__BiCamThi__Ma_ND__73BA3083");

                entity.HasOne(d => d.MaPhongNavigation)
                    .WithMany(p => p.BiCamThis)
                    .HasForeignKey(d => d.MaPhong)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BiCamThi__Ma_Pho__74AE54BC");
            });

            modelBuilder.Entity<BinhLuan>(entity =>
            {
                entity.HasKey(e => new { e.MaBai, e.MaNd, e.ThoiGian })
                    .HasName("PK__BinhLuan__4176DE9182256901");

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
                    .HasConstraintName("FK__BinhLuan__Ma_Bai__693CA210");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.BinhLuans)
                    .HasForeignKey(d => d.MaNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BinhLuan__Ma_ND__6A30C649");
            });

            modelBuilder.Entity<CamXuc>(entity =>
            {
                entity.HasKey(e => new { e.MaNd, e.MaBai })
                    .HasName("PK__CamXuc__CDC7DE286C68564C");

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
                    .HasConstraintName("FK__CamXuc__Ma_Bai__6E01572D");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.CamXucs)
                    .HasForeignKey(d => d.MaNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CamXuc__Ma_ND__6D0D32F4");
            });

            modelBuilder.Entity<CauHoiThi>(entity =>
            {
                entity.HasKey(e => new { e.Stt, e.MaPhong })
                    .HasName("PK__CauHoiTh__E5282BFBECEAB9DA");

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
                    .HasConstraintName("FK__CauHoiThi__Ma_Ph__778AC167");
            });

            modelBuilder.Entity<CauTraLoi>(entity =>
            {
                entity.HasKey(e => new { e.Stt, e.MaPhong, e.MaNd, e.LanThu })
                    .HasName("PK__CauTraLo__1FE834025D55077E");

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
                    .HasConstraintName("FK__CauTraLoi__Ma_ND__7A672E12");

                entity.HasOne(d => d.CauHoiThi)
                    .WithMany(p => p.CauTraLois)
                    .HasForeignKey(d => new { d.Stt, d.MaPhong })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CauTraLoi__7B5B524B");
            });

            modelBuilder.Entity<DanhGiaLop>(entity =>
            {
                entity.HasKey(e => e.MaDg)
                    .HasName("PK__DanhGiaL__2E67451C991DAB0A");

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
                    .HasConstraintName("FK__DanhGiaLo__Ma_Lo__5812160E");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.DanhGiaLops)
                    .HasForeignKey(d => d.MaNd)
                    .HasConstraintName("FK__DanhGiaLo__Ma_ND__571DF1D5");
            });

            modelBuilder.Entity<Ghim>(entity =>
            {
                entity.HasKey(e => e.MaBai)
                    .HasName("PK__Ghim__3A5539EFF4B2AE1E");

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
                    .HasConstraintName("FK__Ghim__Ma_Bai__66603565");
            });

            modelBuilder.Entity<GoiNangCap>(entity =>
            {
                entity.HasKey(e => e.MaGoi)
                    .HasName("PK__GoiNangC__3D0F91480CD9BB0A");

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
                    .HasName("PK__HocSinhT__E2596BF52ACB9466");

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
                    .HasConstraintName("FK__HocSinhTh__Ma_Lo__5FB337D6");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.HocSinhThuocLops)
                    .HasForeignKey(d => d.MaNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HocSinhTh__Ma_ND__5EBF139D");
            });

            modelBuilder.Entity<LichSuGiaoDich>(entity =>
            {
                entity.HasKey(e => new { e.MaNd, e.ThoiGian })
                    .HasName("PK__LichSuGi__B23E77E64E7F767A");

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
                    .HasConstraintName("FK__LichSuGia__Ma_ND__36B12243");
            });

            modelBuilder.Entity<LichSuTruyCap>(entity =>
            {
                entity.HasKey(e => e.ThoiGian)
                    .HasName("PK__LichSuTr__C5CFA508D4F3D9DD");

                entity.ToTable("LichSuTruyCap");

                entity.Property(e => e.ThoiGian)
                    .HasColumnType("datetime")
                    .HasColumnName("Thoi_Gian");

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

                entity.HasOne(d => d.MaLopNavigation)
                    .WithMany(p => p.LichSuTruyCaps)
                    .HasForeignKey(d => d.MaLop)
                    .HasConstraintName("FK__LichSuTru__Ma_Lo__5BE2A6F2");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.LichSuTruyCaps)
                    .HasForeignKey(d => d.MaNd)
                    .HasConstraintName("FK__LichSuTru__Ma_ND__5AEE82B9");
            });

            modelBuilder.Entity<LoaiNd>(entity =>
            {
                entity.HasKey(e => e.MaLoai)
                    .HasName("PK__LoaiND__586312F96D194C92");

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
                    .HasName("PK__LopHoc__C3BE643D8DE3C9DF");

                entity.ToTable("LopHoc");

                entity.HasIndex(e => e.BiDanh, "UQ__LopHoc__7F28B28B55738035")
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
                    .HasConstraintName("FK__LopHoc__Ma_ND__4E88ABD4");
            });

            modelBuilder.Entity<LopThuocTag>(entity =>
            {
                entity.ToTable("LopThuocTag");

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
                    .HasConstraintName("FK__LopThuocT__Ma_Lo__5441852A");

                entity.HasOne(d => d.MaTagNavigation)
                    .WithMany(p => p.LopThuocTags)
                    .HasForeignKey(d => d.MaTag)
                    .HasConstraintName("FK__LopThuocT__Ma_Ta__534D60F1");
            });

            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.HasKey(e => e.MaNd)
                    .HasName("PK__NguoiDun__2E628DB69C88F0CF");

                entity.ToTable("NguoiDung");

                entity.HasIndex(e => e.BiDanh, "UQ__NguoiDun__7F28B28B09F63ED7")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__NguoiDun__A9D10534A59F64A3")
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

            modelBuilder.Entity<PheDuyet>(entity =>
            {
                entity.HasKey(e => e.MaNd)
                    .HasName("PK__PheDuyet__2E628DB60EBDD481");

                entity.ToTable("PheDuyet");

                entity.Property(e => e.MaNd)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Ma_ND")
                    .IsFixedLength();

                entity.Property(e => e.GhiChu)
                    .HasMaxLength(200)
                    .HasColumnName("Ghi_Chu");

                entity.Property(e => e.NgayDangKy)
                    .HasColumnType("datetime")
                    .HasColumnName("Ngay_Dang_Ky");

                entity.Property(e => e.TrangThai).HasColumnName("Trang_Thai");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithOne(p => p.PheDuyet)
                    .HasForeignKey<PheDuyet>(d => d.MaNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PheDuyet__Ma_ND__2B3F6F97");
            });

            modelBuilder.Entity<PhongThi>(entity =>
            {
                entity.HasKey(e => e.MaPhong)
                    .HasName("PK__PhongThi__F369D6B36C267D0E");

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
                    .HasConstraintName("FK__PhongThi__Ma_Lop__70DDC3D8");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => e.MaTag)
                    .HasName("PK__Tag__C1AE337A6A9363A0");

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
                    .HasName("PK__ThichTra__2E62A20F5F19C71B");

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
                    .HasConstraintName("FK__ThichTran__Nguoi__403A8C7D");

                entity.HasOne(d => d.NguoiThichNavigation)
                    .WithMany(p => p.ThichTrangNguoiThichNavigations)
                    .HasForeignKey(d => d.NguoiThich)
                    .HasConstraintName("FK__ThichTran__Nguoi__412EB0B6");
            });

            modelBuilder.Entity<ThoiGianLamBai>(entity =>
            {
                entity.HasKey(e => new { e.MaNd, e.MaPhong, e.LanThu })
                    .HasName("PK__ThoiGian__DFB3C799E05002E9");

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
                    .HasConstraintName("FK__ThoiGianL__Ma_ND__7E37BEF6");

                entity.HasOne(d => d.MaPhongNavigation)
                    .WithMany(p => p.ThoiGianLamBais)
                    .HasForeignKey(d => d.MaPhong)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ThoiGianL__Ma_Ph__7F2BE32F");
            });

            modelBuilder.Entity<ThongBao>(entity =>
            {
                entity.HasKey(e => new { e.MaTb, e.MaNd })
                    .HasName("PK__ThongBao__5C84D3AE762A7E80");

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

                entity.Property(e => e.LoaiTb)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Loai_TB");

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
                    .HasConstraintName("FK__ThongBao__Ma_ND__4AB81AF0");
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
                    .HasConstraintName("FK__TinNhan__Nguoi_G__3C69FB99");

                entity.HasOne(d => d.NguoiNhanNavigation)
                    .WithMany(p => p.TinNhanNguoiNhanNavigations)
                    .HasForeignKey(d => d.NguoiNhan)
                    .HasConstraintName("FK__TinNhan__Nguoi_N__3D5E1FD2");
            });

            modelBuilder.Entity<TrangThaiNangCap>(entity =>
            {
                entity.HasKey(e => e.MaNd)
                    .HasName("PK__TrangTha__2E628DB69D3A95F8");

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
                    .HasConstraintName("FK__TrangThai__Ma_Go__33D4B598");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithOne(p => p.TrangThaiNangCap)
                    .HasForeignKey<TrangThaiNangCap>(d => d.MaNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TrangThai__Ma_ND__32E0915F");
            });

            modelBuilder.Entity<ViNguoiDung>(entity =>
            {
                entity.HasKey(e => e.MaNd)
                    .HasName("PK__ViNguoiD__2E628DB6207A831B");

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
                    .HasConstraintName("FK__ViNguoiDu__Ma_ND__2E1BDC42");
            });

            modelBuilder.Entity<XemTrang>(entity =>
            {
                entity.HasKey(e => e.MaXt)
                    .HasName("PK__XemTrang__2E62DAEE2570E359");

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
                    .HasConstraintName("FK__XemTrang__Nguoi___440B1D61");

                entity.HasOne(d => d.NguoiXemNavigation)
                    .WithMany(p => p.XemTrangNguoiXemNavigations)
                    .HasForeignKey(d => d.NguoiXem)
                    .HasConstraintName("FK__XemTrang__Nguoi___44FF419A");
            });

            modelBuilder.Entity<YeuCauThanhToan>(entity =>
            {
                entity.HasKey(e => new { e.MaNd, e.ThoiGian })
                    .HasName("PK__YeuCauTh__B23E77E699C10F2F");

                entity.ToTable("YeuCauThanhToan");

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

                entity.Property(e => e.LoaiThanhToan)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Loai_Thanh_Toan");

                entity.Property(e => e.SoTaiKhoan)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("So_Tai_Khoan");

                entity.Property(e => e.SoTien).HasColumnName("So_Tien");

                entity.Property(e => e.TrangThai).HasColumnName("Trang_Thai");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.YeuCauThanhToans)
                    .HasForeignKey(d => d.MaNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__YeuCauTha__Ma_ND__398D8EEE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
