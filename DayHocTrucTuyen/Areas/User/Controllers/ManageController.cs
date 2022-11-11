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
            return View();
        }

        //Lấy thông tin giao dịch trong năm hiện tại cho biểu đồ thống kê lịch sử giao dịch
        [Authorize]
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
    }
}
