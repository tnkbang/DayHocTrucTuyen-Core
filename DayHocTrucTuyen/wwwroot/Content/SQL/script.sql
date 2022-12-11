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

create table YeuCauThanhToan(
	Ma_ND char(7) references NguoiDung(Ma_ND),
	Loai_Thanh_Toan varchar(20) not null,
	So_Tai_Khoan varchar(20),
	So_Tien float not null,
	Trang_Thai int not null,
	Thoi_Gian datetime,
	Ghi_Chu nvarchar(200),
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
	Chi_Muc varchar(15) not null,
	Noi_Dung nvarchar(150) not null,
	Ghi_Chu nvarchar(500),
	Thoi_Gian datetime not null
)

create table ThongBao(
	Ma_TB char(5),
	Ma_ND char(7) references NguoiDung(Ma_ND),
	Loai_TB varchar(10) not null,
	Tieu_De nvarchar(20) not null,
	Noi_Dung nvarchar(500),
	Thoi_Gian datetime not null,
	Trang_Thai bit not null,
	Lien_Ket varchar(100),
	primary key (Ma_TB, Ma_ND)
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

create table LichSuTruyCap(
	Thoi_Gian datetime primary key,
	Ma_ND char(7) references NguoiDung(Ma_ND),
	Ma_Lop char(11) references LopHoc(Ma_Lop)
)

create table HocSinhThuocLop(
	Ma_ND char(7) references NguoiDung(Ma_ND),
	Ma_Lop char(11) references LopHoc(Ma_Lop),
	Ngay_Tham_Gia Datetime not null,
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
INSERT [dbo].[LoaiND] ([Ma_Loai], [Ten_Loai]) VALUES (N'02', N'Giáo viên')
INSERT [dbo].[LoaiND] ([Ma_Loai], [Ten_Loai]) VALUES (N'03', N'Học sinh')
GO
INSERT [dbo].[NguoiDung] ([Ma_ND], [Ma_Loai], [Ho_Lot], [Ten], [Ngay_Sinh], [Gioi_Tinh], [SDT], [Email], [Mat_Khau], [Img_Avt], [Img_BG], [Trang_Thai], [Mo_Ta], [Ngay_Tao], [Bi_Danh]) VALUES (N'U000001', N'01', N'Khánh', N'Băng', NULL, NULL, NULL, N'trieukhanhbang123@gmail.com', N'E10ADC3949BA59ABBE56E057F20F883E', N'avt-U000001-593.jpg', N'bg-U000001-205.jpg', 1, NULL, CAST(N'2022-03-27T17:54:25.220' AS DateTime), N'U000001')
INSERT [dbo].[NguoiDung] ([Ma_ND], [Ma_Loai], [Ho_Lot], [Ten], [Ngay_Sinh], [Gioi_Tinh], [SDT], [Email], [Mat_Khau], [Img_Avt], [Img_BG], [Trang_Thai], [Mo_Ta], [Ngay_Tao], [Bi_Danh]) VALUES (N'U000002', N'02', N'Lâm Linh', N'Tuyết', CAST(N'2022-11-25T00:00:00.000' AS DateTime), 2, N'01234567899', N'linhtuyet@gmail.com', N'E10ADC3949BA59ABBE56E057F20F883E', N'avt-U000002-478.jpg', N'bg-U000002-725.jpg', 1, NULL, CAST(N'2022-04-05T18:37:51.557' AS DateTime), N'U000002')
INSERT [dbo].[NguoiDung] ([Ma_ND], [Ma_Loai], [Ho_Lot], [Ten], [Ngay_Sinh], [Gioi_Tinh], [SDT], [Email], [Mat_Khau], [Img_Avt], [Img_BG], [Trang_Thai], [Mo_Ta], [Ngay_Tao], [Bi_Danh]) VALUES (N'U000003', N'03', N'Ngô Minh', N'Nguyệt', NULL, NULL, NULL, N'minhnguyet@gmail.com', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, NULL, 1, NULL, CAST(N'2022-04-20T09:21:05.347' AS DateTime), N'U000003')
INSERT [dbo].[NguoiDung] ([Ma_ND], [Ma_Loai], [Ho_Lot], [Ten], [Ngay_Sinh], [Gioi_Tinh], [SDT], [Email], [Mat_Khau], [Img_Avt], [Img_BG], [Trang_Thai], [Mo_Ta], [Ngay_Tao], [Bi_Danh]) VALUES (N'U000004', N'02', N'Lâm Thu', N'Hà', NULL, NULL, NULL, N'thuha@gmail.com', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, NULL, 1, NULL, CAST(N'2022-04-20T09:21:30.763' AS DateTime), N'U000004')
INSERT [dbo].[NguoiDung] ([Ma_ND], [Ma_Loai], [Ho_Lot], [Ten], [Ngay_Sinh], [Gioi_Tinh], [SDT], [Email], [Mat_Khau], [Img_Avt], [Img_BG], [Trang_Thai], [Mo_Ta], [Ngay_Tao], [Bi_Danh]) VALUES (N'U000005', N'03', N'Nguyễn Nhật', N'Duy', NULL, NULL, NULL, N'nhatduy@gmail.com', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, NULL, 1, NULL, CAST(N'2022-04-20T09:21:51.700' AS DateTime), N'U000005')
INSERT [dbo].[NguoiDung] ([Ma_ND], [Ma_Loai], [Ho_Lot], [Ten], [Ngay_Sinh], [Gioi_Tinh], [SDT], [Email], [Mat_Khau], [Img_Avt], [Img_BG], [Trang_Thai], [Mo_Ta], [Ngay_Tao], [Bi_Danh]) VALUES (N'U000006', N'02', N'Hồ Ánh', N'Nguyệt', NULL, NULL, NULL, N'anhnguyet@gmail.com', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, NULL, 1, NULL, CAST(N'2022-04-20T09:22:14.490' AS DateTime), N'U000006')
INSERT [dbo].[NguoiDung] ([Ma_ND], [Ma_Loai], [Ho_Lot], [Ten], [Ngay_Sinh], [Gioi_Tinh], [SDT], [Email], [Mat_Khau], [Img_Avt], [Img_BG], [Trang_Thai], [Mo_Ta], [Ngay_Tao], [Bi_Danh]) VALUES (N'U000007', N'03', N'Lâm Linh', N'Tuyết', NULL, NULL, NULL, N'linhtuyet@gamail.com', N'827CCB0EEA8A706C4C34A16891F84E7B', NULL, NULL, 1, NULL, CAST(N'2022-11-25T10:10:59.923' AS DateTime), N'U000007')
INSERT [dbo].[NguoiDung] ([Ma_ND], [Ma_Loai], [Ho_Lot], [Ten], [Ngay_Sinh], [Gioi_Tinh], [SDT], [Email], [Mat_Khau], [Img_Avt], [Img_BG], [Trang_Thai], [Mo_Ta], [Ngay_Tao], [Bi_Danh]) VALUES (N'U000008', N'03', N'Lê Quốc', N'Thắng', NULL, NULL, NULL, N'quocthang@gmail.com', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, NULL, 1, NULL, CAST(N'2022-12-11T11:22:30.647' AS DateTime), N'U000008')
INSERT [dbo].[NguoiDung] ([Ma_ND], [Ma_Loai], [Ho_Lot], [Ten], [Ngay_Sinh], [Gioi_Tinh], [SDT], [Email], [Mat_Khau], [Img_Avt], [Img_BG], [Trang_Thai], [Mo_Ta], [Ngay_Tao], [Bi_Danh]) VALUES (N'U000009', N'03', N'Nguyễn Hải', N'Dương', NULL, NULL, NULL, N'haiduong@gmail.com', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, NULL, 1, NULL, CAST(N'2022-12-11T11:27:21.187' AS DateTime), N'U000009')
INSERT [dbo].[NguoiDung] ([Ma_ND], [Ma_Loai], [Ho_Lot], [Ten], [Ngay_Sinh], [Gioi_Tinh], [SDT], [Email], [Mat_Khau], [Img_Avt], [Img_BG], [Trang_Thai], [Mo_Ta], [Ngay_Tao], [Bi_Danh]) VALUES (N'U000010', N'03', N'Lê Mộng', N'Tuyền', NULL, NULL, NULL, N'mongtuyen@gmail.com', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, NULL, 1, NULL, CAST(N'2022-12-11T11:28:07.330' AS DateTime), N'U000010')
INSERT [dbo].[NguoiDung] ([Ma_ND], [Ma_Loai], [Ho_Lot], [Ten], [Ngay_Sinh], [Gioi_Tinh], [SDT], [Email], [Mat_Khau], [Img_Avt], [Img_BG], [Trang_Thai], [Mo_Ta], [Ngay_Tao], [Bi_Danh]) VALUES (N'U000011', N'03', N'Hồ Tấn', N'Tài', NULL, NULL, NULL, N'tantai@gmail.com', N'E10ADC3949BA59ABBE56E057F20F883E', NULL, NULL, 1, NULL, CAST(N'2022-12-11T11:28:44.487' AS DateTime), N'U000011')
GO
INSERT [dbo].[ViNguoiDung] ([Ma_ND], [So_Du], [Ngay_Mo], [Trang_Thai]) VALUES (N'U000001', 109191, CAST(N'2022-11-19T02:03:51.013' AS DateTime), 1)
INSERT [dbo].[ViNguoiDung] ([Ma_ND], [So_Du], [Ngay_Mo], [Trang_Thai]) VALUES (N'U000002', 34493, CAST(N'2022-11-20T16:05:50.783' AS DateTime), 1)
INSERT [dbo].[ViNguoiDung] ([Ma_ND], [So_Du], [Ngay_Mo], [Trang_Thai]) VALUES (N'U000006', 72000, CAST(N'2022-11-24T10:22:27.750' AS DateTime), 1)
INSERT [dbo].[ViNguoiDung] ([Ma_ND], [So_Du], [Ngay_Mo], [Trang_Thai]) VALUES (N'U000008', 174192, CAST(N'2022-12-11T11:30:38.510' AS DateTime), 1)
GO
INSERT [dbo].[GoiNangCap] ([Ma_Goi], [Ten_Goi], [Gia_Tien], [Hieu_Luc], [Mo_Ta]) VALUES (1, N'Vip', 10000, 1, N'Gói nâng cấp thân thiết')
INSERT [dbo].[GoiNangCap] ([Ma_Goi], [Ten_Goi], [Gia_Tien], [Hieu_Luc], [Mo_Ta]) VALUES (2, N'SVip', 30000, 6, N'Gói nâng cấp tin cậy')
INSERT [dbo].[GoiNangCap] ([Ma_Goi], [Ten_Goi], [Gia_Tien], [Hieu_Luc], [Mo_Ta]) VALUES (3, N'Premium', 50000, 12, N'Gói nâng cấp tri kỷ')
GO
INSERT [dbo].[TrangThaiNangCap] ([Ma_ND], [Ma_Goi], [Ngay_Dang_Ky]) VALUES (N'U000001', 3, CAST(N'2022-11-25T08:28:59.743' AS DateTime))
GO
INSERT [dbo].[LichSuGiaoDich] ([Ma_ND], [Thoi_Gian], [Thu_Vao], [So_Tien], [So_Du], [Ghi_Chu]) VALUES (N'U000001', CAST(N'2022-11-19T02:03:51.040' AS DateTime), 1, 109191, 109191, N'Nhận tiền từ quà tặng.')
INSERT [dbo].[LichSuGiaoDich] ([Ma_ND], [Thoi_Gian], [Thu_Vao], [So_Tien], [So_Du], [Ghi_Chu]) VALUES (N'U000001', CAST(N'2022-11-19T02:40:44.667' AS DateTime), 0, 10000, 99191, N'Thực hiện rút tiền về tài khoản MoMo')
INSERT [dbo].[LichSuGiaoDich] ([Ma_ND], [Thoi_Gian], [Thu_Vao], [So_Tien], [So_Du], [Ghi_Chu]) VALUES (N'U000001', CAST(N'2022-11-19T02:56:02.337' AS DateTime), 1, 10000, 109191, N'Hoàn tiền do yêu cầu lúc 11/19/2022 2:40 AM bị từ chối')
INSERT [dbo].[LichSuGiaoDich] ([Ma_ND], [Thoi_Gian], [Thu_Vao], [So_Tien], [So_Du], [Ghi_Chu]) VALUES (N'U000001', CAST(N'2022-11-24T10:22:27.710' AS DateTime), 0, 60000, 49191, N'Phí tham gia lớp: Lập Trình Web')
INSERT [dbo].[LichSuGiaoDich] ([Ma_ND], [Thoi_Gian], [Thu_Vao], [So_Tien], [So_Du], [Ghi_Chu]) VALUES (N'U000001', CAST(N'2022-11-25T08:27:06.653' AS DateTime), 1, 50000, 149191, N'Nạp 50,000 VNĐ vào tài khoản.')
INSERT [dbo].[LichSuGiaoDich] ([Ma_ND], [Thoi_Gian], [Thu_Vao], [So_Tien], [So_Du], [Ghi_Chu]) VALUES (N'U000001', CAST(N'2022-11-25T08:28:59.730' AS DateTime), 0, 50000, 99191, N'Nâng cấp tài khoản với gói: Premium')
INSERT [dbo].[LichSuGiaoDich] ([Ma_ND], [Thoi_Gian], [Thu_Vao], [So_Tien], [So_Du], [Ghi_Chu]) VALUES (N'U000001', CAST(N'2022-11-25T08:42:02.810' AS DateTime), 1, 10000, 119191, N'Nạp 10,000 VNĐ vào tài khoản.')
INSERT [dbo].[LichSuGiaoDich] ([Ma_ND], [Thoi_Gian], [Thu_Vao], [So_Tien], [So_Du], [Ghi_Chu]) VALUES (N'U000002', CAST(N'2022-11-20T16:05:51.010' AS DateTime), 1, 144493, 144493, N'Nhận tiền từ quà tặng.')
INSERT [dbo].[LichSuGiaoDich] ([Ma_ND], [Thoi_Gian], [Thu_Vao], [So_Tien], [So_Du], [Ghi_Chu]) VALUES (N'U000002', CAST(N'2022-11-24T13:07:35.697' AS DateTime), 0, 10000, 134493, N'Thực hiện rút tiền về tài khoản MoMo')
INSERT [dbo].[LichSuGiaoDich] ([Ma_ND], [Thoi_Gian], [Thu_Vao], [So_Tien], [So_Du], [Ghi_Chu]) VALUES (N'U000002', CAST(N'2022-11-24T13:07:43.830' AS DateTime), 0, 10000, 124493, N'Thực hiện rút tiền về tài khoản AgriBank')
INSERT [dbo].[LichSuGiaoDich] ([Ma_ND], [Thoi_Gian], [Thu_Vao], [So_Tien], [So_Du], [Ghi_Chu]) VALUES (N'U000002', CAST(N'2022-11-25T10:18:36.867' AS DateTime), 0, 100000, 24493, N'Thực hiện rút tiền về tài khoản AgriBank')
INSERT [dbo].[LichSuGiaoDich] ([Ma_ND], [Thoi_Gian], [Thu_Vao], [So_Tien], [So_Du], [Ghi_Chu]) VALUES (N'U000002', CAST(N'2022-11-25T10:39:16.907' AS DateTime), 1, 10000, 34493, N'Hoàn tiền do yêu cầu lúc 11/24/2022 1:07 PM bị từ chối')
INSERT [dbo].[LichSuGiaoDich] ([Ma_ND], [Thoi_Gian], [Thu_Vao], [So_Tien], [So_Du], [Ghi_Chu]) VALUES (N'U000006', CAST(N'2022-11-24T10:22:27.763' AS DateTime), 1, 54000, 54000, N'Thu từ học sinh tham gia lớp: Lập Trình Web')
INSERT [dbo].[LichSuGiaoDich] ([Ma_ND], [Thoi_Gian], [Thu_Vao], [So_Tien], [So_Du], [Ghi_Chu]) VALUES (N'U000006', CAST(N'2022-12-11T11:30:43.180' AS DateTime), 1, 18000, 72000, N'Thu từ học sinh tham gia lớp: Toán - Lý')
INSERT [dbo].[LichSuGiaoDich] ([Ma_ND], [Thoi_Gian], [Thu_Vao], [So_Tien], [So_Du], [Ghi_Chu]) VALUES (N'U000008', CAST(N'2022-12-11T11:30:38.557' AS DateTime), 1, 194192, 194192, N'Nhận tiền từ quà tặng.')
INSERT [dbo].[LichSuGiaoDich] ([Ma_ND], [Thoi_Gian], [Thu_Vao], [So_Tien], [So_Du], [Ghi_Chu]) VALUES (N'U000008', CAST(N'2022-12-11T11:30:43.160' AS DateTime), 0, 20000, 174192, N'Phí tham gia lớp: Toán - Lý')
GO
INSERT [dbo].[YeuCauThanhToan] ([Ma_ND], [Loai_Thanh_Toan], [So_Tai_Khoan], [So_Tien], [Trang_Thai], [Thoi_Gian], [Ghi_Chu]) VALUES (N'U000001', N'MoMo', NULL, 10000, 0, CAST(N'2022-11-19T02:40:44.830' AS DateTime), N'không đủ thông tin cá nhân')
INSERT [dbo].[YeuCauThanhToan] ([Ma_ND], [Loai_Thanh_Toan], [So_Tai_Khoan], [So_Tien], [Trang_Thai], [Thoi_Gian], [Ghi_Chu]) VALUES (N'U000002', N'MoMo', NULL, 10000, 0, CAST(N'2022-11-24T13:07:35.713' AS DateTime), N'ddddd')
INSERT [dbo].[YeuCauThanhToan] ([Ma_ND], [Loai_Thanh_Toan], [So_Tai_Khoan], [So_Tien], [Trang_Thai], [Thoi_Gian], [Ghi_Chu]) VALUES (N'U000002', N'AgriBank', N'1111111111111111', 10000, 1, CAST(N'2022-11-24T13:07:43.837' AS DateTime), NULL)
INSERT [dbo].[YeuCauThanhToan] ([Ma_ND], [Loai_Thanh_Toan], [So_Tai_Khoan], [So_Tien], [Trang_Thai], [Thoi_Gian], [Ghi_Chu]) VALUES (N'U000002', N'AgriBank', N'111111111111111', 100000, 1, CAST(N'2022-11-25T10:18:36.893' AS DateTime), NULL)
GO
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (1, N'U000001', N'U000002', CAST(N'2022-04-15T00:00:00.000' AS DateTime), N'Xin chào', 1)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (2, N'U000001', N'U000002', CAST(N'2022-04-16T00:00:00.000' AS DateTime), N'Bạn tên gì?', 1)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (3, N'U000002', N'U000001', CAST(N'2022-04-18T00:00:00.000' AS DateTime), N'Chào bạn', 1)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (4, N'U000002', N'U000001', CAST(N'2022-04-18T16:02:26.717' AS DateTime), N'Xin chào', 1)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (5, N'U000001', N'U000002', CAST(N'2022-04-18T16:03:43.167' AS DateTime), N'Chào lại', 1)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (6, N'U000002', N'U000001', CAST(N'2022-04-18T16:08:41.270' AS DateTime), N'Chào tiếp', 1)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (7, N'U000001', N'U000002', CAST(N'2022-04-18T16:09:29.817' AS DateTime), N'Lại chào', 1)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (8, N'U000001', N'U000002', CAST(N'2022-04-19T16:16:05.237' AS DateTime), N'Bạn tên gì', 1)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (9, N'U000001', N'U000002', CAST(N'2022-04-19T16:19:05.123' AS DateTime), N'Có thể cho mình làm quen không?', 1)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (10, N'U000001', N'U000002', CAST(N'2022-04-19T16:32:28.223' AS DateTime), N'Bạn sống ở đâu?', 1)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (11, N'U000003', N'U000006', CAST(N'2022-04-20T09:37:29.533' AS DateTime), N'Chào bạn', 1)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (12, N'U000003', N'U000006', CAST(N'2022-04-20T09:37:38.207' AS DateTime), N'Cho mình làm quen nhé!', 1)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (13, N'U000006', N'U000002', CAST(N'2022-04-20T09:50:30.487' AS DateTime), N'Chào tuyết', 1)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (14, N'U000002', N'U000006', CAST(N'2022-04-20T10:37:32.190' AS DateTime), N'Bạn có ở đó không vậy', 1)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (15, N'U000006', N'U000002', CAST(N'2022-04-20T10:45:18.260' AS DateTime), N'Tôi đây', 1)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (16, N'U000006', N'U000002', CAST(N'2022-04-20T10:45:30.407' AS DateTime), N'Bạn đang làm gì thế?', 1)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (17, N'U000006', N'U000002', CAST(N'2022-04-20T10:45:45.327' AS DateTime), N'Có thể trò chuyện với tôi không?', 1)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (18, N'U000001', N'U000002', CAST(N'2022-11-20T00:29:52.743' AS DateTime), N'chào bạn', 1)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (19, N'U000002', N'U000001', CAST(N'2022-11-23T22:36:29.523' AS DateTime), N'chào', 1)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (20, N'U000001', N'U000002', CAST(N'2022-11-23T23:53:14.940' AS DateTime), N'chào bạn', 1)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (21, N'U000002', N'U000006', CAST(N'2022-11-25T10:33:12.570' AS DateTime), N'chào', 1)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (22, N'U000006', N'U000002', CAST(N'2022-11-25T10:33:19.997' AS DateTime), N'xin chào', 1)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (23, N'U000006', N'U000002', CAST(N'2022-11-25T10:33:31.993' AS DateTime), N'xin chào', 0)
INSERT [dbo].[TinNhan] ([ID], [Nguoi_Gui], [Nguoi_Nhan], [Thoi_Gian], [Noi_Dung], [Trang_Thai]) VALUES (24, N'U000006', N'U000002', CAST(N'2022-11-25T10:33:58.703' AS DateTime), N'dđ', 0)
GO
INSERT [dbo].[ThichTrang] ([Ma_YT], [Nguoi_Dung], [Nguoi_Thich], [Thoi_Gian]) VALUES (N'U000002_0000001', N'U000002', N'U000006', CAST(N'2022-04-20T09:50:31.553' AS DateTime))
INSERT [dbo].[ThichTrang] ([Ma_YT], [Nguoi_Dung], [Nguoi_Thich], [Thoi_Gian]) VALUES (N'U000002_0000002', N'U000002', N'U000001', CAST(N'2022-11-20T00:29:31.190' AS DateTime))
GO
INSERT [dbo].[XemTrang] ([Ma_XT], [Nguoi_Dung], [Nguoi_Xem], [Thoi_Gian]) VALUES (N'U000001_0000001', N'U000001', N'U000002', CAST(N'2022-04-18T15:26:15.887' AS DateTime))
INSERT [dbo].[XemTrang] ([Ma_XT], [Nguoi_Dung], [Nguoi_Xem], [Thoi_Gian]) VALUES (N'U000002_0000001', N'U000002', N'U000001', CAST(N'2022-04-19T16:19:33.007' AS DateTime))
INSERT [dbo].[XemTrang] ([Ma_XT], [Nguoi_Dung], [Nguoi_Xem], [Thoi_Gian]) VALUES (N'U000002_0000002', N'U000002', N'U000006', CAST(N'2022-04-20T09:50:21.323' AS DateTime))
INSERT [dbo].[XemTrang] ([Ma_XT], [Nguoi_Dung], [Nguoi_Xem], [Thoi_Gian]) VALUES (N'U000002_0000003', N'U000002', N'U000001', CAST(N'2022-11-19T02:02:29.733' AS DateTime))
INSERT [dbo].[XemTrang] ([Ma_XT], [Nguoi_Dung], [Nguoi_Xem], [Thoi_Gian]) VALUES (N'U000002_0000004', N'U000002', N'U000001', CAST(N'2022-11-23T23:31:14.947' AS DateTime))
INSERT [dbo].[XemTrang] ([Ma_XT], [Nguoi_Dung], [Nguoi_Xem], [Thoi_Gian]) VALUES (N'U000003_0000001', N'U000003', N'U000006', CAST(N'2022-11-24T12:28:52.853' AS DateTime))
INSERT [dbo].[XemTrang] ([Ma_XT], [Nguoi_Dung], [Nguoi_Xem], [Thoi_Gian]) VALUES (N'U000006_0000001', N'U000006', N'U000003', CAST(N'2022-04-20T09:37:09.070' AS DateTime))
INSERT [dbo].[XemTrang] ([Ma_XT], [Nguoi_Dung], [Nguoi_Xem], [Thoi_Gian]) VALUES (N'U000006_0000002', N'U000006', N'U000002', CAST(N'2022-04-20T10:37:23.417' AS DateTime))
GO
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00001', N'U000001', N'money', N'Từ chối rút tiền', N'Yêu cầu rút 10,000VNĐ lúc 11/19/2022 2:40 AM bị từ chối.', CAST(N'2022-11-19T02:56:02.300' AS DateTime), 0, N'#')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00001', N'U000002', N'post', N'Bài đăng mới', N'Từ lớp: Lớp học đầu tiên', CAST(N'2022-04-14T22:37:11.187' AS DateTime), 1, N'Courses/Room/Detail?id=tep-bje-sma')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00001', N'U000003', N'post', N'Bài đăng mới', N'Từ lớp: Lập Trình Web Nâng Cao', CAST(N'2022-04-20T09:46:17.577' AS DateTime), 0, N'Courses/Room/Detail?id=fpv-gnj-laj')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00001', N'U000006', N'post', N'Bài đăng mới', N'Từ lớp: Lớp học đầu tiên', CAST(N'2022-11-15T18:12:04.787' AS DateTime), 0, N'courses/room/detail/tep-bje-sma')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00002', N'U000001', N'post', N'Bài đăng mới', N'Từ lớp: Lập Trình Web Nâng Cao', CAST(N'2022-11-24T10:55:04.513' AS DateTime), 0, N'courses/room/detail/fpv-gnj-laj')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00002', N'U000002', N'post', N'Bài đăng mới', N'Từ lớp: Lớp học đầu tiên', CAST(N'2022-04-14T23:06:35.510' AS DateTime), 1, N'Courses/Room/Detail?id=tep-bje-sma')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00002', N'U000003', N'post', N'Bài đăng mới', N'Từ lớp: Lập Trình Web Nâng Cao', CAST(N'2022-04-20T09:47:55.810' AS DateTime), 0, N'Courses/Room/Detail?id=fpv-gnj-laj')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00002', N'U000006', N'post', N'Bài đăng mới', N'Từ lớp: Lớp học đầu tiên', CAST(N'2022-11-15T18:12:25.603' AS DateTime), 0, N'courses/room/detail/tep-bje-sma')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00003', N'U000001', N'post', N'Bài đăng mới', N'Từ lớp: Lập Trình Web Nâng Cao', CAST(N'2022-11-24T10:58:15.317' AS DateTime), 0, N'courses/room/detail/fpv-gnj-laj')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00003', N'U000002', N'post', N'Bài đăng mới', N'Từ lớp: Lớp học đầu tiên', CAST(N'2022-04-14T23:08:04.413' AS DateTime), 1, N'Courses/Room/Detail?id=tep-bje-sma')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00003', N'U000003', N'post', N'Bài đăng mới', N'Từ lớp: Lập Trình Web Nâng Cao', CAST(N'2022-04-20T09:49:43.537' AS DateTime), 0, N'Courses/Room/Detail?id=fpv-gnj-laj')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00003', N'U000006', N'exam', N'Bài thi mới', N'Từ lớp: Lớp học đầu tiên', CAST(N'2022-11-18T12:32:14.830' AS DateTime), 0, N'courses/room/detail/tep-bje-sma')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00004', N'U000001', N'exam', N'Bài thi mới', N'Từ lớp: Lập Trình Web Nâng Cao', CAST(N'2022-11-24T12:08:16.330' AS DateTime), 0, N'courses/room/detail/fpv-gnj-laj')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00004', N'U000002', N'post', N'Bài đăng mới', N'Từ lớp: Lớp học đầu tiên', CAST(N'2022-04-14T23:12:24.647' AS DateTime), 1, N'Courses/Room/Detail?id=tep-bje-sma')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00004', N'U000003', N'post', N'Bài đăng mới', N'Từ lớp: Lập Trình Web Nâng Cao', CAST(N'2022-04-20T09:52:37.377' AS DateTime), 0, N'Courses/Room/Detail?id=fpv-gnj-laj')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00004', N'U000006', N'post', N'Bài đăng mới', N'Từ lớp: Lớp học đầu tiên', CAST(N'2022-11-25T10:21:29.940' AS DateTime), 0, N'courses/room/detail/tep-bje-sma')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00005', N'U000001', N'exam', N'Bài thi mới', N'Từ lớp: Lập Trình Web Nâng Cao', CAST(N'2022-11-25T10:28:06.513' AS DateTime), 0, N'courses/room/detail/fpv-gnj-laj')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00005', N'U000002', N'post', N'Bài đăng mới', N'Từ lớp: Lớp học đầu tiên', CAST(N'2022-04-14T23:13:48.430' AS DateTime), 1, N'Courses/Room/Detail?id=tep-bje-sma')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00005', N'U000003', N'post', N'Bài đăng mới', N'Từ lớp: Lập Trình Web Nâng Cao', CAST(N'2022-04-20T09:53:51.770' AS DateTime), 1, N'Courses/Room/Detail?id=fpv-gnj-laj')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00006', N'U000002', N'post', N'Bài đăng mới', N'Từ lớp: Lớp học đầu tiên', CAST(N'2022-04-14T23:14:10.853' AS DateTime), 1, N'Courses/Room/Detail?id=tep-bje-sma')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00006', N'U000003', N'exam', N'Bài thi mới', N'Từ lớp: Lớp học đầu tiên', CAST(N'2022-11-18T12:32:14.820' AS DateTime), 0, N'courses/room/detail/tep-bje-sma')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00007', N'U000002', N'post', N'Bài đăng mới', N'Từ lớp: Lớp học đầu tiên', CAST(N'2022-04-14T23:14:33.017' AS DateTime), 1, N'Courses/Room/Detail?id=tep-bje-sma')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00007', N'U000003', N'post', N'Bài đăng mới', N'Từ lớp: Lập Trình Web Nâng Cao', CAST(N'2022-11-24T10:55:04.527' AS DateTime), 0, N'courses/room/detail/fpv-gnj-laj')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00008', N'U000002', N'post', N'Bài đăng mới', N'Từ lớp: Lớp học đầu tiên', CAST(N'2022-04-14T23:16:53.550' AS DateTime), 1, N'Courses/Room/Detail?id=tep-bje-sma')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00008', N'U000003', N'post', N'Bài đăng mới', N'Từ lớp: Lập Trình Web Nâng Cao', CAST(N'2022-11-24T10:58:15.323' AS DateTime), 0, N'courses/room/detail/fpv-gnj-laj')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00009', N'U000002', N'post', N'Bài đăng mới', N'Từ lớp: Lớp học đầu tiên', CAST(N'2022-04-14T23:16:57.833' AS DateTime), 1, N'Courses/Room/Detail?id=tep-bje-sma')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00009', N'U000003', N'exam', N'Bài thi mới', N'Từ lớp: Lập Trình Web Nâng Cao', CAST(N'2022-11-24T12:08:16.347' AS DateTime), 0, N'courses/room/detail/fpv-gnj-laj')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00010', N'U000002', N'post', N'Bài đăng mới', N'Từ lớp: Lập Trình Web Nâng Cao', CAST(N'2022-04-20T09:46:17.560' AS DateTime), 1, N'Courses/Room/Detail?id=fpv-gnj-laj')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00010', N'U000003', N'post', N'Bài đăng mới', N'Từ lớp: Lớp học đầu tiên', CAST(N'2022-11-25T10:21:29.923' AS DateTime), 0, N'courses/room/detail/tep-bje-sma')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00011', N'U000002', N'post', N'Bài đăng mới', N'Từ lớp: Lập Trình Web Nâng Cao', CAST(N'2022-04-20T09:47:55.803' AS DateTime), 1, N'Courses/Room/Detail?id=fpv-gnj-laj')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00011', N'U000003', N'post', N'Bài đăng mới', N'Từ lớp: Lập Trình Web Nâng Cao', CAST(N'2022-11-25T10:26:44.607' AS DateTime), 0, N'courses/room/detail/fpv-gnj-laj')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00012', N'U000002', N'post', N'Bài đăng mới', N'Từ lớp: Lập Trình Web Nâng Cao', CAST(N'2022-04-20T09:49:43.530' AS DateTime), 1, N'Courses/Room/Detail?id=fpv-gnj-laj')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00012', N'U000003', N'exam', N'Bài thi mới', N'Từ lớp: Lập Trình Web Nâng Cao', CAST(N'2022-11-25T10:28:06.527' AS DateTime), 0, N'courses/room/detail/fpv-gnj-laj')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00013', N'U000002', N'post', N'Bài đăng mới', N'Từ lớp: Lập Trình Web Nâng Cao', CAST(N'2022-04-20T09:52:37.370' AS DateTime), 1, N'Courses/Room/Detail?id=fpv-gnj-laj')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00014', N'U000002', N'post', N'Bài đăng mới', N'Từ lớp: Lập Trình Web Nâng Cao', CAST(N'2022-04-20T09:53:51.760' AS DateTime), 1, N'Courses/Room/Detail?id=fpv-gnj-laj')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00015', N'U000002', N'post', N'Bài đăng mới', N'Từ lớp: Lớp học đầu tiên', CAST(N'2022-11-15T18:12:04.760' AS DateTime), 1, N'courses/room/detail/tep-bje-sma')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00016', N'U000002', N'post', N'Bài đăng mới', N'Từ lớp: Lớp học đầu tiên', CAST(N'2022-11-15T18:12:25.593' AS DateTime), 1, N'courses/room/detail/tep-bje-sma')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00017', N'U000002', N'exam', N'Bài thi mới', N'Từ lớp: Lớp học đầu tiên', CAST(N'2022-11-18T12:32:14.740' AS DateTime), 1, N'courses/room/detail/tep-bje-sma')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00018', N'U000002', N'exam', N'Bài thi mới', N'Từ lớp: Lập Trình Web Nâng Cao', CAST(N'2022-11-24T12:08:16.340' AS DateTime), 1, N'courses/room/detail/fpv-gnj-laj')
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00019', N'U000002', N'admin', N'Thông báo từ Admin', N'tôi đã xem', CAST(N'2022-11-25T10:38:31.227' AS DateTime), 0, NULL)
INSERT [dbo].[ThongBao] ([Ma_TB], [Ma_ND], [Loai_TB], [Tieu_De], [Noi_Dung], [Thoi_Gian], [Trang_Thai], [Lien_Ket]) VALUES (N'00020', N'U000002', N'money', N'Từ chối rút tiền', N'Yêu cầu rút 10,000VNĐ lúc 11/24/2022 1:07 PM bị từ chối.', CAST(N'2022-11-25T10:39:16.897' AS DateTime), 0, N'#')
GO
INSERT [dbo].[LopHoc] ([Ma_Lop], [Ma_ND], [Ngay_Tao], [Ten_Lop], [Bi_Danh], [Gia_Tien], [Mo_Ta], [Trang_Thai], [Img_Bia]) VALUES (N'ajg-atv-ims', N'U000001', CAST(N'2022-04-14T23:45:33.100' AS DateTime), N'Test chương trình', N'ajg-atv-ims', 0, N'Lớp học tạo ra để kiểm tra các tính năng', 1, N'ajg-atv-ims-101.png')
INSERT [dbo].[LopHoc] ([Ma_Lop], [Ma_ND], [Ngay_Tao], [Ten_Lop], [Bi_Danh], [Gia_Tien], [Mo_Ta], [Trang_Thai], [Img_Bia]) VALUES (N'atk-spu-upa', N'U000002', CAST(N'2022-12-11T11:38:29.337' AS DateTime), N'AI không khó', N'atk-spu-upa', 0, N'Đào tạo kiến thức AI căn bản, buils 1 con AI giúp bán hàng nhanh.', 1, NULL)
INSERT [dbo].[LopHoc] ([Ma_Lop], [Ma_ND], [Ngay_Tao], [Ten_Lop], [Bi_Danh], [Gia_Tien], [Mo_Ta], [Trang_Thai], [Img_Bia]) VALUES (N'eep-phg-aop', N'U000004', CAST(N'2022-12-11T11:48:36.903' AS DateTime), N'AI thật dễ? Tại sao không?', N'eep-phg-aop', 0, NULL, 1, NULL)
INSERT [dbo].[LopHoc] ([Ma_Lop], [Ma_ND], [Ngay_Tao], [Ten_Lop], [Bi_Danh], [Gia_Tien], [Mo_Ta], [Trang_Thai], [Img_Bia]) VALUES (N'ets-mcd-epp', N'U000002', CAST(N'2022-12-11T11:42:57.443' AS DateTime), N'AI và bạn', N'ets-mcd-epp', 0, N'Hướng dẫn tạo AI trò chuyện với Python', 1, NULL)
INSERT [dbo].[LopHoc] ([Ma_Lop], [Ma_ND], [Ngay_Tao], [Ten_Lop], [Bi_Danh], [Gia_Tien], [Mo_Ta], [Trang_Thai], [Img_Bia]) VALUES (N'fpv-gnj-laj', N'U000006', CAST(N'2022-04-20T09:27:27.520' AS DateTime), N'Lập Trình Web Nâng Cao', N'laptrinhwebnangcao', 0, N'Dạy kỹ thuật nâng cao cho những bạn yêu thích mảng lập trình web.', 1, N'fpv-gnj-laj-883.png')
INSERT [dbo].[LopHoc] ([Ma_Lop], [Ma_ND], [Ngay_Tao], [Ten_Lop], [Bi_Danh], [Gia_Tien], [Mo_Ta], [Trang_Thai], [Img_Bia]) VALUES (N'hjh-dea-iuu', N'U000004', CAST(N'2022-12-11T11:46:34.863' AS DateTime), N'Nói không với ngoại ngữ?', N'hjh-dea-iuu', 0, N'Bạn đang sợ Tiếng Anh? Bạn không biết học ngoại ngữ như thế nào là đúng? Hãy để tôi giúp bạn.', 1, NULL)
INSERT [dbo].[LopHoc] ([Ma_Lop], [Ma_ND], [Ngay_Tao], [Ten_Lop], [Bi_Danh], [Gia_Tien], [Mo_Ta], [Trang_Thai], [Img_Bia]) VALUES (N'hru-bpv-nul', N'U000006', CAST(N'2022-12-11T11:07:13.437' AS DateTime), N'Lớp Toán cô Nguyệt', N'hru-bpv-nul', 20000, N'Dạy toán cho học sinh lớp 12', 1, NULL)
INSERT [dbo].[LopHoc] ([Ma_Lop], [Ma_ND], [Ngay_Tao], [Ten_Lop], [Bi_Danh], [Gia_Tien], [Mo_Ta], [Trang_Thai], [Img_Bia]) VALUES (N'ifd-tcm-anp', N'U000001', CAST(N'2022-12-11T11:36:18.787' AS DateTime), N'Tiếng Anh căn bản', N'ifd-tcm-anp', 0, N'Đào tạo tiếng anh cho các bạn mất gốc, đảm bảo chất lượng IELS trên 6.5', 1, NULL)
INSERT [dbo].[LopHoc] ([Ma_Lop], [Ma_ND], [Ngay_Tao], [Ten_Lop], [Bi_Danh], [Gia_Tien], [Mo_Ta], [Trang_Thai], [Img_Bia]) VALUES (N'jki-mfu-mvc', N'U000004', CAST(N'2022-12-11T11:47:17.310' AS DateTime), N'Lập trình PHP', N'jki-mfu-mvc', 0, N'Hướng dẫn lập trình web với PHP', 1, NULL)
INSERT [dbo].[LopHoc] ([Ma_Lop], [Ma_ND], [Ngay_Tao], [Ten_Lop], [Bi_Danh], [Gia_Tien], [Mo_Ta], [Trang_Thai], [Img_Bia]) VALUES (N'kfl-qpg-jgq', N'U000001', CAST(N'2022-12-11T11:34:02.123' AS DateTime), N'Hóa - Sinh', N'kfl-qpg-jgq', 0, N'Dạy hóa học và sinh học cho các bạn lớp 12 mất căn bản', 1, NULL)
INSERT [dbo].[LopHoc] ([Ma_Lop], [Ma_ND], [Ngay_Tao], [Ten_Lop], [Bi_Danh], [Gia_Tien], [Mo_Ta], [Trang_Thai], [Img_Bia]) VALUES (N'kge-gbn-vdl', N'U000006', CAST(N'2022-12-11T09:51:05.090' AS DateTime), N'Lập Trình .Net', N'kge-gbn-vdl', 0, N'Lớp dạy lập trình .net căn bản', 1, NULL)
INSERT [dbo].[LopHoc] ([Ma_Lop], [Ma_ND], [Ngay_Tao], [Ten_Lop], [Bi_Danh], [Gia_Tien], [Mo_Ta], [Trang_Thai], [Img_Bia]) VALUES (N'mfo-chj-hta', N'U000006', CAST(N'2022-04-20T09:24:44.187' AS DateTime), N'Lập Trình Web', N'mfo-chj-hta', 60000, N'Dành cho những ai có yêu thích lập trình web', 1, NULL)
INSERT [dbo].[LopHoc] ([Ma_Lop], [Ma_ND], [Ngay_Tao], [Ten_Lop], [Bi_Danh], [Gia_Tien], [Mo_Ta], [Trang_Thai], [Img_Bia]) VALUES (N'njt-grc-eis', N'U000004', CAST(N'2022-12-11T11:50:19.897' AS DateTime), N'PHP căn bản', N'njt-grc-eis', 0, NULL, 1, NULL)
INSERT [dbo].[LopHoc] ([Ma_Lop], [Ma_ND], [Ngay_Tao], [Ten_Lop], [Bi_Danh], [Gia_Tien], [Mo_Ta], [Trang_Thai], [Img_Bia]) VALUES (N'nqa-sot-ofo', N'U000004', CAST(N'2022-12-11T11:50:03.683' AS DateTime), N'Phython căn bản', N'nqa-sot-ofo', 0, NULL, 1, NULL)
INSERT [dbo].[LopHoc] ([Ma_Lop], [Ma_ND], [Ngay_Tao], [Ten_Lop], [Bi_Danh], [Gia_Tien], [Mo_Ta], [Trang_Thai], [Img_Bia]) VALUES (N'pqr-uvl-hmu', N'U000006', CAST(N'2022-04-20T09:25:37.443' AS DateTime), N'Java Cho Người Mới', N'pqr-uvl-hmu', 150000, N'Dạy java căn bản cho người mới và các bạn mất kiến thức căn bản', 1, NULL)
INSERT [dbo].[LopHoc] ([Ma_Lop], [Ma_ND], [Ngay_Tao], [Ten_Lop], [Bi_Danh], [Gia_Tien], [Mo_Ta], [Trang_Thai], [Img_Bia]) VALUES (N'rcm-lqv-mhi', N'U000002', CAST(N'2022-12-11T11:40:16.463' AS DateTime), N'Hướng đối tượng OOP', N'rcm-lqv-mhi', 0, N'Thế nào là lập trình hướng đối tượng? Ứng dụng của nó là gì?', 1, NULL)
INSERT [dbo].[LopHoc] ([Ma_Lop], [Ma_ND], [Ngay_Tao], [Ten_Lop], [Bi_Danh], [Gia_Tien], [Mo_Ta], [Trang_Thai], [Img_Bia]) VALUES (N'svp-aku-qgd', N'U000004', CAST(N'2022-12-11T11:48:05.697' AS DateTime), N'Web với C#', N'svp-aku-qgd', 0, NULL, 1, NULL)
INSERT [dbo].[LopHoc] ([Ma_Lop], [Ma_ND], [Ngay_Tao], [Ten_Lop], [Bi_Danh], [Gia_Tien], [Mo_Ta], [Trang_Thai], [Img_Bia]) VALUES (N'tep-bje-sma', N'U000001', CAST(N'2022-03-27T17:56:28.623' AS DateTime), N'Lớp học đầu tiên', N'tep-bje-sma', 350000, NULL, 1, N'tep-bje-sma-622.png')
INSERT [dbo].[LopHoc] ([Ma_Lop], [Ma_ND], [Ngay_Tao], [Ten_Lop], [Bi_Danh], [Gia_Tien], [Mo_Ta], [Trang_Thai], [Img_Bia]) VALUES (N'tfs-cle-cjf', N'U000006', CAST(N'2022-12-11T11:16:27.110' AS DateTime), N'Toán - Lý', N'tfs-cle-cjf', 20000, N'Lớp dạy toán lý ôn tập cho các bạn thi đại học', 1, NULL)
INSERT [dbo].[LopHoc] ([Ma_Lop], [Ma_ND], [Ngay_Tao], [Ten_Lop], [Bi_Danh], [Gia_Tien], [Mo_Ta], [Trang_Thai], [Img_Bia]) VALUES (N'tig-ptk-sok', N'U000001', CAST(N'2022-12-11T11:35:07.140' AS DateTime), N'Ngữ văn 12', N'tig-ptk-sok', 0, N'Sự giàu đẹp của thơ ca, văn học Việt Nam trong chương trình giảng dạy lớp 12.', 1, NULL)
INSERT [dbo].[LopHoc] ([Ma_Lop], [Ma_ND], [Ngay_Tao], [Ten_Lop], [Bi_Danh], [Gia_Tien], [Mo_Ta], [Trang_Thai], [Img_Bia]) VALUES (N'tsm-gat-pas', N'U000002', CAST(N'2022-12-11T11:39:38.633' AS DateTime), N'Quy tắc lập trình', N'tsm-gat-pas', 0, N'Dạy cơ bản về các quy tắc lập trình căn bản', 1, NULL)
INSERT [dbo].[LopHoc] ([Ma_Lop], [Ma_ND], [Ngay_Tao], [Ten_Lop], [Bi_Danh], [Gia_Tien], [Mo_Ta], [Trang_Thai], [Img_Bia]) VALUES (N'unu-lqn-hph', N'U000002', CAST(N'2022-12-11T11:42:06.890' AS DateTime), N'Lý thuyết đồ thị', N'unu-lqn-hph', 0, NULL, 1, NULL)
GO
INSERT [dbo].[DanhGiaLop] ([Ma_DG], [Ma_ND], [Ma_Lop], [Muc_Do], [Ghi_Chu], [Thoi_Gian]) VALUES (N'juEHjqQmxulPPyp', N'U000002', N'fpv-gnj-laj', 4, NULL, CAST(N'2022-11-25T10:38:05.217' AS DateTime))
INSERT [dbo].[DanhGiaLop] ([Ma_DG], [Ma_ND], [Ma_Lop], [Muc_Do], [Ghi_Chu], [Thoi_Gian]) VALUES (N'KXrWZPehshKEkZx', N'U000001', N'fpv-gnj-laj', 4, N'Lớp học xịn', CAST(N'2022-11-23T21:13:58.863' AS DateTime))
GO
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-15T18:02:14.777' AS DateTime), N'U000001', N'fpv-gnj-laj')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-15T18:06:06.953' AS DateTime), N'U000001', N'tep-bje-sma')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-15T18:17:07.040' AS DateTime), N'U000003', N'ajg-atv-ims')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-15T18:17:26.287' AS DateTime), N'U000003', N'tep-bje-sma')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-18T12:10:18.467' AS DateTime), N'U000001', N'ajg-atv-ims')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-18T12:10:35.170' AS DateTime), N'U000001', N'tep-bje-sma')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-18T12:12:04.153' AS DateTime), N'U000002', N'tep-bje-sma')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-18T13:11:11.353' AS DateTime), N'U000001', N'tep-bje-sma')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-18T13:12:06.717' AS DateTime), N'U000002', N'tep-bje-sma')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-18T17:47:56.413' AS DateTime), N'U000001', N'tep-bje-sma')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-18T17:53:52.010' AS DateTime), N'U000001', N'fpv-gnj-laj')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-18T18:49:32.297' AS DateTime), N'U000001', N'tep-bje-sma')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-18T22:41:32.383' AS DateTime), N'U000001', N'tep-bje-sma')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-18T23:29:13.730' AS DateTime), N'U000002', N'tep-bje-sma')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-19T00:03:34.213' AS DateTime), N'U000001', N'tep-bje-sma')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-19T01:05:32.710' AS DateTime), N'U000001', N'tep-bje-sma')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-19T02:04:40.933' AS DateTime), N'U000001', N'fpv-gnj-laj')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-19T02:07:21.810' AS DateTime), N'U000001', N'tep-bje-sma')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-19T20:03:34.847' AS DateTime), N'U000001', N'fpv-gnj-laj')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-19T23:22:35.720' AS DateTime), N'U000002', N'fpv-gnj-laj')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-20T00:24:16.473' AS DateTime), N'U000002', N'fpv-gnj-laj')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-20T16:09:45.700' AS DateTime), N'U000001', N'fpv-gnj-laj')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-21T16:21:00.930' AS DateTime), N'U000001', N'ajg-atv-ims')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-21T16:30:42.693' AS DateTime), N'U000001', N'fpv-gnj-laj')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-23T12:35:20.137' AS DateTime), N'U000001', N'fpv-gnj-laj')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-23T12:55:16.340' AS DateTime), N'U000001', N'tep-bje-sma')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-23T13:35:45.347' AS DateTime), N'U000001', N'fpv-gnj-laj')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-23T13:37:08.443' AS DateTime), N'U000001', N'ajg-atv-ims')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-23T14:37:10.313' AS DateTime), N'U000001', N'ajg-atv-ims')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-23T15:02:46.773' AS DateTime), N'U000001', N'fpv-gnj-laj')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-23T15:27:44.950' AS DateTime), N'U000001', N'tep-bje-sma')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-23T15:39:14.867' AS DateTime), N'U000001', N'ajg-atv-ims')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-23T16:03:06.170' AS DateTime), N'U000001', N'fpv-gnj-laj')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-23T16:39:39.650' AS DateTime), N'U000001', N'ajg-atv-ims')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-23T17:11:48.260' AS DateTime), N'U000001', N'fpv-gnj-laj')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-23T20:57:12.830' AS DateTime), N'U000001', N'tep-bje-sma')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-23T21:01:16.793' AS DateTime), N'U000001', N'fpv-gnj-laj')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-24T00:06:33.770' AS DateTime), N'U000001', N'ajg-atv-ims')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-24T09:50:11.973' AS DateTime), N'U000001', N'ajg-atv-ims')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-24T10:13:13.490' AS DateTime), N'U000001', N'mfo-chj-hta')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-24T10:27:46.013' AS DateTime), N'U000002', N'fpv-gnj-laj')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-24T10:28:23.847' AS DateTime), N'U000002', N'tep-bje-sma')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-24T11:23:36.823' AS DateTime), N'U000001', N'ajg-atv-ims')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-24T11:23:41.643' AS DateTime), N'U000001', N'tep-bje-sma')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-24T11:36:58.290' AS DateTime), N'U000002', N'tep-bje-sma')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-24T11:42:21.013' AS DateTime), N'U000002', N'fpv-gnj-laj')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-24T11:56:38.343' AS DateTime), N'U000006', N'fpv-gnj-laj')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-25T10:20:38.343' AS DateTime), N'U000002', N'fpv-gnj-laj')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-25T10:21:09.997' AS DateTime), N'U000002', N'tep-bje-sma')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-25T10:24:45.113' AS DateTime), N'U000006', N'fpv-gnj-laj')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-25T10:25:58.847' AS DateTime), N'U000001', N'fpv-gnj-laj')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-25T10:35:39.327' AS DateTime), N'U000006', N'tep-bje-sma')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-11-25T10:42:42.620' AS DateTime), N'U000001', N'mfo-chj-hta')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-12-11T09:51:05.870' AS DateTime), N'U000006', N'kge-gbn-vdl')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-12-11T10:31:28.723' AS DateTime), N'U000006', N'fpv-gnj-laj')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-12-11T10:51:43.183' AS DateTime), N'U000006', N'kge-gbn-vdl')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-12-11T11:07:13.997' AS DateTime), N'U000006', N'hru-bpv-nul')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-12-11T11:16:27.343' AS DateTime), N'U000006', N'tfs-cle-cjf')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-12-11T11:19:35.890' AS DateTime), N'U000002', N'hru-bpv-nul')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-12-11T11:28:52.573' AS DateTime), N'U000011', N'kge-gbn-vdl')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-12-11T11:29:30.367' AS DateTime), N'U000010', N'kge-gbn-vdl')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-12-11T11:30:10.060' AS DateTime), N'U000008', N'kge-gbn-vdl')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-12-11T11:30:24.600' AS DateTime), N'U000008', N'tfs-cle-cjf')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-12-11T11:31:11.830' AS DateTime), N'U000009', N'kge-gbn-vdl')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-12-11T11:31:41.913' AS DateTime), N'U000002', N'kge-gbn-vdl')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-12-11T11:34:02.340' AS DateTime), N'U000001', N'kfl-qpg-jgq')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-12-11T11:35:07.207' AS DateTime), N'U000001', N'tig-ptk-sok')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-12-11T11:36:18.863' AS DateTime), N'U000001', N'ifd-tcm-anp')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-12-11T11:38:29.390' AS DateTime), N'U000002', N'atk-spu-upa')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-12-11T11:39:38.700' AS DateTime), N'U000002', N'tsm-gat-pas')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-12-11T11:40:16.527' AS DateTime), N'U000002', N'rcm-lqv-mhi')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-12-11T11:42:06.963' AS DateTime), N'U000002', N'unu-lqn-hph')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-12-11T11:42:57.513' AS DateTime), N'U000002', N'ets-mcd-epp')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-12-11T11:45:13.507' AS DateTime), N'U000004', N'kge-gbn-vdl')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-12-11T11:46:34.987' AS DateTime), N'U000004', N'hjh-dea-iuu')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-12-11T11:47:17.400' AS DateTime), N'U000004', N'jki-mfu-mvc')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-12-11T11:48:05.820' AS DateTime), N'U000004', N'svp-aku-qgd')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-12-11T11:48:37.040' AS DateTime), N'U000004', N'eep-phg-aop')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-12-11T11:50:03.767' AS DateTime), N'U000004', N'nqa-sot-ofo')
INSERT [dbo].[LichSuTruyCap] ([Thoi_Gian], [Ma_ND], [Ma_Lop]) VALUES (CAST(N'2022-12-11T11:50:19.997' AS DateTime), N'U000004', N'njt-grc-eis')
GO
INSERT [dbo].[HocSinhThuocLop] ([Ma_ND], [Ma_Lop], [Ngay_Tham_Gia]) VALUES (N'U000001', N'fpv-gnj-laj', CAST(N'2022-11-19T20:20:33.250' AS DateTime))
INSERT [dbo].[HocSinhThuocLop] ([Ma_ND], [Ma_Lop], [Ngay_Tham_Gia]) VALUES (N'U000001', N'mfo-chj-hta', CAST(N'2022-11-24T10:22:27.767' AS DateTime))
INSERT [dbo].[HocSinhThuocLop] ([Ma_ND], [Ma_Lop], [Ngay_Tham_Gia]) VALUES (N'U000002', N'fpv-gnj-laj', CAST(N'2022-11-25T10:30:21.760' AS DateTime))
INSERT [dbo].[HocSinhThuocLop] ([Ma_ND], [Ma_Lop], [Ngay_Tham_Gia]) VALUES (N'U000002', N'kge-gbn-vdl', CAST(N'2022-12-11T11:31:47.147' AS DateTime))
INSERT [dbo].[HocSinhThuocLop] ([Ma_ND], [Ma_Lop], [Ngay_Tham_Gia]) VALUES (N'U000002', N'tep-bje-sma', CAST(N'2022-04-05T18:38:55.017' AS DateTime))
INSERT [dbo].[HocSinhThuocLop] ([Ma_ND], [Ma_Lop], [Ngay_Tham_Gia]) VALUES (N'U000003', N'ajg-atv-ims', CAST(N'2022-11-15T18:17:10.293' AS DateTime))
INSERT [dbo].[HocSinhThuocLop] ([Ma_ND], [Ma_Lop], [Ngay_Tham_Gia]) VALUES (N'U000003', N'fpv-gnj-laj', CAST(N'2022-04-20T09:37:04.107' AS DateTime))
INSERT [dbo].[HocSinhThuocLop] ([Ma_ND], [Ma_Lop], [Ngay_Tham_Gia]) VALUES (N'U000003', N'tep-bje-sma', CAST(N'2022-11-15T18:17:28.943' AS DateTime))
INSERT [dbo].[HocSinhThuocLop] ([Ma_ND], [Ma_Lop], [Ngay_Tham_Gia]) VALUES (N'U000004', N'kge-gbn-vdl', CAST(N'2022-12-11T11:45:24.367' AS DateTime))
INSERT [dbo].[HocSinhThuocLop] ([Ma_ND], [Ma_Lop], [Ngay_Tham_Gia]) VALUES (N'U000006', N'tep-bje-sma', CAST(N'2022-04-20T10:15:56.070' AS DateTime))
INSERT [dbo].[HocSinhThuocLop] ([Ma_ND], [Ma_Lop], [Ngay_Tham_Gia]) VALUES (N'U000008', N'kge-gbn-vdl', CAST(N'2022-12-11T11:30:14.097' AS DateTime))
INSERT [dbo].[HocSinhThuocLop] ([Ma_ND], [Ma_Lop], [Ngay_Tham_Gia]) VALUES (N'U000008', N'tfs-cle-cjf', CAST(N'2022-12-11T11:30:43.183' AS DateTime))
INSERT [dbo].[HocSinhThuocLop] ([Ma_ND], [Ma_Lop], [Ngay_Tham_Gia]) VALUES (N'U000009', N'kge-gbn-vdl', CAST(N'2022-12-11T11:31:15.280' AS DateTime))
INSERT [dbo].[HocSinhThuocLop] ([Ma_ND], [Ma_Lop], [Ngay_Tham_Gia]) VALUES (N'U000010', N'kge-gbn-vdl', CAST(N'2022-12-11T11:29:33.373' AS DateTime))
INSERT [dbo].[HocSinhThuocLop] ([Ma_ND], [Ma_Lop], [Ngay_Tham_Gia]) VALUES (N'U000011', N'kge-gbn-vdl', CAST(N'2022-12-11T11:28:57.107' AS DateTime))
GO
INSERT [dbo].[BaiDang] ([Ma_Bai], [Ma_Lop], [Ma_ND], [Thoi_Gian], [Noi_Dung], [Dinh_Kem], [Trang_Thai]) VALUES (N'fpv-gnj-laj-001', N'fpv-gnj-laj', N'U000002', CAST(N'2022-04-20T09:39:43.323' AS DateTime), N'Bài đăng nhiều ảnh', N'fpv-gnj-laj-001-1554969988_hinh-nen-4k-tuyet-dep-cho-may-tinh-08.png,fpv-gnj-laj-001-anh-hoa-oai-huong-dep.png,fpv-gnj-laj-001-bien-dao-co-to.png,fpv-gnj-laj-001-doi-mong-mo-1.png,fpv-gnj-laj-001-ec6b2b877f7a20be4ab082f317fd8a3d.png', 1)
INSERT [dbo].[BaiDang] ([Ma_Bai], [Ma_Lop], [Ma_ND], [Thoi_Gian], [Noi_Dung], [Dinh_Kem], [Trang_Thai]) VALUES (N'fpv-gnj-laj-002', N'fpv-gnj-laj', N'U000002', CAST(N'2022-04-20T09:46:17.527' AS DateTime), N'Bài đăng 1 ảnh', N'fpv-gnj-laj-002-bien-dao-co-to.png', 1)
INSERT [dbo].[BaiDang] ([Ma_Bai], [Ma_Lop], [Ma_ND], [Thoi_Gian], [Noi_Dung], [Dinh_Kem], [Trang_Thai]) VALUES (N'fpv-gnj-laj-003', N'fpv-gnj-laj', N'U000006', CAST(N'2022-04-20T09:47:55.790' AS DateTime), N'Bài đăng này được ghim nhé', NULL, 0)
INSERT [dbo].[BaiDang] ([Ma_Bai], [Ma_Lop], [Ma_ND], [Thoi_Gian], [Noi_Dung], [Dinh_Kem], [Trang_Thai]) VALUES (N'fpv-gnj-laj-004', N'fpv-gnj-laj', N'U000006', CAST(N'2022-04-20T09:49:43.483' AS DateTime), N'Bài đăng nhiều file', N'fpv-gnj-laj-004-BAI TAP TONG HOP.pdf,fpv-gnj-laj-004-BAOCAO_QTDA.ppt,fpv-gnj-laj-004-New Text Document.txt,fpv-gnj-laj-004-Sodo Pert_baitap_ON TONG HOP.xlsx,fpv-gnj-laj-004-Thuy?t trình.docx', 1)
INSERT [dbo].[BaiDang] ([Ma_Bai], [Ma_Lop], [Ma_ND], [Thoi_Gian], [Noi_Dung], [Dinh_Kem], [Trang_Thai]) VALUES (N'fpv-gnj-laj-005', N'fpv-gnj-laj', N'U000003', CAST(N'2022-04-20T09:53:51.747' AS DateTime), N'Chào mọi người nha !!!', NULL, 1)
INSERT [dbo].[BaiDang] ([Ma_Bai], [Ma_Lop], [Ma_ND], [Thoi_Gian], [Noi_Dung], [Dinh_Kem], [Trang_Thai]) VALUES (N'fpv-gnj-laj-006', N'fpv-gnj-laj', N'U000002', CAST(N'2022-11-24T10:55:04.383' AS DateTime), N'bài đăng có 3 ảnh', N'fpv-gnj-laj-006-382.jpg,fpv-gnj-laj-006-418.jpg,fpv-gnj-laj-006-432.png', 1)
INSERT [dbo].[BaiDang] ([Ma_Bai], [Ma_Lop], [Ma_ND], [Thoi_Gian], [Noi_Dung], [Dinh_Kem], [Trang_Thai]) VALUES (N'fpv-gnj-laj-007', N'fpv-gnj-laj', N'U000002', CAST(N'2022-11-24T10:58:15.287' AS DateTime), NULL, N'fpv-gnj-laj-007-287.jpg', 1)
INSERT [dbo].[BaiDang] ([Ma_Bai], [Ma_Lop], [Ma_ND], [Thoi_Gian], [Noi_Dung], [Dinh_Kem], [Trang_Thai]) VALUES (N'fpv-gnj-laj-008', N'fpv-gnj-laj', N'U000001', CAST(N'2022-11-25T10:26:44.590' AS DateTime), N'fddddddfffd', NULL, 1)
INSERT [dbo].[BaiDang] ([Ma_Bai], [Ma_Lop], [Ma_ND], [Thoi_Gian], [Noi_Dung], [Dinh_Kem], [Trang_Thai]) VALUES (N'hru-bpv-nul-001', N'hru-bpv-nul', N'U000006', CAST(N'2022-12-11T11:14:41.910' AS DateTime), N'Các thành viên lớp', N'hru-bpv-nul-001-Danh sách l?p.xlsx', 1)
INSERT [dbo].[BaiDang] ([Ma_Bai], [Ma_Lop], [Ma_ND], [Thoi_Gian], [Noi_Dung], [Dinh_Kem], [Trang_Thai]) VALUES (N'hru-bpv-nul-002', N'hru-bpv-nul', N'U000006', CAST(N'2022-12-11T11:14:58.060' AS DateTime), NULL, N'hru-bpv-nul-002-Hàm s?.docx', 1)
INSERT [dbo].[BaiDang] ([Ma_Bai], [Ma_Lop], [Ma_ND], [Thoi_Gian], [Noi_Dung], [Dinh_Kem], [Trang_Thai]) VALUES (N'hru-bpv-nul-003', N'hru-bpv-nul', N'U000006', CAST(N'2022-12-11T11:15:07.827' AS DateTime), NULL, N'hru-bpv-nul-003-Nguyên hàm.pptx,hru-bpv-nul-003-Tích phân.docx', 1)
INSERT [dbo].[BaiDang] ([Ma_Bai], [Ma_Lop], [Ma_ND], [Thoi_Gian], [Noi_Dung], [Dinh_Kem], [Trang_Thai]) VALUES (N'kge-gbn-vdl-001', N'kge-gbn-vdl', N'U000006', CAST(N'2022-12-11T10:00:17.717' AS DateTime), N'Tài liệu cơ bản', N'kge-gbn-vdl-001-Giao_trinh_SQL.pdf,kge-gbn-vdl-001-Luu_y_khi_tao_Form.pdf,kge-gbn-vdl-001-SQL_Server_Thao_tac_co_ban.pdf,kge-gbn-vdl-001-Su dung ComboBox trong Form.pdf,kge-gbn-vdl-001-Them_thanh_phan_da_co_vao_du_an.pdf', 1)
INSERT [dbo].[BaiDang] ([Ma_Bai], [Ma_Lop], [Ma_ND], [Thoi_Gian], [Noi_Dung], [Dinh_Kem], [Trang_Thai]) VALUES (N'kge-gbn-vdl-002', N'kge-gbn-vdl', N'U000006', CAST(N'2022-12-11T10:01:19.517' AS DateTime), N'Các lỗi thường gặp', N'kge-gbn-vdl-002-Loi thuong gap.pdf', 1)
INSERT [dbo].[BaiDang] ([Ma_Bai], [Ma_Lop], [Ma_ND], [Thoi_Gian], [Noi_Dung], [Dinh_Kem], [Trang_Thai]) VALUES (N'kge-gbn-vdl-003', N'kge-gbn-vdl', N'U000006', CAST(N'2022-12-11T10:02:00.537' AS DateTime), N'Phân công bài tập nhóm', N'kge-gbn-vdl-003-Phan cong_Danh gia_Ket qua BT nhom.docx', 1)
INSERT [dbo].[BaiDang] ([Ma_Bai], [Ma_Lop], [Ma_ND], [Thoi_Gian], [Noi_Dung], [Dinh_Kem], [Trang_Thai]) VALUES (N'kge-gbn-vdl-004', N'kge-gbn-vdl', N'U000006', CAST(N'2022-12-11T10:03:19.540' AS DateTime), N'Dữ liệu ôn tập', N'kge-gbn-vdl-004-QL_HoiDienVanNghe.bak,kge-gbn-vdl-004-QL_VuiCungHugo.bak,kge-gbn-vdl-004-Support.pdf', 0)
INSERT [dbo].[BaiDang] ([Ma_Bai], [Ma_Lop], [Ma_ND], [Thoi_Gian], [Noi_Dung], [Dinh_Kem], [Trang_Thai]) VALUES (N'kge-gbn-vdl-005', N'kge-gbn-vdl', N'U000006', CAST(N'2022-12-11T10:06:44.597' AS DateTime), N'Một số giao diện bài tập', N'kge-gbn-vdl-005-Picture1.png,kge-gbn-vdl-005-Picture2.png,kge-gbn-vdl-005-Picture3.png,kge-gbn-vdl-005-Picture4.png', 1)
INSERT [dbo].[BaiDang] ([Ma_Bai], [Ma_Lop], [Ma_ND], [Thoi_Gian], [Noi_Dung], [Dinh_Kem], [Trang_Thai]) VALUES (N'kge-gbn-vdl-006', N'kge-gbn-vdl', N'U000006', CAST(N'2022-12-11T10:08:59.943' AS DateTime), NULL, N'kge-gbn-vdl-006-TN207_Huong dan Chuong 5_Form Lop chuyen nganh_Cach 1.pdf,kge-gbn-vdl-006-TN207_Huong dan Chuong 5_Form Lop chuyen nganh_Cach 2.pdf,kge-gbn-vdl-006-TN207_Huong dan Chuong 5_Form Lop chuyen nganh_Cach 3.pdf', 1)
INSERT [dbo].[BaiDang] ([Ma_Bai], [Ma_Lop], [Ma_ND], [Thoi_Gian], [Noi_Dung], [Dinh_Kem], [Trang_Thai]) VALUES (N'tep-bje-sma-001', N'tep-bje-sma', N'U000002', CAST(N'2022-04-14T20:07:11.613' AS DateTime), N'Bài đăng mới', NULL, 1)
INSERT [dbo].[BaiDang] ([Ma_Bai], [Ma_Lop], [Ma_ND], [Thoi_Gian], [Noi_Dung], [Dinh_Kem], [Trang_Thai]) VALUES (N'tep-bje-sma-002', N'tep-bje-sma', N'U000001', CAST(N'2022-11-15T18:12:04.597' AS DateTime), N'dddddđ', NULL, 1)
INSERT [dbo].[BaiDang] ([Ma_Bai], [Ma_Lop], [Ma_ND], [Thoi_Gian], [Noi_Dung], [Dinh_Kem], [Trang_Thai]) VALUES (N'tep-bje-sma-003', N'tep-bje-sma', N'U000001', CAST(N'2022-11-15T18:12:25.583' AS DateTime), N'fffffffff', NULL, 1)
INSERT [dbo].[BaiDang] ([Ma_Bai], [Ma_Lop], [Ma_ND], [Thoi_Gian], [Noi_Dung], [Dinh_Kem], [Trang_Thai]) VALUES (N'tep-bje-sma-004', N'tep-bje-sma', N'U000002', CAST(N'2022-11-25T10:21:29.870' AS DateTime), N'bài đăng demo', NULL, 1)
GO
INSERT [dbo].[BinhLuan] ([Ma_Bai], [Ma_ND], [Thoi_Gian], [Noi_Dung], [Dinh_Kem]) VALUES (N'fpv-gnj-laj-002', N'U000006', CAST(N'2022-04-20T09:51:19.837' AS DateTime), N'ảnh đẹp', NULL)
INSERT [dbo].[BinhLuan] ([Ma_Bai], [Ma_ND], [Thoi_Gian], [Noi_Dung], [Dinh_Kem]) VALUES (N'tep-bje-sma-004', N'U000002', CAST(N'2022-11-25T10:21:39.910' AS DateTime), N'xccxc
', NULL)
GO
INSERT [dbo].[CamXuc] ([Ma_ND], [Ma_Bai], [Thoi_Gian]) VALUES (N'U000001', N'fpv-gnj-laj-002', CAST(N'2022-11-19T20:05:58.840' AS DateTime))
INSERT [dbo].[CamXuc] ([Ma_ND], [Ma_Bai], [Thoi_Gian]) VALUES (N'U000001', N'tep-bje-sma-001', CAST(N'2022-04-15T21:29:52.387' AS DateTime))
INSERT [dbo].[CamXuc] ([Ma_ND], [Ma_Bai], [Thoi_Gian]) VALUES (N'U000002', N'fpv-gnj-laj-001', CAST(N'2022-04-20T09:52:07.393' AS DateTime))
INSERT [dbo].[CamXuc] ([Ma_ND], [Ma_Bai], [Thoi_Gian]) VALUES (N'U000002', N'fpv-gnj-laj-002', CAST(N'2022-04-20T09:52:02.050' AS DateTime))
INSERT [dbo].[CamXuc] ([Ma_ND], [Ma_Bai], [Thoi_Gian]) VALUES (N'U000002', N'fpv-gnj-laj-003', CAST(N'2022-04-20T09:52:03.427' AS DateTime))
INSERT [dbo].[CamXuc] ([Ma_ND], [Ma_Bai], [Thoi_Gian]) VALUES (N'U000002', N'fpv-gnj-laj-004', CAST(N'2022-04-20T09:52:05.177' AS DateTime))
INSERT [dbo].[CamXuc] ([Ma_ND], [Ma_Bai], [Thoi_Gian]) VALUES (N'U000002', N'kge-gbn-vdl-004', CAST(N'2022-12-11T11:31:54.930' AS DateTime))
INSERT [dbo].[CamXuc] ([Ma_ND], [Ma_Bai], [Thoi_Gian]) VALUES (N'U000002', N'tep-bje-sma-004', CAST(N'2022-11-25T10:21:37.023' AS DateTime))
INSERT [dbo].[CamXuc] ([Ma_ND], [Ma_Bai], [Thoi_Gian]) VALUES (N'U000006', N'fpv-gnj-laj-001', CAST(N'2022-04-20T09:50:13.520' AS DateTime))
INSERT [dbo].[CamXuc] ([Ma_ND], [Ma_Bai], [Thoi_Gian]) VALUES (N'U000006', N'fpv-gnj-laj-002', CAST(N'2022-04-20T09:50:11.890' AS DateTime))
INSERT [dbo].[CamXuc] ([Ma_ND], [Ma_Bai], [Thoi_Gian]) VALUES (N'U000006', N'fpv-gnj-laj-003', CAST(N'2022-04-20T09:50:08.120' AS DateTime))
INSERT [dbo].[CamXuc] ([Ma_ND], [Ma_Bai], [Thoi_Gian]) VALUES (N'U000006', N'fpv-gnj-laj-004', CAST(N'2022-04-20T09:50:09.627' AS DateTime))
INSERT [dbo].[CamXuc] ([Ma_ND], [Ma_Bai], [Thoi_Gian]) VALUES (N'U000006', N'kge-gbn-vdl-003', CAST(N'2022-12-11T10:07:41.033' AS DateTime))
INSERT [dbo].[CamXuc] ([Ma_ND], [Ma_Bai], [Thoi_Gian]) VALUES (N'U000006', N'kge-gbn-vdl-004', CAST(N'2022-12-11T10:07:56.687' AS DateTime))
INSERT [dbo].[CamXuc] ([Ma_ND], [Ma_Bai], [Thoi_Gian]) VALUES (N'U000009', N'kge-gbn-vdl-004', CAST(N'2022-12-11T11:31:19.557' AS DateTime))
INSERT [dbo].[CamXuc] ([Ma_ND], [Ma_Bai], [Thoi_Gian]) VALUES (N'U000009', N'kge-gbn-vdl-005', CAST(N'2022-12-11T11:31:21.790' AS DateTime))
INSERT [dbo].[CamXuc] ([Ma_ND], [Ma_Bai], [Thoi_Gian]) VALUES (N'U000009', N'kge-gbn-vdl-006', CAST(N'2022-12-11T11:31:23.007' AS DateTime))
INSERT [dbo].[CamXuc] ([Ma_ND], [Ma_Bai], [Thoi_Gian]) VALUES (N'U000010', N'kge-gbn-vdl-002', CAST(N'2022-12-11T11:29:44.707' AS DateTime))
INSERT [dbo].[CamXuc] ([Ma_ND], [Ma_Bai], [Thoi_Gian]) VALUES (N'U000010', N'kge-gbn-vdl-004', CAST(N'2022-12-11T11:29:39.517' AS DateTime))
INSERT [dbo].[CamXuc] ([Ma_ND], [Ma_Bai], [Thoi_Gian]) VALUES (N'U000010', N'kge-gbn-vdl-005', CAST(N'2022-12-11T11:29:43.263' AS DateTime))
INSERT [dbo].[CamXuc] ([Ma_ND], [Ma_Bai], [Thoi_Gian]) VALUES (N'U000010', N'kge-gbn-vdl-006', CAST(N'2022-12-11T11:29:41.273' AS DateTime))
INSERT [dbo].[CamXuc] ([Ma_ND], [Ma_Bai], [Thoi_Gian]) VALUES (N'U000011', N'kge-gbn-vdl-003', CAST(N'2022-12-11T11:29:07.943' AS DateTime))
INSERT [dbo].[CamXuc] ([Ma_ND], [Ma_Bai], [Thoi_Gian]) VALUES (N'U000011', N'kge-gbn-vdl-004', CAST(N'2022-12-11T11:29:02.353' AS DateTime))
INSERT [dbo].[CamXuc] ([Ma_ND], [Ma_Bai], [Thoi_Gian]) VALUES (N'U000011', N'kge-gbn-vdl-005', CAST(N'2022-12-11T11:29:06.703' AS DateTime))
INSERT [dbo].[CamXuc] ([Ma_ND], [Ma_Bai], [Thoi_Gian]) VALUES (N'U000011', N'kge-gbn-vdl-006', CAST(N'2022-12-11T11:29:04.620' AS DateTime))
GO
INSERT [dbo].[PhongThi] ([Ma_Phong], [Ma_Lop], [Ten_Phong], [Ngay_Tao], [Mat_Khau], [Ngay_Mo], [Ngay_Dong], [Luot_Thi], [Xem_Lai], [Thoi_Luong]) VALUES (N'fpv-gnj-laj-001', N'fpv-gnj-laj', N'Bài thi thử', CAST(N'2022-04-20T09:31:05.913' AS DateTime), N'abc123', CAST(N'2022-04-20T09:30:00.000' AS DateTime), CAST(N'2022-04-27T09:30:00.000' AS DateTime), 3, 1, 15)
INSERT [dbo].[PhongThi] ([Ma_Phong], [Ma_Lop], [Ten_Phong], [Ngay_Tao], [Mat_Khau], [Ngay_Mo], [Ngay_Dong], [Luot_Thi], [Xem_Lai], [Thoi_Luong]) VALUES (N'fpv-gnj-laj-002', N'fpv-gnj-laj', N'Bài thi demo', CAST(N'2022-11-24T12:08:16.297' AS DateTime), NULL, CAST(N'2022-11-24T12:07:00.000' AS DateTime), CAST(N'2022-11-25T12:07:00.000' AS DateTime), 10, 1, 40)
INSERT [dbo].[PhongThi] ([Ma_Phong], [Ma_Lop], [Ten_Phong], [Ngay_Tao], [Mat_Khau], [Ngay_Mo], [Ngay_Dong], [Luot_Thi], [Xem_Lai], [Thoi_Luong]) VALUES (N'fpv-gnj-laj-003', N'fpv-gnj-laj', N'Demo', CAST(N'2022-11-25T10:28:06.490' AS DateTime), NULL, CAST(N'2022-11-25T10:27:00.000' AS DateTime), CAST(N'2022-11-26T10:27:00.000' AS DateTime), 3, 0, 23)
INSERT [dbo].[PhongThi] ([Ma_Phong], [Ma_Lop], [Ten_Phong], [Ngay_Tao], [Mat_Khau], [Ngay_Mo], [Ngay_Dong], [Luot_Thi], [Xem_Lai], [Thoi_Luong]) VALUES (N'kge-gbn-vdl-001', N'kge-gbn-vdl', N'Kiểm tra chất lượng', CAST(N'2022-12-11T10:13:27.907' AS DateTime), N'abc123', CAST(N'2022-12-11T10:13:00.000' AS DateTime), CAST(N'2022-12-15T10:13:00.000' AS DateTime), 10, 1, 15)
INSERT [dbo].[PhongThi] ([Ma_Phong], [Ma_Lop], [Ten_Phong], [Ngay_Tao], [Mat_Khau], [Ngay_Mo], [Ngay_Dong], [Luot_Thi], [Xem_Lai], [Thoi_Luong]) VALUES (N'tep-bje-sma-001', N'tep-bje-sma', N'Bài thi thử', CAST(N'2022-03-27T18:39:34.330' AS DateTime), N'abc123', CAST(N'2022-03-27T18:39:00.000' AS DateTime), CAST(N'2022-04-27T18:39:00.000' AS DateTime), 10, 1, 45)
INSERT [dbo].[PhongThi] ([Ma_Phong], [Ma_Lop], [Ten_Phong], [Ngay_Tao], [Mat_Khau], [Ngay_Mo], [Ngay_Dong], [Luot_Thi], [Xem_Lai], [Thoi_Luong]) VALUES (N'tep-bje-sma-002', N'tep-bje-sma', N'Bài thi cuối kỳ', CAST(N'2022-03-29T17:33:18.953' AS DateTime), N'123', CAST(N'2022-03-29T17:32:00.000' AS DateTime), CAST(N'2022-11-25T17:32:00.000' AS DateTime), 20, 1, 90)
INSERT [dbo].[PhongThi] ([Ma_Phong], [Ma_Lop], [Ten_Phong], [Ngay_Tao], [Mat_Khau], [Ngay_Mo], [Ngay_Dong], [Luot_Thi], [Xem_Lai], [Thoi_Luong]) VALUES (N'tep-bje-sma-003', N'tep-bje-sma', N'Bài thi test', CAST(N'2022-11-18T12:32:14.650' AS DateTime), NULL, CAST(N'2022-11-17T12:54:00.000' AS DateTime), CAST(N'2022-11-18T12:54:00.000' AS DateTime), 12, 1, 12)
GO
INSERT [dbo].[CauHoiThi] ([STT], [Ma_Phong], [Cau_Hoi], [Loi_Giai], [Dap_An]) VALUES (1, N'fpv-gnj-laj-001', N'Lớp học tên gì?', N'Lập trình web nâng cao', N'Lập trình web\Lập trình web căn bản\Lập trình web nâng cao\Lập trình web cơ sở')
INSERT [dbo].[CauHoiThi] ([STT], [Ma_Phong], [Cau_Hoi], [Loi_Giai], [Dap_An]) VALUES (1, N'fpv-gnj-laj-002', N'Công nghệ sử dụng cho luận văn là gì?', N'.Net MVC\.Net Core', N'.Net MVC\PHP\.Net Core\Python')
INSERT [dbo].[CauHoiThi] ([STT], [Ma_Phong], [Cau_Hoi], [Loi_Giai], [Dap_An]) VALUES (1, N'fpv-gnj-laj-003', N'demo 1', N'a\v', N'a\v\d\f')
INSERT [dbo].[CauHoiThi] ([STT], [Ma_Phong], [Cau_Hoi], [Loi_Giai], [Dap_An]) VALUES (1, N'kge-gbn-vdl-001', N'Trang Web Asp.net có thể được sọan thảo trên phần mềm nào:', N'MS Visual Studio', N'MS Word\Macromedia Dreamweaver\MS Visual Studio\Cả B và C đều đúng')
INSERT [dbo].[CauHoiThi] ([STT], [Ma_Phong], [Cau_Hoi], [Loi_Giai], [Dap_An]) VALUES (1, N'tep-bje-sma-001', N'abc ssssss', N'dd', N'dd\đ\s\dddđ')
INSERT [dbo].[CauHoiThi] ([STT], [Ma_Phong], [Cau_Hoi], [Loi_Giai], [Dap_An]) VALUES (1, N'tep-bje-sma-002', N'Lớp học tên gì?', N'Lớp học đầu tiên', N'Không có tên\Lớp A\Lớp học đầu tiên\Tất cả điều sai')
INSERT [dbo].[CauHoiThi] ([STT], [Ma_Phong], [Cau_Hoi], [Loi_Giai], [Dap_An]) VALUES (1, N'tep-bje-sma-003', N'lớp học tên gì', N'tên c', N'tên a\tên b\tên c\tên d')
INSERT [dbo].[CauHoiThi] ([STT], [Ma_Phong], [Cau_Hoi], [Loi_Giai], [Dap_An]) VALUES (2, N'fpv-gnj-laj-001', N'Giáo viên tên gì?', N'Hồ Ánh Nguyệt', N'Hồ Ánh Nguyệt\Ánh Nguyệt\Thu Hà\Linh Tuyết')
INSERT [dbo].[CauHoiThi] ([STT], [Ma_Phong], [Cau_Hoi], [Loi_Giai], [Dap_An]) VALUES (2, N'kge-gbn-vdl-001', N'ASP viết tắt bởi:', N'Active Server Pages', N'Active Server Pages\Association of Software Professionals\ActiveX Server Page\Kết quả khác')
INSERT [dbo].[CauHoiThi] ([STT], [Ma_Phong], [Cau_Hoi], [Loi_Giai], [Dap_An]) VALUES (2, N'tep-bje-sma-001', N'2', N'2', N'1\2\3\4')
INSERT [dbo].[CauHoiThi] ([STT], [Ma_Phong], [Cau_Hoi], [Loi_Giai], [Dap_An]) VALUES (2, N'tep-bje-sma-002', N'Giáo viên là ai?', N'Triệu Nguyễn Khánh Băng\Khánh Băng', N'Không có giáo viên\Triệu Nguyễn Khánh Băng\a và c đúng\Khánh Băng')
INSERT [dbo].[CauHoiThi] ([STT], [Ma_Phong], [Cau_Hoi], [Loi_Giai], [Dap_An]) VALUES (2, N'tep-bje-sma-003', N'222222', N'22222222', N'22222222\333333333\4444444444\55555555')
INSERT [dbo].[CauHoiThi] ([STT], [Ma_Phong], [Cau_Hoi], [Loi_Giai], [Dap_An]) VALUES (3, N'fpv-gnj-laj-001', N'Chủ đề lớp là gì?', N'Lập trình web\Lập trình .Net', N'Lập trình web\Lập trình Java\Lập trình .Net\Thiết kết web')
INSERT [dbo].[CauHoiThi] ([STT], [Ma_Phong], [Cau_Hoi], [Loi_Giai], [Dap_An]) VALUES (3, N'kge-gbn-vdl-001', N'Tập tin code behide của trang ASP.Net có phần mở rộng tùy theo ngôn ngữ kịch bản phía trình chủ cụ thể nếu sử dụng ngôn ngữ lập trình C# thì sẽ có phần mở rộng là:', N'cs', N'.C#\.ascx\cs\Tất cả đều đúng')
INSERT [dbo].[CauHoiThi] ([STT], [Ma_Phong], [Cau_Hoi], [Loi_Giai], [Dap_An]) VALUES (3, N'tep-bje-sma-002', N'Chủ đề lớp là gì?', N'Lập trình Web\Lập trình .Net', N'Lập trình Web\Lập trình .Net\Lập trình Android\Lập trình Java')
INSERT [dbo].[CauHoiThi] ([STT], [Ma_Phong], [Cau_Hoi], [Loi_Giai], [Dap_An]) VALUES (4, N'fpv-gnj-laj-001', N'Ứng dụng tên gì?', N'Dạy học trực tuyến', N'Dạy học trực tuyến\Quản lý dạy học\A & B điều đúng\A & B điều sai')
INSERT [dbo].[CauHoiThi] ([STT], [Ma_Phong], [Cau_Hoi], [Loi_Giai], [Dap_An]) VALUES (4, N'kge-gbn-vdl-001', N'Phần mềm Webserver IIS viết tắt bởi:', N'Internet Information Services', N'Internet Information Services\International Information Services\Information Internet Services\Kết quả khác')
INSERT [dbo].[CauHoiThi] ([STT], [Ma_Phong], [Cau_Hoi], [Loi_Giai], [Dap_An]) VALUES (4, N'tep-bje-sma-002', N'Lớp có mấy thành viên?', N'2', N'1\2\10\5')
INSERT [dbo].[CauHoiThi] ([STT], [Ma_Phong], [Cau_Hoi], [Loi_Giai], [Dap_An]) VALUES (5, N'fpv-gnj-laj-001', N'Ngày tạo lớp học', N'20/04/2022', N'19/04/2022\20/04/2022\21/04/2022\22/04/2022')
INSERT [dbo].[CauHoiThi] ([STT], [Ma_Phong], [Cau_Hoi], [Loi_Giai], [Dap_An]) VALUES (5, N'kge-gbn-vdl-001', N'NET Framework. Cung cấp một môi trường runtime được gọi là ?', N'CLR\Common Language Runtime', N'RMT\CLR\RCT\Common Language Runtime')
INSERT [dbo].[CauHoiThi] ([STT], [Ma_Phong], [Cau_Hoi], [Loi_Giai], [Dap_An]) VALUES (6, N'kge-gbn-vdl-001', N'Câu nào sau đây là SAI?', N'Các ứng dụng ASP NET có thể chạy không cần máy chủ Web\Tất cả đều đúng', N'Các ứng dụng ASP NET có thể chạy không cần máy chủ Web\ASP. NET là nâng cấp của ASP\ASP.Net là ngôn ngữ lập trình web\Tất cả đều đúng')
INSERT [dbo].[CauHoiThi] ([STT], [Ma_Phong], [Cau_Hoi], [Loi_Giai], [Dap_An]) VALUES (7, N'kge-gbn-vdl-001', N'ASP.Net được Microsoft giới thiệu vào năm nào?', N'2002', N'1996\1998\2000\2002')
INSERT [dbo].[CauHoiThi] ([STT], [Ma_Phong], [Cau_Hoi], [Loi_Giai], [Dap_An]) VALUES (8, N'kge-gbn-vdl-001', N'ASP.Net', N'Ngôn ngữ lập trình', N'Ngôn ngữ lập trình\Kỹ thuật lập trình phía server\Kỹ thuật lập trình phía client\Ngôn ngữ lập trình cơ sở dữ liệu tương tự như SQL')
INSERT [dbo].[CauHoiThi] ([STT], [Ma_Phong], [Cau_Hoi], [Loi_Giai], [Dap_An]) VALUES (9, N'kge-gbn-vdl-001', N'Để chạy chương trình (ứng dụng asp.net) trong môi trường MS Visual 2005 có debug ta thực hiện', N'Tất cả đều đúng', N'Nhấn phím F5\Click biểu tượng Start debugging\Chọn menu Buil / Start debugging\Tất cả đều đúng')
INSERT [dbo].[CauHoiThi] ([STT], [Ma_Phong], [Cau_Hoi], [Loi_Giai], [Dap_An]) VALUES (10, N'kge-gbn-vdl-001', N'Các Control kiểm tra dữ liệu nào có trong ASP.NET:', N'CompareValidator\RangeValidator', N'CompareValidator\RangeValidator\CustomValidator\Tất cả đều sai')
GO
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (1, N'fpv-gnj-laj-001', N'U000002', 1, N'Lập trình web nâng cao')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (1, N'fpv-gnj-laj-001', N'U000002', 2, N'Lập trình web nâng cao')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (1, N'tep-bje-sma-002', N'U000002', 1, N'Lớp học đầu tiên')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (1, N'tep-bje-sma-002', N'U000002', 4, N'Không có tên')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (1, N'tep-bje-sma-002', N'U000002', 5, N'Không có tên')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (1, N'tep-bje-sma-002', N'U000002', 6, N'Lớp học đầu tiên')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (1, N'tep-bje-sma-002', N'U000002', 11, N'Lớp học đầu tiên')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (1, N'tep-bje-sma-002', N'U000002', 12, N'Không có tên')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (2, N'fpv-gnj-laj-001', N'U000002', 1, N'Hồ Ánh Nguyệt')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (2, N'fpv-gnj-laj-001', N'U000002', 2, N'Hồ Ánh Nguyệt')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (2, N'tep-bje-sma-002', N'U000002', 1, N'Triệu Nguyễn Khánh Băng\Khánh Băng')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (2, N'tep-bje-sma-002', N'U000002', 4, N'Không có giáo viên\Triệu Nguyễn Khánh Băng\a và c đúng')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (2, N'tep-bje-sma-002', N'U000002', 5, N'Khánh Băng')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (2, N'tep-bje-sma-002', N'U000002', 6, N'Triệu Nguyễn Khánh Băng')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (2, N'tep-bje-sma-002', N'U000002', 7, N'Không có giáo viên')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (2, N'tep-bje-sma-002', N'U000002', 8, N'Khánh Băng')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (2, N'tep-bje-sma-002', N'U000002', 11, N'Khánh Băng')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (2, N'tep-bje-sma-002', N'U000002', 12, N'Không có giáo viên\Triệu Nguyễn Khánh Băng')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (3, N'fpv-gnj-laj-001', N'U000002', 1, N'Lập trình .Net')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (3, N'tep-bje-sma-002', N'U000002', 1, N'Lập trình Web\Lập trình .Net')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (3, N'tep-bje-sma-002', N'U000002', 4, N'Lập trình Web\Lập trình Java')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (3, N'tep-bje-sma-002', N'U000002', 5, N'Lập trình Web\Lập trình .Net')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (3, N'tep-bje-sma-002', N'U000002', 6, N'Lập trình Java')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (3, N'tep-bje-sma-002', N'U000002', 8, N'Lập trình Web')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (3, N'tep-bje-sma-002', N'U000002', 11, N'Lập trình Web')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (3, N'tep-bje-sma-002', N'U000002', 12, N'Lập trình Web\Lập trình Android')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (4, N'fpv-gnj-laj-001', N'U000002', 1, N'A & B điều đúng')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (4, N'tep-bje-sma-002', N'U000002', 4, N'2')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (4, N'tep-bje-sma-002', N'U000002', 5, N'2')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (4, N'tep-bje-sma-002', N'U000002', 6, N'2')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (4, N'tep-bje-sma-002', N'U000002', 8, N'2')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (4, N'tep-bje-sma-002', N'U000002', 11, N'1')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (4, N'tep-bje-sma-002', N'U000002', 12, N'1')
INSERT [dbo].[CauTraLoi] ([STT], [Ma_Phong], [Ma_ND], [Lan_Thu], [Dap_An]) VALUES (5, N'fpv-gnj-laj-001', N'U000002', 1, N'20/04/2022')
GO
INSERT [dbo].[ThoiGianLamBai] ([Ma_ND], [Ma_Phong], [Lan_Thu], [Bat_Dau], [Ket_Thuc]) VALUES (N'U000002', N'fpv-gnj-laj-001', 1, CAST(N'2022-04-20T09:38:28.790' AS DateTime), CAST(N'2022-04-20T09:38:47.463' AS DateTime))
INSERT [dbo].[ThoiGianLamBai] ([Ma_ND], [Ma_Phong], [Lan_Thu], [Bat_Dau], [Ket_Thuc]) VALUES (N'U000002', N'fpv-gnj-laj-001', 2, CAST(N'2022-04-20T11:48:16.737' AS DateTime), CAST(N'2022-04-20T11:54:50.360' AS DateTime))
INSERT [dbo].[ThoiGianLamBai] ([Ma_ND], [Ma_Phong], [Lan_Thu], [Bat_Dau], [Ket_Thuc]) VALUES (N'U000002', N'tep-bje-sma-002', 1, CAST(N'2022-04-05T23:54:15.693' AS DateTime), NULL)
INSERT [dbo].[ThoiGianLamBai] ([Ma_ND], [Ma_Phong], [Lan_Thu], [Bat_Dau], [Ket_Thuc]) VALUES (N'U000002', N'tep-bje-sma-002', 2, CAST(N'2022-04-06T23:14:14.027' AS DateTime), NULL)
INSERT [dbo].[ThoiGianLamBai] ([Ma_ND], [Ma_Phong], [Lan_Thu], [Bat_Dau], [Ket_Thuc]) VALUES (N'U000002', N'tep-bje-sma-002', 3, CAST(N'2022-04-09T23:31:18.700' AS DateTime), CAST(N'2022-04-09T23:32:42.000' AS DateTime))
INSERT [dbo].[ThoiGianLamBai] ([Ma_ND], [Ma_Phong], [Lan_Thu], [Bat_Dau], [Ket_Thuc]) VALUES (N'U000002', N'tep-bje-sma-002', 4, CAST(N'2022-04-13T11:45:35.087' AS DateTime), CAST(N'2022-04-13T11:46:07.840' AS DateTime))
INSERT [dbo].[ThoiGianLamBai] ([Ma_ND], [Ma_Phong], [Lan_Thu], [Bat_Dau], [Ket_Thuc]) VALUES (N'U000002', N'tep-bje-sma-002', 5, CAST(N'2022-11-18T12:15:18.293' AS DateTime), CAST(N'2022-11-18T12:15:54.960' AS DateTime))
INSERT [dbo].[ThoiGianLamBai] ([Ma_ND], [Ma_Phong], [Lan_Thu], [Bat_Dau], [Ket_Thuc]) VALUES (N'U000002', N'tep-bje-sma-002', 6, CAST(N'2022-11-18T13:12:23.200' AS DateTime), CAST(N'2022-11-18T13:26:54.617' AS DateTime))
INSERT [dbo].[ThoiGianLamBai] ([Ma_ND], [Ma_Phong], [Lan_Thu], [Bat_Dau], [Ket_Thuc]) VALUES (N'U000002', N'tep-bje-sma-002', 7, CAST(N'2022-11-18T13:34:09.513' AS DateTime), NULL)
INSERT [dbo].[ThoiGianLamBai] ([Ma_ND], [Ma_Phong], [Lan_Thu], [Bat_Dau], [Ket_Thuc]) VALUES (N'U000002', N'tep-bje-sma-002', 8, CAST(N'2022-11-18T23:40:53.733' AS DateTime), CAST(N'2022-11-18T23:41:08.903' AS DateTime))
INSERT [dbo].[ThoiGianLamBai] ([Ma_ND], [Ma_Phong], [Lan_Thu], [Bat_Dau], [Ket_Thuc]) VALUES (N'U000002', N'tep-bje-sma-002', 9, CAST(N'2022-11-18T23:41:30.820' AS DateTime), CAST(N'2022-11-18T23:41:38.233' AS DateTime))
INSERT [dbo].[ThoiGianLamBai] ([Ma_ND], [Ma_Phong], [Lan_Thu], [Bat_Dau], [Ket_Thuc]) VALUES (N'U000002', N'tep-bje-sma-002', 10, CAST(N'2022-11-24T11:21:49.520' AS DateTime), CAST(N'2022-11-24T11:21:56.017' AS DateTime))
INSERT [dbo].[ThoiGianLamBai] ([Ma_ND], [Ma_Phong], [Lan_Thu], [Bat_Dau], [Ket_Thuc]) VALUES (N'U000002', N'tep-bje-sma-002', 11, CAST(N'2022-11-24T11:26:51.507' AS DateTime), CAST(N'2022-11-24T11:35:03.207' AS DateTime))
INSERT [dbo].[ThoiGianLamBai] ([Ma_ND], [Ma_Phong], [Lan_Thu], [Bat_Dau], [Ket_Thuc]) VALUES (N'U000002', N'tep-bje-sma-002', 12, CAST(N'2022-11-25T10:22:38.803' AS DateTime), CAST(N'2022-11-25T10:24:01.750' AS DateTime))
GO
INSERT [dbo].[Tag] ([Ma_Tag], [Ten_Tag]) VALUES (N'CT001', N'Lập Trình Web')
INSERT [dbo].[Tag] ([Ma_Tag], [Ten_Tag]) VALUES (N'CT002', N'Lập Trình .Net')
INSERT [dbo].[Tag] ([Ma_Tag], [Ten_Tag]) VALUES (N'CT003', N'Lập Trình Java')
INSERT [dbo].[Tag] ([Ma_Tag], [Ten_Tag]) VALUES (N'CT004', N'Tìm Hiểu C và C++')
INSERT [dbo].[Tag] ([Ma_Tag], [Ten_Tag]) VALUES (N'CT005', N'Trí Tuệ Nhân Tạo')
INSERT [dbo].[Tag] ([Ma_Tag], [Ten_Tag]) VALUES (N'CT006', N'Python')
INSERT [dbo].[Tag] ([Ma_Tag], [Ten_Tag]) VALUES (N'CT007', N'PHP')
INSERT [dbo].[Tag] ([Ma_Tag], [Ten_Tag]) VALUES (N'CT008', N'Lý thuyết đồ thị')
INSERT [dbo].[Tag] ([Ma_Tag], [Ten_Tag]) VALUES (N'TH001', N'Toán')
INSERT [dbo].[Tag] ([Ma_Tag], [Ten_Tag]) VALUES (N'TH002', N'Ngữ Văn')
INSERT [dbo].[Tag] ([Ma_Tag], [Ten_Tag]) VALUES (N'TH003', N'Tiếng Anh')
INSERT [dbo].[Tag] ([Ma_Tag], [Ten_Tag]) VALUES (N'TH004', N'Vật Lý')
INSERT [dbo].[Tag] ([Ma_Tag], [Ten_Tag]) VALUES (N'TH005', N'Hóa Học')
INSERT [dbo].[Tag] ([Ma_Tag], [Ten_Tag]) VALUES (N'TH006', N'Sinh Học')
GO
SET IDENTITY_INSERT [dbo].[LopThuocTag] ON 

INSERT [dbo].[LopThuocTag] ([Id], [Ma_Tag], [Ma_Lop]) VALUES (1, N'CT001', N'ajg-atv-ims')
INSERT [dbo].[LopThuocTag] ([Id], [Ma_Tag], [Ma_Lop]) VALUES (3, N'CT001', N'mfo-chj-hta')
INSERT [dbo].[LopThuocTag] ([Id], [Ma_Tag], [Ma_Lop]) VALUES (5, N'CT002', N'ajg-atv-ims')
INSERT [dbo].[LopThuocTag] ([Id], [Ma_Tag], [Ma_Lop]) VALUES (8, N'CT003', N'pqr-uvl-hmu')
INSERT [dbo].[LopThuocTag] ([Id], [Ma_Tag], [Ma_Lop]) VALUES (11, N'CT001', N'tep-bje-sma')
INSERT [dbo].[LopThuocTag] ([Id], [Ma_Tag], [Ma_Lop]) VALUES (12, N'CT002', N'tep-bje-sma')
INSERT [dbo].[LopThuocTag] ([Id], [Ma_Tag], [Ma_Lop]) VALUES (13, N'CT001', N'fpv-gnj-laj')
INSERT [dbo].[LopThuocTag] ([Id], [Ma_Tag], [Ma_Lop]) VALUES (14, N'CT002', N'fpv-gnj-laj')
INSERT [dbo].[LopThuocTag] ([Id], [Ma_Tag], [Ma_Lop]) VALUES (15, N'CT002', N'kge-gbn-vdl')
INSERT [dbo].[LopThuocTag] ([Id], [Ma_Tag], [Ma_Lop]) VALUES (19, N'TH001', N'hru-bpv-nul')
INSERT [dbo].[LopThuocTag] ([Id], [Ma_Tag], [Ma_Lop]) VALUES (20, N'TH001', N'tfs-cle-cjf')
INSERT [dbo].[LopThuocTag] ([Id], [Ma_Tag], [Ma_Lop]) VALUES (21, N'TH004', N'tfs-cle-cjf')
INSERT [dbo].[LopThuocTag] ([Id], [Ma_Tag], [Ma_Lop]) VALUES (22, N'TH005', N'kfl-qpg-jgq')
INSERT [dbo].[LopThuocTag] ([Id], [Ma_Tag], [Ma_Lop]) VALUES (23, N'TH006', N'kfl-qpg-jgq')
INSERT [dbo].[LopThuocTag] ([Id], [Ma_Tag], [Ma_Lop]) VALUES (24, N'TH002', N'tig-ptk-sok')
INSERT [dbo].[LopThuocTag] ([Id], [Ma_Tag], [Ma_Lop]) VALUES (25, N'TH003', N'ifd-tcm-anp')
INSERT [dbo].[LopThuocTag] ([Id], [Ma_Tag], [Ma_Lop]) VALUES (26, N'CT005', N'atk-spu-upa')
INSERT [dbo].[LopThuocTag] ([Id], [Ma_Tag], [Ma_Lop]) VALUES (27, N'CT004', N'tsm-gat-pas')
INSERT [dbo].[LopThuocTag] ([Id], [Ma_Tag], [Ma_Lop]) VALUES (28, N'CT004', N'rcm-lqv-mhi')
INSERT [dbo].[LopThuocTag] ([Id], [Ma_Tag], [Ma_Lop]) VALUES (29, N'CT003', N'rcm-lqv-mhi')
INSERT [dbo].[LopThuocTag] ([Id], [Ma_Tag], [Ma_Lop]) VALUES (30, N'CT008', N'unu-lqn-hph')
INSERT [dbo].[LopThuocTag] ([Id], [Ma_Tag], [Ma_Lop]) VALUES (31, N'CT006', N'ets-mcd-epp')
INSERT [dbo].[LopThuocTag] ([Id], [Ma_Tag], [Ma_Lop]) VALUES (32, N'CT005', N'ets-mcd-epp')
INSERT [dbo].[LopThuocTag] ([Id], [Ma_Tag], [Ma_Lop]) VALUES (33, N'TH003', N'hjh-dea-iuu')
INSERT [dbo].[LopThuocTag] ([Id], [Ma_Tag], [Ma_Lop]) VALUES (34, N'CT007', N'jki-mfu-mvc')
INSERT [dbo].[LopThuocTag] ([Id], [Ma_Tag], [Ma_Lop]) VALUES (35, N'CT001', N'jki-mfu-mvc')
INSERT [dbo].[LopThuocTag] ([Id], [Ma_Tag], [Ma_Lop]) VALUES (36, N'CT001', N'svp-aku-qgd')
INSERT [dbo].[LopThuocTag] ([Id], [Ma_Tag], [Ma_Lop]) VALUES (37, N'CT002', N'svp-aku-qgd')
INSERT [dbo].[LopThuocTag] ([Id], [Ma_Tag], [Ma_Lop]) VALUES (38, N'CT006', N'eep-phg-aop')
INSERT [dbo].[LopThuocTag] ([Id], [Ma_Tag], [Ma_Lop]) VALUES (39, N'CT005', N'eep-phg-aop')
INSERT [dbo].[LopThuocTag] ([Id], [Ma_Tag], [Ma_Lop]) VALUES (40, N'CT006', N'nqa-sot-ofo')
INSERT [dbo].[LopThuocTag] ([Id], [Ma_Tag], [Ma_Lop]) VALUES (41, N'CT001', N'njt-grc-eis')
INSERT [dbo].[LopThuocTag] ([Id], [Ma_Tag], [Ma_Lop]) VALUES (42, N'CT007', N'njt-grc-eis')
SET IDENTITY_INSERT [dbo].[LopThuocTag] OFF
GO
INSERT [dbo].[Ghim] ([Ma_Bai], [Thoi_Gian]) VALUES (N'fpv-gnj-laj-002', CAST(N'2022-04-20T09:50:56.757' AS DateTime))
INSERT [dbo].[Ghim] ([Ma_Bai], [Thoi_Gian]) VALUES (N'fpv-gnj-laj-003', CAST(N'2022-04-20T09:48:47.153' AS DateTime))
INSERT [dbo].[Ghim] ([Ma_Bai], [Thoi_Gian]) VALUES (N'kge-gbn-vdl-004', CAST(N'2022-12-11T10:06:59.450' AS DateTime))
GO