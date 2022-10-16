GO
Create database DayHocTrucTuyen
GO
use DayHocTrucTuyen
GO

create table LoaiND(
	Ma_Loai char(2) primary key,
	Ten_Loai nvarchar(10) not null
)

create table NguoiDung(
	Ma_ND char(7) primary key,
	Ma_Loai char(2) references LoaiND(Ma_Loai),
	Ho_Lot nvarchar(20),
	Ten nvarchar(7) not null,
	Ngay_Sinh datetime,
	Gioi_Tinh int,
	SDT varchar(11),
	Email varchar(50) not null unique,
	Mat_Khau varchar(32) not null,
	Img_Avt varchar(100),
	Img_BG varchar(100),
	Img_Nhan_Dien varchar(100),
	Trang_Thai bit not null,
	Mo_Ta nvarchar(300),
	Ngay_Tao datetime not null,
	Bi_Danh varchar(20) unique
)

create table PheDuyet(
	Ma_ND char(7) primary key references NguoiDung(Ma_ND),
	Ngay_Dang_Ky datetime not null,
	Trang_Thai bit not null,
	Ghi_Chu nvarchar(200)
)

create table ViNguoiDung(
	Ma_ND char(7) primary key references NguoiDung(Ma_ND),
	So_Du float not null,
	Ngay_Mo datetime not null,
	Trang_Thai bit not null
)

create table GoiNangCap(
	Ma_Goi int primary key,
	Ten_Goi nvarchar(10) not null,
	Gia_Tien float not null,
	Hieu_Luc int not null,
	Mo_Ta nvarchar(100)
)

create table TrangThaiNangCap(
	Ma_ND char(7) primary key references NguoiDung(Ma_ND),
	Ma_Goi int references GoiNangCap(Ma_Goi),
	Ngay_Dang_Ky datetime not null
)

create table LichSuGiaoDich(
	Ma_ND char(7) references NguoiDung(Ma_ND),
	Thoi_Gian datetime,
	Thu_Vao bit not null,
	So_Tien float not null,
	So_Du float not null,
	Ghi_Chu nvarchar(200) not null,
	primary key (Ma_ND, Thoi_Gian)
)

create table TinNhan(
	ID int primary key,
	Nguoi_Gui char(7) references NguoiDung(Ma_ND),
	Nguoi_Nhan char(7) references NguoiDung(Ma_ND),
	Thoi_Gian datetime not null,
	Noi_Dung nvarchar(500) not null,
	Trang_Thai bit not null
)

create table ThichTrang(
	Ma_YT char(15) primary key,
	Nguoi_Dung char(7) references NguoiDung(Ma_ND),
	Nguoi_Thich char(7) references NguoiDung(Ma_ND),
	Thoi_Gian datetime not null
)

create table XemTrang(
	Ma_XT char(15) primary key,
	Nguoi_Dung char(7) references NguoiDung(Ma_ND),
	Nguoi_Xem char(7) references NguoiDung(Ma_ND),
	Thoi_Gian datetime not null
)

create table BaoCao(
	Ma_Bao_Cao char(10) primary key,
	Ma_ND char(7) references NguoiDung(Ma_ND),
	Chi_Muc char(15) not null,
	Noi_Dung nvarchar(150) not null,
	Ghi_Chu nvarchar(500),
	Thoi_Gian datetime not null
)

create table ThongBao(
	Ma_TB char(5),
	Ma_ND char(7) references NguoiDung(Ma_ND),
	Tieu_De nvarchar(20) not null,
	Noi_Dung nvarchar(500),
	Thoi_Gian datetime not null,
	Trang_Thai bit not null,
	Lien_Ket varchar(100),
	primary key (Ma_TB, Ma_ND)
)

create table PhieuBinhChon(
	Ma_Phieu char(5) primary key,
	Ma_ND char(7) references NguoiDung(Ma_ND),
	Chuc_Nang nvarchar(30) not null,
	Muc_Do int not null,
	Ghi_Chu nvarchar(500)
)

create table LopHoc(
	Ma_Lop char(11) primary key,
	Ma_ND char(7) references NguoiDung(Ma_ND),
	Ngay_Tao datetime not null,
	Ten_Lop nvarchar(40) not null,
	Bi_Danh varchar(20) unique,
	Gia_Tien float not null,
	Mo_Ta nvarchar(300),
	Trang_Thai bit not null,
	Img_Bia varchar(20)
)

create table Tag(
	Ma_Tag char(5) primary key,
	Ten_Tag nvarchar(30) not null
)

create table LopThuocTag(
	Id int primary key identity(1,1),
	Ma_Tag char(5) references Tag(Ma_Tag),
	Ma_Lop char(11) references LopHoc(Ma_Lop)
)

create table DanhGiaLop(
	Ma_DG char(15) primary key,
	Ma_ND char(7) references NguoiDung(Ma_ND),
	Ma_Lop char(11) references LopHoc(Ma_Lop),
	Muc_Do int not null,
	Ghi_Chu nvarchar(500),
	Thoi_Gian datetime
)

create table HocSinhThuocLop(
	Ma_ND char(7) references NguoiDung(Ma_ND),
	Ma_Lop char(11) references LopHoc(Ma_Lop),
	Ngay_Tham_Gia Datetime,
	primary key (Ma_ND, Ma_Lop)
)

create table BaiDang(
	Ma_Bai char(15) primary key,
	Ma_Lop char(11) references LopHoc(Ma_Lop),
	Ma_ND char(7) references NguoiDung(Ma_ND),
	Thoi_Gian datetime not null,
	Noi_Dung nvarchar(500),
	Dinh_Kem varchar(1000),
	Trang_Thai bit not null
)

create table Ghim(
	Ma_Bai char(15) primary key references BaiDang(Ma_Bai),
	Thoi_Gian datetime not null
)

create table BinhLuan(
	Ma_Bai char(15) references BaiDang(Ma_Bai),
	Ma_ND char(7) references NguoiDung(Ma_ND),
	Thoi_Gian datetime not null,
	Noi_Dung nvarchar(500),
	Dinh_Kem varchar(1000),
	primary key (Ma_Bai, Ma_ND, Thoi_Gian)
)

create table CamXuc(
	Ma_ND char(7) references NguoiDung(Ma_ND),
	Ma_Bai char(15) references BaiDang(Ma_Bai),
	Thoi_Gian datetime not null,
	primary key (Ma_ND, Ma_Bai)
)

create table PhongThi(
	Ma_Phong char(15) primary key,
	Ma_Lop char(11) references LopHoc(Ma_Lop),
	Ten_Phong nvarchar(50) not null,
	Ngay_Tao datetime not null,
	Mat_Khau varchar(50),
	Ngay_Mo datetime not null,
	Ngay_Dong datetime not null,
	Luot_Thi int not null,
	Xem_Lai bit not null,
	Thoi_Luong int not null
)

create table BiCamThi(
	Ma_ND char(7) references NguoiDung(Ma_ND),
	Ma_Phong char(15) references PhongThi(Ma_Phong),
	Ly_Do nvarchar(100),
	primary key (Ma_ND, Ma_Phong)
)

create table CauHoiThi(
	STT int,
	Ma_Phong char(15) references PhongThi(Ma_Phong),
	Cau_Hoi nvarchar(500) not null,
	Loi_Giai nvarchar(100) not null,
	Dap_An nvarchar(500) not null,
	primary key (STT, Ma_Phong)
)

create table CauTraLoi(
	STT int,
	Ma_Phong char(15),
	Ma_ND char(7) references NguoiDung(Ma_ND),
	Lan_Thu int,
	Dap_An nvarchar(500) not null,
	primary key (STT, Ma_Phong, Ma_ND, Lan_Thu)
)
ALTER TABLE CauTraLoi ADD FOREIGN KEY (STT, Ma_Phong) REFERENCES CauHoiThi(STT, Ma_Phong)

create table ThoiGianLamBai(
	Ma_ND char(7) references NguoiDung(Ma_ND),
	Ma_Phong char(15) references PhongThi(Ma_Phong),
	Lan_Thu int not null,
	Bat_Dau datetime not null,
	Ket_Thuc datetime,
	primary key (Ma_ND, Ma_Phong, Lan_Thu)
)

GO
INSERT [dbo].[LoaiND] ([Ma_Loai], [Ten_Loai]) VALUES (N'01', N'Admin')
INSERT [dbo].[LoaiND] ([Ma_Loai], [Ten_Loai]) VALUES (N'02', N'GiaoVien')
INSERT [dbo].[LoaiND] ([Ma_Loai], [Ten_Loai]) VALUES (N'03', N'HocSinh')

INSERT [dbo].[GoiNangCap] ([Ma_Goi], [Ten_Goi], [Gia_Tien], [Hieu_Luc], [Mo_Ta]) VALUES (1, N'Vip', 10000, 1, N'Gói nâng cấp thân thiết')
INSERT [dbo].[GoiNangCap] ([Ma_Goi], [Ten_Goi], [Gia_Tien], [Hieu_Luc], [Mo_Ta]) VALUES (2, N'SVip', 30000, 6, N'Gói nâng cấp tin cậy')
INSERT [dbo].[GoiNangCap] ([Ma_Goi], [Ten_Goi], [Gia_Tien], [Hieu_Luc], [Mo_Ta]) VALUES (3, N'Premium', 50000, 12, N'Gói nâng cấp tri kỷ')

INSERT [dbo].[NguoiDung] ([Ma_ND], [Ma_Loai], [Ho_Lot], [Ten], [Ngay_Sinh], [Gioi_Tinh], [SDT], [Email], [Mat_Khau], [Img_Avt], [Img_BG], [Img_Nhan_Dien], [Trang_Thai], [Mo_Ta], [Ngay_Tao], [Bi_Danh]) VALUES (N'U000001', N'01', N'Khánh', N'Băng', NULL, NULL, NULL, N'trieukhanhbang123@gmail.com', N'B486D7696F563BA2B80EEB936BC63166', N'avt-U000001-310.jpg', NULL, NULL, 1, NULL, CAST(0x0000AE650127192E AS DateTime), N'U000001')
INSERT [dbo].[NguoiDung] ([Ma_ND], [Ma_Loai], [Ho_Lot], [Ten], [Ngay_Sinh], [Gioi_Tinh], [SDT], [Email], [Mat_Khau], [Img_Avt], [Img_BG], [Img_Nhan_Dien], [Trang_Thai], [Mo_Ta], [Ngay_Tao], [Bi_Danh]) VALUES (N'U000002', N'03', N'Lâm Linh', N'Tuyết', NULL, NULL, NULL, N'linhtuyet@gmail.com', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, NULL, NULL, 1, NULL, CAST(0x0000AE6E0133077B AS DateTime), N'U000002')
INSERT [dbo].[NguoiDung] ([Ma_ND], [Ma_Loai], [Ho_Lot], [Ten], [Ngay_Sinh], [Gioi_Tinh], [SDT], [Email], [Mat_Khau], [Img_Avt], [Img_BG], [Img_Nhan_Dien], [Trang_Thai], [Mo_Ta], [Ngay_Tao], [Bi_Danh]) VALUES (N'U000003', N'03', N'Ngô Minh', N'Nguyệt', NULL, NULL, NULL, N'minhnguyet@gmail.com', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, NULL, NULL, 1, NULL, CAST(0x0000AE7D009A1B94 AS DateTime), N'U000003')
INSERT [dbo].[NguoiDung] ([Ma_ND], [Ma_Loai], [Ho_Lot], [Ten], [Ngay_Sinh], [Gioi_Tinh], [SDT], [Email], [Mat_Khau], [Img_Avt], [Img_BG], [Img_Nhan_Dien], [Trang_Thai], [Mo_Ta], [Ngay_Tao], [Bi_Danh]) VALUES (N'U000004', N'03', N'Lâm Thu', N'Hà', NULL, NULL, NULL, N'thuha@gmail.com', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, NULL, NULL, 1, NULL, CAST(0x0000AE7D009A395D AS DateTime), N'U000004')
INSERT [dbo].[NguoiDung] ([Ma_ND], [Ma_Loai], [Ho_Lot], [Ten], [Ngay_Sinh], [Gioi_Tinh], [SDT], [Email], [Mat_Khau], [Img_Avt], [Img_BG], [Img_Nhan_Dien], [Trang_Thai], [Mo_Ta], [Ngay_Tao], [Bi_Danh]) VALUES (N'U000005', N'03', N'Nguyễn Nhật', N'Duy', NULL, NULL, NULL, N'nhatduy@gmail.com', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, NULL, NULL, 1, NULL, CAST(0x0000AE7D009A51E6 AS DateTime), N'U000005')
INSERT [dbo].[NguoiDung] ([Ma_ND], [Ma_Loai], [Ho_Lot], [Ten], [Ngay_Sinh], [Gioi_Tinh], [SDT], [Email], [Mat_Khau], [Img_Avt], [Img_BG], [Img_Nhan_Dien], [Trang_Thai], [Mo_Ta], [Ngay_Tao], [Bi_Danh]) VALUES (N'U000006', N'02', N'Hồ Ánh', N'Nguyệt', NULL, NULL, NULL, N'anhnguyet@gmail.com', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, NULL, NULL, 1, NULL, CAST(0x0000AE7D009A6C9B AS DateTime), N'U000006')

INSERT [dbo].[Tag] ([Ma_Tag], [Ten_Tag]) VALUES (N'CT001', N'Lập Trình Web')
INSERT [dbo].[Tag] ([Ma_Tag], [Ten_Tag]) VALUES (N'CT002', N'Lập Trình .Net')
INSERT [dbo].[Tag] ([Ma_Tag], [Ten_Tag]) VALUES (N'CT003', N'Lập Trình Java')
INSERT [dbo].[Tag] ([Ma_Tag], [Ten_Tag]) VALUES (N'CT004', N'Tìm Hiểu C và C++')
INSERT [dbo].[Tag] ([Ma_Tag], [Ten_Tag]) VALUES (N'CT005', N'Trí Tuệ Nhân Tạo')
INSERT [dbo].[Tag] ([Ma_Tag], [Ten_Tag]) VALUES (N'TH001', N'Toán')
INSERT [dbo].[Tag] ([Ma_Tag], [Ten_Tag]) VALUES (N'TH002', N'Ngữ Văn')
INSERT [dbo].[Tag] ([Ma_Tag], [Ten_Tag]) VALUES (N'TH003', N'Tiếng Anh')
INSERT [dbo].[Tag] ([Ma_Tag], [Ten_Tag]) VALUES (N'TH004', N'Vật Lý')
INSERT [dbo].[Tag] ([Ma_Tag], [Ten_Tag]) VALUES (N'TH005', N'Hóa Học')
INSERT [dbo].[Tag] ([Ma_Tag], [Ten_Tag]) VALUES (N'TH006', N'Sinh Học')

INSERT [dbo].[LopHoc] ([Ma_Lop], [Ma_ND], [Ngay_Tao], [Ten_Lop], [Bi_Danh], [Gia_Tien], [Mo_Ta], [Trang_Thai], [Img_Bia]) VALUES (N'ajg-atv-ims', N'U000001', CAST(0x0000AE7701878A1A AS DateTime), N'Test chương trình', N'ajg-atv-ims', 0, N'Lớp học tạo ra để kiểm tra các tính năng', 1, N'ajg-atv-ims-101.png')
INSERT [dbo].[LopHoc] ([Ma_Lop], [Ma_ND], [Ngay_Tao], [Ten_Lop], [Bi_Danh], [Gia_Tien], [Mo_Ta], [Trang_Thai], [Img_Bia]) VALUES (N'fpv-gnj-laj', N'U000006', CAST(0x0000AE7D009BDB70 AS DateTime), N'Lập Trình Web Nâng Cao', N'laptrinhwebnangcao', 0, N'Dạy kỹ thuật nâng cao cho những bạn yêu thích mảng lập trình web.', 1, N'fpv-gnj-laj-883.png')
INSERT [dbo].[LopHoc] ([Ma_Lop], [Ma_ND], [Ngay_Tao], [Ten_Lop], [Bi_Danh], [Gia_Tien], [Mo_Ta], [Trang_Thai], [Img_Bia]) VALUES (N'mfo-chj-hta', N'U000006', CAST(0x0000AE7D009B1C08 AS DateTime), N'Lập Trình Web', N'mfo-chj-hta', 0, N'Dành cho những ai có yêu thích lập trình web', 1, NULL)
INSERT [dbo].[LopHoc] ([Ma_Lop], [Ma_ND], [Ngay_Tao], [Ten_Lop], [Bi_Danh], [Gia_Tien], [Mo_Ta], [Trang_Thai], [Img_Bia]) VALUES (N'pqr-uvl-hmu', N'U000006', CAST(0x0000AE7D009B5A71 AS DateTime), N'Java Cho Người Mới', N'pqr-uvl-hmu', 0, N'Dạy java căn bản cho người mới và các bạn mất kiến thức căn bản', 1, NULL)
INSERT [dbo].[LopHoc] ([Ma_Lop], [Ma_ND], [Ngay_Tao], [Ten_Lop], [Bi_Danh], [Gia_Tien], [Mo_Ta], [Trang_Thai], [Img_Bia]) VALUES (N'tep-bje-sma', N'U000001', CAST(0x0000AE650127A9CB AS DateTime), N'Lớp học đầu tiên', N'tep-bje-sma', 0, NULL, 1, N'tep-bje-sma-622.png')

INSERT [dbo].[LopThuocTag] ([Ma_Tag], [Ma_Lop]) VALUES (N'CT001', N'ajg-atv-ims')
INSERT [dbo].[LopThuocTag] ([Ma_Tag], [Ma_Lop]) VALUES (N'CT001', N'fpv-gnj-laj')
INSERT [dbo].[LopThuocTag] ([Ma_Tag], [Ma_Lop]) VALUES (N'CT001', N'mfo-chj-hta')
INSERT [dbo].[LopThuocTag] ([Ma_Tag], [Ma_Lop]) VALUES (N'CT001', N'tep-bje-sma')
INSERT [dbo].[LopThuocTag] ([Ma_Tag], [Ma_Lop]) VALUES (N'CT002', N'ajg-atv-ims')
INSERT [dbo].[LopThuocTag] ([Ma_Tag], [Ma_Lop]) VALUES (N'CT002', N'fpv-gnj-laj')
INSERT [dbo].[LopThuocTag] ([Ma_Tag], [Ma_Lop]) VALUES (N'CT002', N'tep-bje-sma')
INSERT [dbo].[LopThuocTag] ([Ma_Tag], [Ma_Lop]) VALUES (N'CT003', N'pqr-uvl-hmu')

INSERT [dbo].[HocSinhThuocLop] ([Ma_ND], [Ma_Lop], [Ngay_Tham_Gia]) VALUES (N'U000001', N'fpv-gnj-laj', CAST(0x0000AE7D00A34D46 AS DateTime))
INSERT [dbo].[HocSinhThuocLop] ([Ma_ND], [Ma_Lop], [Ngay_Tham_Gia]) VALUES (N'U000002', N'fpv-gnj-laj', CAST(0x0000AE7D009EC32D AS DateTime))
INSERT [dbo].[HocSinhThuocLop] ([Ma_ND], [Ma_Lop], [Ngay_Tham_Gia]) VALUES (N'U000002', N'tep-bje-sma', CAST(0x0000AE6E013351D9 AS DateTime))
INSERT [dbo].[HocSinhThuocLop] ([Ma_ND], [Ma_Lop], [Ngay_Tham_Gia]) VALUES (N'U000003', N'fpv-gnj-laj', CAST(0x0000AE7D009E7F20 AS DateTime))
INSERT [dbo].[HocSinhThuocLop] ([Ma_ND], [Ma_Lop], [Ngay_Tham_Gia]) VALUES (N'U000006', N'tep-bje-sma', CAST(0x0000AE7D00A92BE5 AS DateTime))

INSERT [dbo].[BaiDang] ([Ma_Bai], [Ma_Lop], [Ma_ND], [Thoi_Gian], [Noi_Dung], [Dinh_Kem], [Trang_Thai]) VALUES (N'fpv-gnj-laj-001', N'fpv-gnj-laj', N'U000002', CAST(0x0000AE7D009F39B5 AS DateTime), N'Bài đăng nhiều ảnh', N'fpv-gnj-laj-001-1554969988_hinh-nen-4k-tuyet-dep-cho-may-tinh-08.png,fpv-gnj-laj-001-anh-hoa-oai-huong-dep.png,fpv-gnj-laj-001-bien-dao-co-to.png,fpv-gnj-laj-001-doi-mong-mo-1.png,fpv-gnj-laj-001-ec6b2b877f7a20be4ab082f317fd8a3d.png', 1)
INSERT [dbo].[BaiDang] ([Ma_Bai], [Ma_Lop], [Ma_ND], [Thoi_Gian], [Noi_Dung], [Dinh_Kem], [Trang_Thai]) VALUES (N'fpv-gnj-laj-002', N'fpv-gnj-laj', N'U000002', CAST(0x0000AE7D00A107AA AS DateTime), N'Bài đăng 1 ảnh', N'fpv-gnj-laj-002-bien-dao-co-to.png', 1)
INSERT [dbo].[BaiDang] ([Ma_Bai], [Ma_Lop], [Ma_ND], [Thoi_Gian], [Noi_Dung], [Dinh_Kem], [Trang_Thai]) VALUES (N'fpv-gnj-laj-003', N'fpv-gnj-laj', N'U000006', CAST(0x0000AE7D00A17AD1 AS DateTime), N'Bài đăng này được ghim nhé', NULL, 0)
INSERT [dbo].[BaiDang] ([Ma_Bai], [Ma_Lop], [Ma_ND], [Thoi_Gian], [Noi_Dung], [Dinh_Kem], [Trang_Thai]) VALUES (N'fpv-gnj-laj-004', N'fpv-gnj-laj', N'U000006', CAST(0x0000AE7D00A1F905 AS DateTime), N'Bài đăng nhiều file', N'fpv-gnj-laj-004-BAI TAP TONG HOP.pdf,fpv-gnj-laj-004-BAOCAO_QTDA.ppt,fpv-gnj-laj-004-New Text Document.txt,fpv-gnj-laj-004-Sodo Pert_baitap_ON TONG HOP.xlsx,fpv-gnj-laj-004-Thuy?t trình.docx', 1)
INSERT [dbo].[BaiDang] ([Ma_Bai], [Ma_Lop], [Ma_ND], [Thoi_Gian], [Noi_Dung], [Dinh_Kem], [Trang_Thai]) VALUES (N'fpv-gnj-laj-005', N'fpv-gnj-laj', N'U000003', CAST(0x0000AE7D00A31BF4 AS DateTime), N'Chào mọi người nha !!!', NULL, 1)
INSERT [dbo].[BaiDang] ([Ma_Bai], [Ma_Lop], [Ma_ND], [Thoi_Gian], [Noi_Dung], [Dinh_Kem], [Trang_Thai]) VALUES (N'tep-bje-sma-001', N'tep-bje-sma', N'U000002', CAST(0x0000AE77014B90CC AS DateTime), N'Bài đăng mới', NULL, 1)
INSERT [dbo].[BinhLuan] ([Ma_Bai], [Ma_ND], [Thoi_Gian], [Noi_Dung], [Dinh_Kem]) VALUES (N'fpv-gnj-laj-002', N'U000006', CAST(0x0000AE7D00A269EF AS DateTime), N'ảnh đẹp', NULL)

INSERT [dbo].[CamXuc] ([Ma_ND], [Ma_Bai], [Thoi_Gian]) VALUES (N'U000001', N'tep-bje-sma-001', CAST(0x0000AE7801624634 AS DateTime))
INSERT [dbo].[CamXuc] ([Ma_ND], [Ma_Bai], [Thoi_Gian]) VALUES (N'U000002', N'fpv-gnj-laj-001', CAST(0x0000AE7D00A2A1AA AS DateTime))
INSERT [dbo].[CamXuc] ([Ma_ND], [Ma_Bai], [Thoi_Gian]) VALUES (N'U000002', N'fpv-gnj-laj-002', CAST(0x0000AE7D00A29B67 AS DateTime))
INSERT [dbo].[CamXuc] ([Ma_ND], [Ma_Bai], [Thoi_Gian]) VALUES (N'U000002', N'fpv-gnj-laj-003', CAST(0x0000AE7D00A29D04 AS DateTime))
INSERT [dbo].[CamXuc] ([Ma_ND], [Ma_Bai], [Thoi_Gian]) VALUES (N'U000002', N'fpv-gnj-laj-004', CAST(0x0000AE7D00A29F11 AS DateTime))
INSERT [dbo].[CamXuc] ([Ma_ND], [Ma_Bai], [Thoi_Gian]) VALUES (N'U000006', N'fpv-gnj-laj-001', CAST(0x0000AE7D00A21C38 AS DateTime))
INSERT [dbo].[CamXuc] ([Ma_ND], [Ma_Bai], [Thoi_Gian]) VALUES (N'U000006', N'fpv-gnj-laj-002', CAST(0x0000AE7D00A21A4F AS DateTime))
INSERT [dbo].[CamXuc] ([Ma_ND], [Ma_Bai], [Thoi_Gian]) VALUES (N'U000006', N'fpv-gnj-laj-003', CAST(0x0000AE7D00A215E4 AS DateTime))
INSERT [dbo].[CamXuc] ([Ma_ND], [Ma_Bai], [Thoi_Gian]) VALUES (N'U000006', N'fpv-gnj-laj-004', CAST(0x0000AE7D00A217A8 AS DateTime))

INSERT [dbo].[PhongThi] ([Ma_Phong], [Ma_Lop], [Ten_Phong], [Ngay_Tao], [Mat_Khau], [Ngay_Mo], [Ngay_Dong], [Luot_Thi], [Xem_Lai], [Thoi_Luong]) VALUES (N'fpv-gnj-laj-001', N'fpv-gnj-laj', N'Bài thi thử', CAST(0x0000AE7D009CDB5E AS DateTime), N'abc123', CAST(0x0000AE7D009C8E20 AS DateTime), CAST(0x0000AE84009C8E20 AS DateTime), 3, 1, 15)
INSERT [dbo].[PhongThi] ([Ma_Phong], [Ma_Lop], [Ten_Phong], [Ngay_Tao], [Mat_Khau], [Ngay_Mo], [Ngay_Dong], [Luot_Thi], [Xem_Lai], [Thoi_Luong]) VALUES (N'tep-bje-sma-001', N'tep-bje-sma', N'Bài thi thử', CAST(0x0000AE6501337FEB AS DateTime), N'abc123', CAST(0x0000AE65013357B0 AS DateTime), CAST(0x0000AE84013357B0 AS DateTime), 10, 0, 45)
INSERT [dbo].[PhongThi] ([Ma_Phong], [Ma_Lop], [Ten_Phong], [Ngay_Tao], [Mat_Khau], [Ngay_Mo], [Ngay_Dong], [Luot_Thi], [Xem_Lai], [Thoi_Luong]) VALUES (N'tep-bje-sma-002', N'tep-bje-sma', N'Bài thi cuối kỳ', CAST(0x0000AE6701214D46 AS DateTime), N'abc123', CAST(0x0000AE670120F0C0 AS DateTime), CAST(0x0000AEC40120F0C0 AS DateTime), 5, 1, 90)

INSERT [dbo].[CauHoiThi] ([STT], [Ma_Phong], [Cau_Hoi], [Loi_Giai], [Dap_An]) VALUES (1, N'fpv-gnj-laj-001', N'Lớp học tên gì?', N'Lập trình web nâng cao', N'Lập trình web\Lập trình web căn bản\Lập trình web nâng cao\Lập trình web cơ sở')
INSERT [dbo].[CauHoiThi] ([STT], [Ma_Phong], [Cau_Hoi], [Loi_Giai], [Dap_An]) VALUES (1, N'tep-bje-sma-002', N'Lớp học tên gì?', N'Lớp học đầu tiên', N'Không có tên\Lớp A\Lớp học đầu tiên\Tất cả điều sai')
INSERT [dbo].[CauHoiThi] ([STT], [Ma_Phong], [Cau_Hoi], [Loi_Giai], [Dap_An]) VALUES (2, N'fpv-gnj-laj-001', N'Giáo viên tên gì?', N'Hồ Ánh Nguyệt', N'Hồ Ánh Nguyệt\Ánh Nguyệt\Thu Hà\Linh Tuyết')
INSERT [dbo].[CauHoiThi] ([STT], [Ma_Phong], [Cau_Hoi], [Loi_Giai], [Dap_An]) VALUES (2, N'tep-bje-sma-002', N'Giáo viên là ai?', N'Triệu Nguyễn Khánh Băng\Khánh Băng', N'Không có giáo viên\Triệu Nguyễn Khánh Băng\a và c đúng\Khánh Băng')
INSERT [dbo].[CauHoiThi] ([STT], [Ma_Phong], [Cau_Hoi], [Loi_Giai], [Dap_An]) VALUES (3, N'fpv-gnj-laj-001', N'Chủ đề lớp là gì?', N'Lập trình web\Lập trình .Net', N'Lập trình web\Lập trình Java\Lập trình .Net\Thiết kết web')
INSERT [dbo].[CauHoiThi] ([STT], [Ma_Phong], [Cau_Hoi], [Loi_Giai], [Dap_An]) VALUES (3, N'tep-bje-sma-002', N'Chủ đề lớp là gì?', N'Lập trình Web\Lập trình .Net', N'Lập trình Web\Lập trình .Net\Lập trình Android\Lập trình Java')
INSERT [dbo].[CauHoiThi] ([STT], [Ma_Phong], [Cau_Hoi], [Loi_Giai], [Dap_An]) VALUES (4, N'fpv-gnj-laj-001', N'Ứng dụng tên gì?', N'Dạy học trực tuyến', N'Dạy học trực tuyến\Quản lý dạy học\A & B điều đúng\A & B điều sai')
INSERT [dbo].[CauHoiThi] ([STT], [Ma_Phong], [Cau_Hoi], [Loi_Giai], [Dap_An]) VALUES (4, N'tep-bje-sma-002', N'Lớp có mấy thành viên?', N'2', N'1\2\10\5')
INSERT [dbo].[CauHoiThi] ([STT], [Ma_Phong], [Cau_Hoi], [Loi_Giai], [Dap_An]) VALUES (5, N'fpv-gnj-laj-001', N'Ngày tạo lớp học', N'20/04/2022', N'19/04/2022\20/04/2022\21/04/2022\22/04/2022')

INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (1, N'fpv-gnj-laj-001', N'U000002', 1, N'Lập trình web nâng cao')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (1, N'fpv-gnj-laj-001', N'U000002', 2, N'Lập trình web nâng cao')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (1, N'tep-bje-sma-002', N'U000002', 1, N'Lớp học đầu tiên')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (1, N'tep-bje-sma-002', N'U000002', 4, N'Không có tên')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (2, N'fpv-gnj-laj-001', N'U000002', 1, N'Hồ Ánh Nguyệt')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (2, N'fpv-gnj-laj-001', N'U000002', 2, N'Hồ Ánh Nguyệt')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (2, N'tep-bje-sma-002', N'U000002', 1, N'Triệu Nguyễn Khánh Băng\Khánh Băng')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (2, N'tep-bje-sma-002', N'U000002', 4, N'Không có giáo viên\Triệu Nguyễn Khánh Băng\a và c đúng')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (3, N'fpv-gnj-laj-001', N'U000002', 1, N'Lập trình .Net')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (3, N'tep-bje-sma-002', N'U000002', 1, N'Lập trình Web\Lập trình .Net')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (3, N'tep-bje-sma-002', N'U000002', 4, N'Lập trình Web\Lập trình Java')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (4, N'fpv-gnj-laj-001', N'U000002', 1, N'A & B điều đúng')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (4, N'tep-bje-sma-002', N'U000002', 4, N'2')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (5, N'fpv-gnj-laj-001', N'U000002', 1, N'20/04/2022')

INSERT [dbo].[Ghim] ([Ma_Bai], [Thoi_Gian]) VALUES (N'fpv-gnj-laj-002', CAST(0x0000AE7D00A24EE3 AS DateTime))
INSERT [dbo].[Ghim] ([Ma_Bai], [Thoi_Gian]) VALUES (N'fpv-gnj-laj-003', CAST(0x0000AE7D00A1B702 AS DateTime))

INSERT [dbo].[ThichTrang] ([Ma_YT], [Nguoi_Dung], [Nguoi_Thich], [Thoi_Gian]) VALUES (N'U000002_0000001', N'U000002', N'U000006', CAST(0x0000AE7D00A2315A AS DateTime))

INSERT [dbo].[ThoiGianLamBai] ([Ma_ND], [Ma_Phong], [Lan_Thu], [Bat_Dau], [Ket_Thuc]) VALUES (N'U000002', N'fpv-gnj-laj-001', 1, CAST(0x0000AE7D009EE25D AS DateTime), CAST(0x0000AE7D009EF83F AS DateTime))
INSERT [dbo].[ThoiGianLamBai] ([Ma_ND], [Ma_Phong], [Lan_Thu], [Bat_Dau], [Ket_Thuc]) VALUES (N'U000002', N'fpv-gnj-laj-001', 2, CAST(0x0000AE7D00C288DD AS DateTime), CAST(0x0000AE7D00C45624 AS DateTime))
INSERT [dbo].[ThoiGianLamBai] ([Ma_ND], [Ma_Phong], [Lan_Thu], [Bat_Dau], [Ket_Thuc]) VALUES (N'U000002', N'tep-bje-sma-002', 1, CAST(0x0000AE6E0189EE84 AS DateTime), NULL)
INSERT [dbo].[ThoiGianLamBai] ([Ma_ND], [Ma_Phong], [Lan_Thu], [Bat_Dau], [Ket_Thuc]) VALUES (N'U000002', N'tep-bje-sma-002', 2, CAST(0x0000AE6F017EF010 AS DateTime), NULL)
INSERT [dbo].[ThoiGianLamBai] ([Ma_ND], [Ma_Phong], [Lan_Thu], [Bat_Dau], [Ket_Thuc]) VALUES (N'U000002', N'tep-bje-sma-002', 3, CAST(0x0000AE720183A0DA AS DateTime), CAST(0x0000AE7201840278 AS DateTime))
INSERT [dbo].[ThoiGianLamBai] ([Ma_ND], [Ma_Phong], [Lan_Thu], [Bat_Dau], [Ket_Thuc]) VALUES (N'U000002', N'tep-bje-sma-002', 4, CAST(0x0000AE7600C1CB6E AS DateTime), CAST(0x0000AE7600C1F1D0 AS DateTime))

INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00001', N'U000002', N'Bài đăng mới', N'post\Từ lớp Lớp học đầu tiên', CAST(0x0000AE770174C32C AS DateTime), 1, N'Courses/Room/Detail?Room=tep-bje-sma')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00001', N'U000003', N'Bài đăng mới', N'post\Từ lớp Lập Trình Web Nâng Cao', CAST(0x0000AE7D00A107B9 AS DateTime), 0, N'Courses/Room/Detail?Room=fpv-gnj-laj')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00002', N'U000002', N'Bài đăng mới', N'post\Từ lớp Lớp học đầu tiên', CAST(0x0000AE77017CD6BD AS DateTime), 1, N'Courses/Room/Detail?Room=tep-bje-sma')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00002', N'U000003', N'Bài đăng mới', N'post\Từ lớp Lập Trình Web Nâng Cao', CAST(0x0000AE7D00A17AD7 AS DateTime), 0, N'Courses/Room/Detail?Room=fpv-gnj-laj')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00003', N'U000002', N'Bài đăng mới', N'post\Từ lớp Lớp học đầu tiên', CAST(0x0000AE77017D3EEC AS DateTime), 1, N'Courses/Room/Detail?Room=tep-bje-sma')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00003', N'U000003', N'Bài đăng mới', N'post\Từ lớp Lập Trình Web Nâng Cao', CAST(0x0000AE7D00A1F915 AS DateTime), 0, N'Courses/Room/Detail?Room=fpv-gnj-laj')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00004', N'U000002', N'Bài đăng mới', N'post\Từ lớp Lớp học đầu tiên', CAST(0x0000AE77017E6FE2 AS DateTime), 1, N'Courses/Room/Detail?Room=tep-bje-sma')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00004', N'U000003', N'Bài đăng mới', N'post\Từ lớp Lập Trình Web Nâng Cao', CAST(0x0000AE7D00A2C4CD AS DateTime), 0, N'Courses/Room/Detail?Room=fpv-gnj-laj')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00005', N'U000002', N'Bài đăng mới', N'post\Từ lớp Lớp học đầu tiên', CAST(0x0000AE77017ED211 AS DateTime), 1, N'Courses/Room/Detail?Room=tep-bje-sma')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00005', N'U000003', N'Bài đăng mới', N'post\Từ lớp Lập Trình Web Nâng Cao', CAST(0x0000AE7D00A31BFB AS DateTime), 1, N'Courses/Room/Detail?Room=fpv-gnj-laj')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00006', N'U000002', N'Bài đăng mới', N'post\Từ lớp Lớp học đầu tiên', CAST(0x0000AE77017EEC58 AS DateTime), 1, N'Courses/Room/Detail?Room=tep-bje-sma')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00007', N'U000002', N'Bài đăng mới', N'post\Từ lớp Lớp học đầu tiên', CAST(0x0000AE77017F0651 AS DateTime), 1, N'Courses/Room/Detail?Room=tep-bje-sma')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00008', N'U000002', N'Bài đăng mới', N'post\Từ lớp Lớp học đầu tiên', CAST(0x0000AE77017FAB01 AS DateTime), 1, N'Courses/Room/Detail?Room=tep-bje-sma')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00009', N'U000002', N'Bài đăng mới', N'post\Từ lớp Lớp học đầu tiên', CAST(0x0000AE77017FB006 AS DateTime), 1, N'Courses/Room/Detail?Room=tep-bje-sma')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00010', N'U000002', N'Bài đăng mới', N'post\Từ lớp Lập Trình Web Nâng Cao', CAST(0x0000AE7D00A107B4 AS DateTime), 0, N'Courses/Room/Detail?Room=fpv-gnj-laj')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00011', N'U000002', N'Bài đăng mới', N'post\Từ lớp Lập Trình Web Nâng Cao', CAST(0x0000AE7D00A17AD5 AS DateTime), 0, N'Courses/Room/Detail?Room=fpv-gnj-laj')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00012', N'U000002', N'Bài đăng mới', N'post\Từ lớp Lập Trình Web Nâng Cao', CAST(0x0000AE7D00A1F913 AS DateTime), 0, N'Courses/Room/Detail?Room=fpv-gnj-laj')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00013', N'U000002', N'Bài đăng mới', N'post\Từ lớp Lập Trình Web Nâng Cao', CAST(0x0000AE7D00A2C4CB AS DateTime), 0, N'Courses/Room/Detail?Room=fpv-gnj-laj')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00014', N'U000002', N'Bài đăng mới', N'post\Từ lớp Lập Trình Web Nâng Cao', CAST(0x0000AE7D00A31BF8 AS DateTime), 0, N'Courses/Room/Detail?Room=fpv-gnj-laj')

INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (1, N'U000001', N'U000002', CAST(0x0000AE7800000000 AS DateTime), N'Xin chào', 1)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (2, N'U000001', N'U000002', CAST(0x0000AE7900000000 AS DateTime), N'Bạn tên gì?', 1)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (3, N'U000002', N'U000001', CAST(0x0000AE7B00000000 AS DateTime), N'Chào bạn', 1)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (4, N'U000002', N'U000001', CAST(0x0000AE7B010857EF AS DateTime), N'Xin chào', 1)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (5, N'U000001', N'U000002', CAST(0x0000AE7B0108B186 AS DateTime), N'Chào lại', 1)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (6, N'U000002', N'U000001', CAST(0x0000AE7B010A0EDD AS DateTime), N'Chào tiếp', 1)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (7, N'U000001', N'U000002', CAST(0x0000AE7B010A47C1 AS DateTime), N'Lại chào', 1)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (8, N'U000001', N'U000002', CAST(0x0000AE7C010C1723 AS DateTime), N'Bạn tên gì', 0)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (9, N'U000001', N'U000002', CAST(0x0000AE7C010CE9F1 AS DateTime), N'Có thể cho mình làm quen không?', 0)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (10, N'U000001', N'U000002', CAST(0x0000AE7C01109713 AS DateTime), N'Bạn sống ở đâu?', 0)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (11, N'U000003', N'U000006', CAST(0x0000AE7D009E9CEC AS DateTime), N'Chào bạn', 1)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (12, N'U000003', N'U000006', CAST(0x0000AE7D009EA716 AS DateTime), N'Cho mình làm quen nhé!', 1)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (13, N'U000006', N'U000002', CAST(0x0000AE7D00A2301A AS DateTime), N'Chào tuyết', 0)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (14, N'U000002', N'U000006', CAST(0x0000AE7D00AF1AC9 AS DateTime), N'Bạn có ở đó không vậy', 1)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (15, N'U000006', N'U000002', CAST(0x0000AE7D00B13CF6 AS DateTime), N'Tôi đây', 0)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (16, N'U000006', N'U000002', CAST(0x0000AE7D00B14B32 AS DateTime), N'Bạn đang làm gì thế?', 0)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (17, N'U000006', N'U000002', CAST(0x0000AE7D00B15CAE AS DateTime), N'Có thể trò chuyện với tôi không?', 0)

INSERT [dbo].[XemTrang] ([Ma_XT], [Nguoi_Dung], [Nguoi_Xem], [Thoi_Gian]) VALUES (N'U000001_0000001', N'U000001', N'U000002', CAST(0x0000AE7B00FE67FE AS DateTime))
INSERT [dbo].[XemTrang] ([Ma_XT], [Nguoi_Dung], [Nguoi_Xem], [Thoi_Gian]) VALUES (N'U000002_0000001', N'U000002', N'U000001', CAST(0x0000AE7C010D0A9E AS DateTime))
INSERT [dbo].[XemTrang] ([Ma_XT], [Nguoi_Dung], [Nguoi_Xem], [Thoi_Gian]) VALUES (N'U000002_0000002', N'U000002', N'U000006', CAST(0x0000AE7D00A2255D AS DateTime))
INSERT [dbo].[XemTrang] ([Ma_XT], [Nguoi_Dung], [Nguoi_Xem], [Thoi_Gian]) VALUES (N'U000006_0000001', N'U000006', N'U000003', CAST(0x0000AE7D009E84F1 AS DateTime))
INSERT [dbo].[XemTrang] ([Ma_XT], [Nguoi_Dung], [Nguoi_Xem], [Thoi_Gian]) VALUES (N'U000006_0000002', N'U000006', N'U000002', CAST(0x0000AE7D00AF1081 AS DateTime))