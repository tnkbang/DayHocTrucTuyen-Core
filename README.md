# Dạy Học Trực Tuyến

Website quản lý dạy và học trực tuyến. Có tích hợp thanh toán online qua MoMo và sử dụng Google API để đăng nhập.

## Kỹ Thuật

- ASP.Net Core 6 MVC
- Bootstrap 5
- SignalR
- Google Cloud API
- MoMo API

## Cài đặt

```
git clone https://github.com/tnkbang/DayHocTrucTuyen-Core.git
```

Lấy file script sql tại:
```
./wwwroot/Content/SQL/script.sql
```

Cấu hình kết nối csdl tại:
```
./appsettings.json

Change: "Data Source=KHANHBANG\\MSSQL;Initial Catalog=DayHocTrucTuyen;Persist Security Info=True;User ID=tnkb;Password=khanhbang"
```

Cấu hình xác thực email:
```
./appsettings.json

Thay đổi địa chỉ email và chữ ký ứng dụng
```

Cần đổi cấu hình API login Google tại:
```
./wwwroot/Template/js/getAPI.js
```

Thay đổi xác thực MoMo tại:
```
./Areas/Payment/MoMoController.cs
```

## Demo

[![Watch the video](/DayHocTrucTuyen/wwwroot/Content/Img/Resources/about.jpg)](/DayHocTrucTuyen/wwwroot/demo/demo.mp4)