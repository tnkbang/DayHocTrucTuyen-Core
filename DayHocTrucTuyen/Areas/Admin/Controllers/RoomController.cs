using DayHocTrucTuyen.Areas.Admin.Models;
using DayHocTrucTuyen.Areas.User.Controllers;
using DayHocTrucTuyen.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DayHocTrucTuyen.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Route("Admin/[controller]/[action]")]
    [Authorize(Roles = "01")]
    public class RoomController : Controller
    {
        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();

        #region Quản lý lớp học
        //Trang danh sách lớp học
        public IActionResult List()
        {
            //ViewBag thể hiện trang đang được hiển thị trên layout
            ViewBag.RoomList = "active";

            return View();
        }

        //Lấy danh sách lớp học
        [HttpGet]
        public IActionResult getList(string? search, string? sort, string? order, int? offset, int? limit)
        {
            var lst = db.LopHocs.Where(x => x.MaLop != null);

            //Nếu tìm kiếm không rỗng thì xử lý tìm kiếm mã, tên, trạng thái, giá tiền,....
            if (!string.IsNullOrEmpty(search))
            {
                var nd = db.NguoiDungs.FirstOrDefault(s => string.Concat(s.HoLot, " ", s.Ten).Contains(search));

                if(nd != null)
                {
                    lst = lst.Where(s => s.MaNd == nd.MaNd);
                }
                else
                {
                    lst = lst.Where(s => s.TenLop.Contains(search)
                                || s.MaLop.Contains(search)
                                || s.GiaTien.ToString().Contains(search)
                                || s.BiDanh.Contains(search));
                }
            }

            //Xử lý sắp xếp
            if (!string.IsNullOrEmpty(sort) && !string.IsNullOrEmpty(order))
            {
                switch (sort)
                {
                    case "maLop":
                        if (order.Equals("asc"))
                        {
                            lst = lst.OrderBy(x => x.MaLop);
                        }
                        else
                        {
                            lst = lst.OrderByDescending(x => x.MaLop);
                        }
                        break;
                    case "tenLop":
                        if (order.Equals("asc"))
                        {
                            lst = lst.OrderBy(x => x.TenLop);
                        }
                        else
                        {
                            lst = lst.OrderByDescending(x => x.TenLop);
                        }
                        break;
                    case "tenOwner":
                        if (order.Equals("asc"))
                        {
                            lst = lst.OrderBy(x => x.MaNd);
                        }
                        else
                        {
                            lst = lst.OrderByDescending(x => x.MaNd);
                        }
                        break;
                    case "ngayTao":
                        if (order.Equals("asc"))
                        {
                            lst = lst.OrderBy(x => x.NgayTao);
                        }
                        else
                        {
                            lst = lst.OrderByDescending(x => x.NgayTao);
                        }
                        break;
                    case "giaTien":
                        if (order.Equals("asc"))
                        {
                            lst = lst.OrderBy(x => x.GiaTien);
                        }
                        else
                        {
                            lst = lst.OrderByDescending(x => x.GiaTien);
                        }
                        break;
                    case "trangThai":
                        if (order.Equals("asc"))
                        {
                            lst = lst.OrderBy(x => x.TrangThai);
                        }
                        else
                        {
                            lst = lst.OrderByDescending(x => x.TrangThai);
                        }
                        break;
                }
            }

            List<dynamic> lstResult = new List<dynamic>();
            foreach (var item in lst.ToList())
            {
                var temp = new
                {
                    maLop = item.MaLop,
                    tenLop = item.TenLop,
                    maOwner = item.MaNd,
                    tenOwner = item.getOwner().getFullName(),
                    imgBg = item.getImage(),
                    ngayTao = item.NgayTao.ToString("g"),
                    biDanh = item.BiDanh == item.MaLop ? null : item.BiDanh,
                    giaTien = item.GiaTien.ToString("n0"),
                    moTa = item.MoTa,
                    trangThai = item.TrangThai ? "Hoạt động" : "Bị khóa",
                    ttBool = item.TrangThai,
                    thaoTac = customThaoTac(item.MaLop, item.TrangThai)
                };
                lstResult.Add(temp);
            }

            //Các tham số của phân trang như sau:
            //      đầu tiên là danh sách truyền vào phân trang
            //      tham số thứ 2 là vị trí phân trang
            //      tham số cuối là số lượng trang
            var result = PaginatedList<dynamic>.CreateAsync(lstResult, offset ?? 0, limit ?? 10);

            return Json(new { total = lst.ToList().Count, totalNotFiltered = lst.ToList().Count, rows = result });
        }

        //Hàm xử lý các button thao tác, vì bootstrap-table không hỗ trợ update với formatter row nên phải dùng cách này
        public string customThaoTac(string ma, bool tt)
        {
            var result = "<button data-toggle=\"tooltip\" title=\"Xem\" class=\"pd-setting-ed pressed-size ml-1 mr-1\" onclick=\"window.location.href=\'/Courses/Room/Detail?id=" + ma + "\'\"><i class=\"fa fa-eye\" aria-hidden=\"true\"></i></button>";
            if (tt)
            {
                result += "<button data-toggle=\"tooltip\" title=\"Khóa\" class=\"pd-setting-ed mt-1\" onclick=\"setRoomLock(\'" + ma + "\', this)\" ><i data-toggle=\"modal\" class=\"fa fa-lock\" aria-hidden=\"true\"></i></button>";
            }
            else
            {
                result += "<button data-toggle=\"tooltip\" title=\"Mở khóa\" class=\"pd-setting-ed pressed-size mt-1\" onclick=\"setRoomLock(\'" + ma + "\', this)\" ><i data-toggle=\"modal\" class=\"fa fa-unlock\" aria-hidden=\"true\"></i></button>";
            }
            return result;
        }

        //Khóa hoặc mở khóa lớp học
        [HttpPost]
        public async Task<IActionResult> LockRoom(string ma)
        {
            var lp = await db.LopHocs.FirstOrDefaultAsync(x => x.MaLop == ma);
            if (lp.TrangThai)
            {
                lp.TrangThai = false;
            }
            else lp.TrangThai = true;
            db.SaveChanges();

            return Json(new { tt = lp.TrangThai, thaoTac = customThaoTac(lp.MaLop, lp.TrangThai) });
        }
        #endregion Quản lý lớp học

        #region Quản lý báo cáo
        public IActionResult Report()
        {
            //ViewBag thể hiện trang đang được hiển thị trên layout
            ViewBag.ReportList = "active";

            return View();
        }

        //Lấy danh sách báo cáo
        [HttpGet]
        public IActionResult getReport(string? search, string? sort, string? order, int? offset, int? limit)
        {
            var lst = db.BaoCaos.Where(x => x.MaBaoCao != null);

            //Nếu tìm kiếm không rỗng thì xử lý tìm kiếm mã, chỉ mục, nội dung, ghi chú,....
            if (!string.IsNullOrEmpty(search))
            {
                lst = lst.Where(s => s.MaBaoCao.Contains(search)
                                || s.ChiMuc.Contains(search)
                                || s.NoiDung.Contains(search)
                                || s.GhiChu.Contains(search));
            }

            //Xử lý sắp xếp
            if (!string.IsNullOrEmpty(sort) && !string.IsNullOrEmpty(order))
            {
                switch (sort)
                {
                    case "maBaoCao":
                        if (order.Equals("asc"))
                        {
                            lst = lst.OrderBy(x => x.MaBaoCao);
                        }
                        else
                        {
                            lst = lst.OrderByDescending(x => x.MaBaoCao);
                        }
                        break;
                    case "tenOwner":
                        if (order.Equals("asc"))
                        {
                            lst = lst.OrderBy(x => x.MaNd);
                        }
                        else
                        {
                            lst = lst.OrderByDescending(x => x.MaNd);
                        }
                        break;
                    case "chiMuc":
                        if (order.Equals("asc"))
                        {
                            lst = lst.OrderBy(x => x.ChiMuc);
                        }
                        else
                        {
                            lst = lst.OrderByDescending(x => x.ChiMuc);
                        }
                        break;
                    case "noiDung":
                        if (order.Equals("asc"))
                        {
                            lst = lst.OrderBy(x => x.NoiDung);
                        }
                        else
                        {
                            lst = lst.OrderByDescending(x => x.NoiDung);
                        }
                        break;
                    case "thoiGian":
                        if (order.Equals("asc"))
                        {
                            lst = lst.OrderBy(x => x.ThoiGian);
                        }
                        else
                        {
                            lst = lst.OrderByDescending(x => x.ThoiGian);
                        }
                        break;
                }
            }

            List<dynamic> lstResult = new List<dynamic>();
            foreach (var item in lst.ToList())
            {
                var temp = new
                {
                    maBaoCao = item.MaBaoCao,
                    maOwner = item.MaNd,
                    tenOwner = item.getOwner().getFullName(),
                    chiMuc = item.ChiMuc,
                    tenLop = item.getRoom().TenLop,
                    noiDung = item.NoiDung,
                    ghiChu = item.GhiChu,
                    thoiGian = item.ThoiGian,
                    thaoTac = reportThaoTac(item.MaNd, item.MaBaoCao)
                };
                lstResult.Add(temp);
            }

            //Các tham số của phân trang như sau:
            //      đầu tiên là danh sách truyền vào phân trang
            //      tham số thứ 2 là vị trí phân trang
            //      tham số cuối là số lượng trang
            var result = PaginatedList<dynamic>.CreateAsync(lstResult, offset ?? 0, limit ?? 10);

            return Json(new { total = lst.ToList().Count, totalNotFiltered = lst.ToList().Count, rows = result });
        }

        //Hàm xử lý các button thao tác, vì bootstrap-table không hỗ trợ update với formatter row nên phải dùng cách này
        public string reportThaoTac(string maUser, string maBaoCao)
        {
            var result = "<button data-toggle=\"tooltip\" title=\"Gửi thông báo\" class=\"pd-setting-ed pressed-size ml-1 mr-1\" onclick=\"setSendNoti(\'" + maUser + "\')\" ><i data-toggle=\"modal\" class=\"fa fa-send\" aria-hidden=\"true\"></i></button>";
            result += "<button data-toggle=\"tooltip\" title=\"Xóa\" class=\"pd-setting-ed mt-1\" onclick=\"setRemoveReport(\'" + maBaoCao + "\', this)\" ><i data-toggle=\"modal\" class=\"fa fa-trash\" aria-hidden=\"true\"></i></button>";
            
            return result;
        }

        //Gửi thông báo cho người dùng
        [HttpPost]
        public IActionResult sendNoti(string maUser, string nd)
        {
            NotificationController notification = new NotificationController();
            notification.setThongBao(maUser, "Thông báo từ Admin","admin\\" + nd, "");

            return Json(new { tt = true });
        }

        //Xóa báo cáo
        [HttpPost]
        public IActionResult removeReport(List<string> ma)
        {
            foreach(var i in ma)
            {
                var rp = db.BaoCaos.FirstOrDefault(x => x.MaBaoCao == i);
                if (rp != null) db.BaoCaos.Remove(rp);
            }
            db.SaveChanges();

            return Json(new { tt = true });
        }

        #endregion Quản lý báo cáo
    }
}
