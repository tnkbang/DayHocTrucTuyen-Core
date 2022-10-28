using DayHocTrucTuyen.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DayHocTrucTuyen.Areas.Courses.Controllers
{
    [Area(nameof(Courses))]
    [Route("Courses/[controller]/[action]/{id?}")]
    [Authorize]
    public class ReportController : Controller
    {
        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();

        [HttpPost]
        public IActionResult createReport(string chimuc, string noidung, string ghichu)
        {
            if(String.IsNullOrEmpty(chimuc) || String.IsNullOrEmpty(noidung))
            {
                return Json(new { tt = false });
            }

            BaoCao newBC = new BaoCao();

            newBC.MaBaoCao = newBC.setMa();
            newBC.MaNd = User.Claims.First().Value;
            newBC.ChiMuc = chimuc;
            newBC.NoiDung = noidung;
            if(!String.IsNullOrEmpty(ghichu)) newBC.GhiChu = ghichu;
            newBC.ThoiGian = DateTime.Now;

            db.BaoCaos.Add(newBC);
            db.SaveChanges();

            return Json(new { tt = true });
        }
    }
}
