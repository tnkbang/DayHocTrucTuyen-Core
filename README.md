﻿# Dạy Học Trực Tuyến

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

Cấu hình gửi xác thực email:
```
./appsettings.json

Thay đổi địa chỉ email và chữ ký ứng dụng
```

Có thể thay đổi cấu hình API login Google tại:
```
./wwwroot/Template/js/getAPI.js
```

Có thể thay đổi xác thực MoMo tại:
```
./Areas/Payment/MoMoController.cs
```

## Demo

[![Watch the video](https://seobyweb.com/wp-content/uploads/2017/10/y-nghia-cua-demo.jpg)](https://dl.dropbox.com/s/fg6j010s4f89vhl/demo_dayhoctructuyen.mp4?dl=0)
</br>
[Link Dropbox](https://dl.dropbox.com/s/fg6j010s4f89vhl/demo_dayhoctructuyen.mp4?dl=0)
</br>
hoặc
</br>
[Link OneDrive](https://winofiwin292-my.sharepoint.com/:v:/g/personal/tnkb_winofiwin292_onmicrosoft_com/Eb3HPJaGnbNNqvfOT-K4KVkBMRQQOALSfJpKHOQU35FnOQ?e=urjtJo)
