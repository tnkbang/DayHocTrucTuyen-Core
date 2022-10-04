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

        public async Task<IActionResult> List(string? q, int? p, int? s, string? l)
        {
            var mems = db.NguoiDungs.Where(x => x.MaLoai != "01");

            //Lọc người dùng theo các tiêu chí họ tên, email, mã,...
            if (!String.IsNullOrEmpty(q))
            {
                mems = mems.Where(s => string.Concat(s.HoLot, " ", s.Ten).Contains(q) || s.MaNd.Contains(q) || s.Email.Contains(q));
            }

            //Select trả về khác rỗng và khác 01 (01 là trường hợp admin không hiển thị, thay vào đó là hiển thị tất cả)
            if (!String.IsNullOrEmpty(l) && l != "01")
            {
                mems = mems.Where(s => s.MaLoai == l);
            }

            //Số lượng người dùng được trả về trên một trang
            int pageSize = s ?? 5;

            //Chờ đợi xử lý phân trang rồi mới trả về view
            //Các tham số của phân trang như sau:
            //      nd.AsNoTracking() là danh sách người dùng chỉ xem
            //      p là trang muốn hiển thị, ở đây nếu không nhập thì ngầm hiểu trang hiển thị là 1 tức là trang đầu
            //      pageSize là số số lượng người hiển thị trên trang
            return View(await PaginatedList<NguoiDung>.CreateAsync(mems.OrderByDescending(x => x.NgayTao).AsNoTracking(), p ?? 1, pageSize));
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
