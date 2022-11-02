using DayHocTrucTuyen.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DayHocTrucTuyen.Areas.Courses.Controllers
{
    [Area(nameof(Courses))]
    [Route("Courses/[controller]/[action]/{id?}")]
    [AllowAnonymous]
    public class SuggestController : Controller
    {
        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();

        //Lấy gợi ý lớp học
        public List<LopHoc> getSuggest(string maNd)
        {
            //lấy list lớp thuộc tag theo lịch sử truy cập
            var lopthuoctag = from ls in db.LichSuTruyCaps
                      join ltt in db.LopThuocTags on ls.MaLop equals ltt.MaLop
                      where ls.MaNd == maNd
                      orderby ls.ThoiGian descending
                      select ltt;
            
            //Lấy duy nhất mã tag trong list để làm gọn
            lopthuoctag = lopthuoctag.DistinctBy(x => x.MaTag);
            List<LopHoc> result = new List<LopHoc>();

            //Tìm lớp theo tag
            foreach (var t in lopthuoctag)
            {
                var lop = db.LopHocs.Where(x => x.MaLop == t.MaLop);
                result.AddRange(lop);
            }

            //Trả về gợi ý 10 lớp học
            return result.Take(10).ToList();
        }
    }
}
