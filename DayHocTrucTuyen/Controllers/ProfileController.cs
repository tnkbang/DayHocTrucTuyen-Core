using DayHocTrucTuyen.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DayHocTrucTuyen.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        // GET: Profile
        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();

        //Trang thông tin người dùng
        public IActionResult Info(string id)
        {
            NguoiDung userMa = db.NguoiDungs.FirstOrDefault(x => x.MaNd == id);
            NguoiDung userBiDanh = db.NguoiDungs.FirstOrDefault(x => x.BiDanh == id);
            if (userMa == null && userBiDanh == null || User == null)
            {
                //Response.StatusCode = 404;
                return NotFound();
            }
            if (userMa == null)
            {
                setXemTrang(userBiDanh.MaNd, User.Claims.First().Value);
                return View(userBiDanh);
            }
            setXemTrang(userMa.MaNd, User.Claims.First().Value);
            return View(userMa);
        }
        //Hàm set xem trang
        public void setXemTrang(string nd, string nx)
        {
            if (nd == nx) return;
            XemTrang xt = db.XemTrangs.Where(x => x.NguoiDung == nd && x.NguoiXem == nx).OrderByDescending(x => x.MaXt).FirstOrDefault();
            if (xt != null)
            {
                TimeSpan minTime = new TimeSpan(24, 0, 0);
                if (DateTime.Now - xt.ThoiGian > minTime)
                {
                    XemTrang newXT = new XemTrang();
                    newXT.MaXt = newXT.setMa(nd);
                    newXT.NguoiDung = nd;
                    newXT.NguoiXem = nx;
                    newXT.ThoiGian = DateTime.Now;

                    db.XemTrangs.Add(newXT);
                    db.SaveChanges();
                }
            }
            else
            {
                XemTrang newXT = new XemTrang();
                newXT.MaXt = newXT.setMa(nd);
                newXT.NguoiDung = nd;
                newXT.NguoiXem = nx;
                newXT.ThoiGian = DateTime.Now;

                db.XemTrangs.Add(newXT);
                db.SaveChanges();
            }
        }
        //Hàm set thích trang
        public IActionResult setThichTrang(string nd, string nt)
        {
            if (nd == nt) return Json(new { tt = false });
            ThichTrang yt = db.ThichTrangs.Where(x => x.NguoiDung == nd && x.NguoiThich == nt).OrderByDescending(x => x.MaYt).FirstOrDefault();
            if (yt != null)
            {
                TimeSpan minTime = new TimeSpan(24, 0, 0);
                if (DateTime.Now - yt.ThoiGian > minTime)
                {
                    ThichTrang newYT = new ThichTrang();
                    newYT.MaYt = newYT.setMa(nd);
                    newYT.NguoiDung = nd;
                    newYT.NguoiThich = nt;
                    newYT.ThoiGian = DateTime.Now;

                    db.ThichTrangs.Add(newYT);
                    db.SaveChanges();
                }
            }
            else
            {
                ThichTrang newYT = new ThichTrang();
                newYT.MaYt = newYT.setMa(nd);
                newYT.NguoiDung = nd;
                newYT.NguoiThich = nt;
                newYT.ThoiGian = DateTime.Now;

                db.ThichTrangs.Add(newYT);
                db.SaveChanges();
            }
            return Json(new { tt = true });
        }
    }
}
