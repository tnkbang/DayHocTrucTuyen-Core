using DayHocTrucTuyen.Areas.Admin.Models;
using DayHocTrucTuyen.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DayHocTrucTuyen.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Route("Admin/[controller]/[action]")]
    [Authorize(Roles = "01")]
    public class UserController : Controller
    {
        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();

        #region Quản lý người dùng
        //Trang danh sách người dùng
        public IActionResult List()
        {
            //ViewBag thể hiện trang đang được hiển thị trên layout
            ViewBag.UserList = "active";

            return View();
        }

        //Lấy danh sách người dùng
        [HttpGet]
        public IActionResult getList(string? search, string? sort, string? order, int? offset, int? limit)
        {
            var lst = db.NguoiDungs.Where(x => x.MaLoai != "01");

            //Nếu tìm kiếm không rỗng thì xử lý tìm kiếm mã, họ tên, email, loại,....
            if (!string.IsNullOrEmpty(search))
            {
                var maLoai = "";
                if (search.ToLower().Equals("giáo viên")) { maLoai = "02"; }
                if (search.ToLower().Equals("học sinh")) { maLoai = "03"; }

                lst = lst.Where(s => string.Concat(s.HoLot, " ", s.Ten).Contains(search) 
                                || s.MaNd.Contains(search) 
                                || s.MaLoai.Equals(maLoai)
                                || s.Email.Contains(search));
            }

            //Xử lý sắp xếp
            if (!string.IsNullOrEmpty(sort) && !string.IsNullOrEmpty(order))
            {
                switch (sort)
                {
                    case "maNd":
                        if (order.Equals("asc"))
                        {
                            lst = lst.OrderBy(x => x.MaNd);
                        }
                        else
                        {
                            lst = lst.OrderByDescending(x => x.MaNd);
                        }
                        break;
                    case "loai":
                        if (order.Equals("asc"))
                        {
                            lst = lst.OrderBy(x => x.MaLoai);
                        }
                        else
                        {
                            lst = lst.OrderByDescending(x => x.MaLoai);
                        }
                        break;
                    case "hoTen":
                        if (order.Equals("asc"))
                        {
                            lst = lst.OrderBy(x => x.Ten).ThenBy(x => x.HoLot);
                        }
                        else
                        {
                            lst = lst.OrderByDescending(x => x.Ten).ThenByDescending(x => x.HoLot);
                        }
                        break;
                    case "email":
                        if (order.Equals("asc"))
                        {
                            lst = lst.OrderBy(x => x.Email);
                        }
                        else
                        {
                            lst = lst.OrderByDescending(x => x.Email);
                        }
                        break;
                    case "gioiTinh":
                        if (order.Equals("asc"))
                        {
                            lst = lst.OrderBy(x => x.GioiTinh);
                        }
                        else
                        {
                            lst = lst.OrderByDescending(x => x.GioiTinh);
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
                    maNd = item.MaNd,
                    loai = item.getTenLoai(),
                    imgAvt = item.getImageAvt(),
                    hoTen = item.HoLot + " " + item.Ten,
                    email = item.Email,
                    gioiTinh = item.GioiTinh == 1 ? "Nam" : item.GioiTinh == 2 ? "Nữ" : item.GioiTinh == 3 ? "Thứ 3" : null,
                    sdt = item.Sdt,
                    biDanh = item.BiDanh == item.MaNd ? null : item.BiDanh,
                    trangThai = item.TrangThai ? "Hoạt động" : "Bị khóa",
                    ttBool = item.TrangThai,
                    thaoTac = customThaoTac(item.MaNd, item.TrangThai)
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
            var result = "<button data-toggle=\"tooltip\" title=\"Xem\" class=\"pd-setting-ed pressed-size ml-1 mr-1\" onclick=\"window.location.href=\'/Profile/Info?id=" + ma + "\'\"><i class=\"fa fa-eye\" aria-hidden=\"true\"></i></button>";
            if (tt)
            {
                result += "<button data-toggle=\"tooltip\" title=\"Khóa\" class=\"pd-setting-ed mt-1\" onclick=\"setUserLock(\'" + ma + "\', this)\" ><i data-toggle=\"modal\" class=\"fa fa-lock\" aria-hidden=\"true\"></i></button>";
            }
            else
            {
                result += "<button data-toggle=\"tooltip\" title=\"Mở khóa\" class=\"pd-setting-ed pressed-size mt-1\" onclick=\"setUserLock(\'" + ma + "\', this)\" ><i data-toggle=\"modal\" class=\"fa fa-unlock\" aria-hidden=\"true\"></i></button>";
            }
            return result;
        }

        //Khóa hoặc mở khóa người dùng
        [HttpPost]
        public async Task<IActionResult> LockUser(string ma)
        {
            var user = await db.NguoiDungs.FirstOrDefaultAsync(x => x.MaNd == ma);
            if (user.TrangThai)
            {
                user.TrangThai = false;
            }
            else user.TrangThai = true;
            db.SaveChanges();

            return Json(new { tt = user.TrangThai, thaoTac = customThaoTac(user.MaNd, user.TrangThai) });
        }
        #endregion Quản lý người dùng

        #region Phê duyệt người dùng
        //Trang phê duyệt người dùng nâng cấp lên giáo viên
        public IActionResult Approve()
        {
            //ViewBag thể hiện trang đang được hiển thị trên layout
            ViewBag.Approve = "active";

            return View();
        }

        //Lấy danh sách phê duyệt
        [HttpGet]
        public IActionResult getApprove(string? search, string? sort, string? order, int? offset, int? limit)
        {
            var lst = db.PheDuyets.Where(x => x.TrangThai);

            //Nếu tìm kiếm không rỗng thì xử lý tìm kiếm mã, họ tên, email, loại,....
            if (!string.IsNullOrEmpty(search))
            {
                var nd = db.NguoiDungs.FirstOrDefault(s => string.Concat(s.HoLot, " ", s.Ten).Contains(search));

                if (nd != null)
                {
                    lst = lst.Where(s => s.MaNd == nd.MaNd);
                }
                else
                {
                    lst = lst.Where(s => s.MaNd.Contains(search));
                }
            }

            //Xử lý sắp xếp
            if (!string.IsNullOrEmpty(sort) && !string.IsNullOrEmpty(order))
            {
                switch (sort)
                {
                    case "maNd":
                        if (order.Equals("asc"))
                        {
                            lst = lst.OrderBy(x => x.MaNd);
                        }
                        else
                        {
                            lst = lst.OrderByDescending(x => x.MaNd);
                        }
                        break;
                    case "ngayDK":
                        if (order.Equals("asc"))
                        {
                            lst = lst.OrderBy(x => x.NgayDangKy);
                        }
                        else
                        {
                            lst = lst.OrderByDescending(x => x.NgayDangKy);
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
                var user = item.getNguoiDung();
                var temp = new
                {
                    maNd = item.MaNd,
                    hoTen = user.getFullName(),
                    email = user.Email,
                    gioiTinh = user.GioiTinh == 1 ? "Nam" : user.GioiTinh == 2 ? "Nữ" : user.GioiTinh == 3 ? "Thứ 3" : null,
                    sdt = user.Sdt,
                    ngaySinh = user.NgaySinh?.ToString("g"),
                    biDanh = user.BiDanh == user.MaNd ? null : user.BiDanh,
                    ngayDK = item.NgayDangKy.ToString("g"),
                    trangThai = item.TrangThai ? "Chờ phê duyệt" : "Bị từ chối",
                    ttBool = item.TrangThai,
                    thaoTac = customApprove(item.MaNd)
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
        public string customApprove(string ma)
        {
            var result = "<button data-toggle=\"tooltip\" title=\"Đồng ý\" class=\"pd-setting-ed pressed-size ml-1 mr-1\" onclick=\"approveAccept(\'" + ma + "\')\" ><i data-toggle=\"modal\" class=\"fa fa-check\" aria-hidden=\"true\"></i></button>";
            result += "<button data-toggle=\"tooltip\" title=\"Từ chối\" class=\"pd-setting-ed mt-1\" onclick=\"approveRefuse(\'" + ma + "\')\" ><i data-toggle=\"modal\" class=\"fa fa-close\" aria-hidden=\"true\"></i></button>";
            return result;
        }

        //Đồng ý hoặc từ chối người dùng nâng cấp giáo viên
        [HttpPost]
        public IActionResult setApprove(string ma, bool tt, string gc)
        {
            var user = db.NguoiDungs.FirstOrDefault(x => x.MaNd == ma);
            var pd = db.PheDuyets.FirstOrDefault(x => x.MaNd == ma);
            if(pd != null && user != null)
            {
                if (tt)
                {
                    user.MaLoai = "02";
                    db.PheDuyets.Remove(pd);
                }
                else
                {
                    pd.TrangThai = false;
                    pd.GhiChu = gc;
                }
                db.SaveChanges();
            }

            return Json(new { tt = true });
        }
        #endregion Phê duyệt người dùng
    }
}
