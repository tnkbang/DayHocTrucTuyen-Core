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

        public struct UserRoom{
            public NguoiDung User { get; set; }
            public List<LopHoc> Room { get; set; }
            public DateTime ThoiGian { get; set; }
        }

        public static List<UserRoom> SuggestList = new List<UserRoom>();

        //Hàm khởi tạo dữ liệu
        public void CreateData()
        {
            foreach (var u in db.NguoiDungs.ToList())
            {
                SuggestList.Add(new UserRoom { User = u, Room = setSuggest(u.MaNd), ThoiGian = DateTime.Now });
            }
        }

        //Lấy list lớp học theo mã
        public List<LopHoc> getRoom(string maNd)
        {
            var result = SuggestList.FirstOrDefault(x => x.User.MaNd == maNd);

            if (result.User == null)
            {
                var user = db.NguoiDungs.FirstOrDefault(x => x.MaNd == maNd);
                if (user != null) SuggestList.Add(new UserRoom
                {
                    User = user,
                    Room = setSuggest(user.MaNd),
                    ThoiGian = DateTime.Now
                });
            }
            else if (result.ThoiGian.AddDays(1) < DateTime.Now)
            {
                result.Room = setSuggest(maNd);
                result.ThoiGian = DateTime.Now;
            }

            if (result.Room == null || result.Room.Count == 0) return getSuggestAll();
            return result.Room;
        }

        //set gợi ý lớp học theo mã người dùng
        private List<LopHoc> setSuggest(string maNd)
        {
            //lấy list lớp thuộc tag theo lịch sử truy cập, lấy 10 lịch sử gần nhất
            var lopthuoctag = (from ls in db.LichSuTruyCaps
                               join ltt in db.LopThuocTags on ls.MaLop equals ltt.MaLop
                               where ls.MaNd == maNd
                               orderby ls.ThoiGian descending
                               select ltt).ToList().Take(10).DistinctBy(x => x.MaTag);

            List<LopHoc> lst = new List<LopHoc>();

            //Tìm lớp theo tag
            foreach (var t in lopthuoctag.ToList())
            {
                var lop = (from l in db.LopHocs
                           join ltt in db.LopThuocTags on l.MaLop equals ltt.MaLop
                           where ltt.MaTag == t.MaTag
                           select l).ToList().DistinctBy(x => x.MaLop);
                lst.AddRange(lop);
            }

            lst = lst.DistinctBy(x => x.MaLop).ToList();
            lst = sortByInteraction(lst);
            return lst;
        }

        //Lấy gợi ý toàn hệ thống
        private List<LopHoc> getSuggestAll()
        {
            var sug = SuggestList.ToList();
            List<LopHoc> lst = new List<LopHoc>();
            foreach (var s in sug)
            {
                lst.AddRange(s.Room);
            }
            lst = lst.DistinctBy(x => x.MaLop).ToList();

            //Nếu gợi ý quá ít thì thêm lớp mặc định để lấp đầy
            if (lst.Count < 9)
            {
                var lh = db.LopHocs.Where(x => x.TrangThai);
                lst.AddRange(lh);
            }
            lst = lst.DistinctBy(x => x.MaLop).ToList();
            lst = sortByInteraction(lst);

            return lst;
        }

        //Sắp xếp theo lượt tương tác
        private List<LopHoc> sortByInteraction(List<LopHoc> rawData)
        {
            List<sortRaw> tempRaw = new List<sortRaw>();
            foreach(var r in rawData)
            {
                tempRaw.Add(new sortRaw { 
                    room = r, 
                    Members = r.getMembers(), 
                    Liker = r.getSLCamXuc() 
                });
            }
            tempRaw = tempRaw.OrderByDescending(x => x.Liker)
                .ThenByDescending(x => x.Members)
                .ThenByDescending(x => x.room.NgayTao).ToList();

            List<LopHoc> result = new List<LopHoc>();
            foreach(var r in tempRaw)
            {
                result.Add(r.room);
            }

            return result;
        }

        //Struc sắp xếp
        private struct sortRaw
        {
            public LopHoc room { get; set; }
            public int Members { get; set; }
            public int Liker { get; set; }
        }
    }
}
