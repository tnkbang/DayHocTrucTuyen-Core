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

        public IActionResult List()
        {
            //ViewBag thể hiện trang đang được hiển thị trên layout
            ViewBag.UserList = "active";

            return View();
        }
        [HttpGet]
        public IActionResult getList(string? search, string? sort, string? order, int? offset, int? limit)
        {
            var lst = db.NguoiDungs.Where(x => x.MaLoai != "01");

            //Nếu tìm kiếm không rỗng thì xử lý tìm kiếm mã, họ tên, email,....
            if (!string.IsNullOrEmpty(search))
            {
                lst = db.NguoiDungs.Where(
                                s => string.Concat(s.HoLot, " ", s.Ten).Contains(search) 
                                || s.MaNd.Contains(search) 
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
                    imgAvt = item.getImageAvt(),
                    hoTen = item.HoLot + " " + item.Ten,
                    email = item.Email,
                    ngayTao = item.NgayTao.ToString("g"),
                    trangThai = item.TrangThai
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

            return Json(new { tt = user.TrangThai });
        }
    }
}
