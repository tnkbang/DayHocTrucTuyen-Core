using DayHocTrucTuyen.Areas.Admin.Models;
using DayHocTrucTuyen.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DayHocTrucTuyen.Areas.User.Controllers
{
    [Area(nameof(User))]
    [Route("user/[controller]/[action]/{id?}")]
    [Authorize]
    public class ManageController : Controller
    {
        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();
        public IActionResult Index()
        {
            var user = db.NguoiDungs.FirstOrDefault(x => x.MaNd == User.Claims.First().Value);
            return View(user);
        }

        //Trang thống kê lịch sử giao dịch
        public IActionResult TransHistory()
        {
            var user = db.NguoiDungs.FirstOrDefault(x => x.MaNd == User.Claims.First().Value);
            return View(user);
        }

        //Lấy thông tin giao dịch trong năm hiện tại cho biểu đồ thống kê lịch sử giao dịch
        [HttpGet]
        public IActionResult getTransHisOfYear()
        {
            var thisYear = DateTime.Now.Year;
            var maNd = User.Claims.First().Value;

            List<double> lstThu = new List<double>();
            List<double> lstChi = new List<double>();
            List<double> lstSoDu = new List<double>();

            for(int i = 1; i <= 12; i++)
            {
                double tempThu = 0;
                double tempChi = 0;
                double tempSoDu = 0;
                var temp = db.LichSuGiaoDiches.Where(x=>x.MaNd == maNd && x.ThoiGian.Year == thisYear && x.ThoiGian.Month == i).ToList();
                
                foreach(var ls in temp)
                {
                    if (ls.ThuVao) tempThu += ls.SoTien;
                    else tempChi += ls.SoTien;
                    tempSoDu = ls.SoDu;
                }

                lstThu.Add(tempThu);
                lstChi.Add(tempChi);
                lstSoDu.Add(tempSoDu);
            }

            return Json(new { thu = lstThu, chi = lstChi, sodu = lstSoDu });
        }


        //Lấy thông tin lịch sử giao dịch đưa vào table
        [HttpGet]
        public IActionResult getTransHisTable(string? search, string? sort, string? order, int? offset, int? limit)
        {
            var maNd = User.Claims.First().Value;
            var lst = db.LichSuGiaoDiches.Where(x => x.MaNd == maNd);

            //Nếu tìm kiếm không rỗng thì xử lý tìm kiếm mã, chỉ mục, nội dung, ghi chú,....
            if (!string.IsNullOrEmpty(search))
            {
                DateTime timer;
                try
                {
                    timer = DateTime.Parse(search);
                    if (timer.Year < 2000) timer = DateTime.Now;
                }
                catch (Exception) {
                    timer = DateTime.Now;
                }
                lst = lst.Where(s => s.ThoiGian.Date == timer.Date);
            }

            //Xử lý sắp xếp
            if (!string.IsNullOrEmpty(sort) && !string.IsNullOrEmpty(order))
            {
                switch (sort)
                {
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
                    case "soTien":
                        if (order.Equals("asc"))
                        {
                            lst = lst.OrderBy(x => x.SoTien);
                        }
                        else
                        {
                            lst = lst.OrderByDescending(x => x.SoTien);
                        }
                        break;
                }
            }

            List<dynamic> lstResult = new List<dynamic>();
            foreach (var item in lst.ToList())
            {
                var temp = new
                {
                    thoiGian = item.ThoiGian.ToString("g"),
                    soTien = item.SoTien.ToString("n0"),
                    soDu = item.SoDu.ToString("n0"),
                    thuVao = item.ThuVao,
                    ghiChu = item.GhiChu
                };
                lstResult.Add(temp);
            }

            //Các tham số của phân trang như sau:
            //      đầu tiên là danh sách truyền vào phân trang
            //      tham số thứ 2 là vị trí phân trang
            //      tham số cuối là số lượng trang
            var result = PaginatedList<dynamic>.Create(lstResult, offset ?? 0, limit ?? 10);

            return Json(new { total = lst.ToList().Count, totalNotFiltered = lst.ToList().Count, rows = result });
        }
    }
}
