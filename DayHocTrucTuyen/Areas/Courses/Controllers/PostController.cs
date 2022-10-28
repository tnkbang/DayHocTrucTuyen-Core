using DayHocTrucTuyen.Areas.User.Controllers;
using DayHocTrucTuyen.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DayHocTrucTuyen.Areas.Courses.Controllers
{
    [Area(nameof(Courses))]
    [Route("Courses/[controller]/[action]")]
    [Authorize]
    public class PostController : Controller
    {
        // GET: Courses/Post
        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();

        //Thêm mới bài đăng
        [HttpPost]
        public async Task<IActionResult> createPost(string malop, string noidung, List<IFormFile> files)
        {
            BaiDang newPost = new BaiDang();

            newPost.MaBai = newPost.setMa(malop);
            newPost.MaLop = malop;
            newPost.MaNd = User.Claims.First().Value;
            newPost.ThoiGian = DateTime.Now;
            newPost.NoiDung = noidung;
            newPost.TrangThai = true;

            if(files.Count != 0)
            {
                var temp = "";
                //Khai báo đường dẫn lưu file
                var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot\\Content\\filePost\\");
                bool basePathExists = Directory.Exists(basePath);

                //Nếu thư mục không có thì tạo mới
                if (!basePathExists) Directory.CreateDirectory(basePath);

                foreach(var file in files)
                {
                    string file_extension = Path.GetFileName(file.FileName).Substring(Path.GetFileName(file.FileName).LastIndexOf('.'));
                    var fileName = newPost.MaBai + "-" + DateTime.Now.Millisecond + file_extension;
                    var filePath = Path.Combine(basePath, fileName);

                    //Thêm file vào server và cập nhật vào csdl
                    if (fileName != null && !System.IO.File.Exists(filePath))
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        temp += fileName + ",";
                    }
                    newPost.DinhKem = temp.Substring(0, temp.Length - 1);
                }
            }

            db.BaiDangs.Add(newPost);
            db.SaveChanges();

            //Gửi thông báo đến tất cả thành viên thuộc lớp
            NotificationController notification = new NotificationController();
            var lop = db.LopHocs.FirstOrDefault(x => x.MaLop == malop);
            var thanhvienlop = db.HocSinhThuocLops.Where(x => x.MaLop == lop.MaLop && x.MaNd != newPost.MaNd);
            foreach (var tvl in thanhvienlop)
            {
                notification.setThongBao(tvl.MaNd, "Bài đăng mới", "post" + "\\Từ lớp " + lop.TenLop, "Courses/Room/Detail?id=" + lop.MaLop);
            }

            return Json(new { mess = "Thành công" });
        }

        //Khóa hoặc mở khóa bài đăng
        [HttpPost]
        public IActionResult setTrangThaiPost(string maPost)
        {
            var post = db.BaiDangs.FirstOrDefault(x => x.MaBai == maPost);
            if (post == null)
            {
                return Json(new { tt = false });
            }
            if (post.TrangThai) post.TrangThai = false;
            else post.TrangThai = true;

            db.SaveChanges();

            if (post.TrangThai)
            {
                return Json(new { tt = true });
            }

            return Json(new { tt = false });
        }

        //Xóa bài đăng
        [HttpPost]
        public IActionResult deletePost(string maPost)
        {
            var post = db.BaiDangs.FirstOrDefault(x => x.MaBai == maPost);
            if (post == null)
            {
                return Json(new { tt = false });
            }
            //Xóa file đính kèm
            if (post.DinhKem != null)
            {
                string[] list = post.DinhKem.Split(',');
                foreach (string str in list)
                {
                    //Khai báo đường dẫn lưu file
                    var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot\\Content\\filePost\\");

                    //Xóa file cũ khỏi server
                    if (System.IO.File.Exists(Path.Combine(basePath, str)))
                    {
                        System.IO.File.Delete(basePath + str);
                    }
                }
            }
            //Xóa bình luận
            if (post.getSLBinhLuan() != 0)
            {
                var listComment = db.BinhLuans.Where(x => x.MaBai == post.MaBai);
                foreach (var comment in listComment)
                {
                    db.BinhLuans.Remove(comment);
                }
            }
            //Xóa cảm xúc
            if (post.getSLCamXuc() != 0)
            {
                var listYT = db.CamXucs.Where(x => x.MaBai == post.MaBai);
                foreach (var yT in listYT)
                {
                    db.CamXucs.Remove(yT);
                }
            }
            //Xóa trạng thái ghim
            if (post.isGhim())
            {
                var ghim = db.Ghims.FirstOrDefault(x => x.MaBai == post.MaBai);
                db.Ghims.Remove(ghim);
            }

            db.BaiDangs.Remove(post);
            db.SaveChanges();

            return Json(new { tt = true });
        }

        //Set yêu thích bài đăng hoặc bỏ yêu thích
        [HttpPost]
        public IActionResult setLikePost(string maPost, string maND)
        {
            CamXuc yt = db.CamXucs.FirstOrDefault(x => x.MaBai == maPost && x.MaNd == maND);
            BaiDang bd = db.BaiDangs.FirstOrDefault(x => x.MaBai == maPost);

            if (yt == null)
            {
                CamXuc newYT = new CamXuc();
                newYT.MaBai = maPost;
                newYT.MaNd = maND;
                newYT.ThoiGian = DateTime.Now;

                db.CamXucs.Add(newYT);
                db.SaveChanges();
                return Json(new { tt = true, sl = bd.getSLCamXuc() });
            }
            else
            {
                db.CamXucs.Remove(yt);
                db.SaveChanges();
                return Json(new { tt = false, sl = bd.getSLCamXuc() });
            }
        }

        //Thêm mới bình luận
        [HttpPost]
        public async Task<IActionResult> createComment(string maPost, string nd, List<IFormFile> files)
        {
            BinhLuan newBL = new BinhLuan();

            newBL.MaBai = maPost;
            newBL.MaNd = User.Claims.First().Value;
            newBL.ThoiGian = DateTime.Now;
            newBL.NoiDung = nd;

            if (files.Count != 0)
            {
                var temp = "";
                //Khai báo đường dẫn lưu file
                var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot\\Content\\filePost\\");
                bool basePathExists = Directory.Exists(basePath);

                //Nếu thư mục không có thì tạo mới
                if (!basePathExists) Directory.CreateDirectory(basePath);

                foreach (var file in files)
                {
                    string file_extension = Path.GetFileName(file.FileName).Substring(Path.GetFileName(file.FileName).LastIndexOf('.'));
                    var fileName = newBL.MaBai + "-" + DateTime.Now.Millisecond + file_extension;
                    var filePath = Path.Combine(basePath, fileName);

                    //Thêm file vào server và cập nhật vào csdl
                    if (fileName != null && !System.IO.File.Exists(filePath))
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        temp += fileName + ",";
                    }
                    newBL.DinhKem = temp.Substring(0, temp.Length - 1);
                }
            }

            db.BinhLuans.Add(newBL);
            db.SaveChanges();

            var userComment = db.NguoiDungs.FirstOrDefault(x => x.MaNd == User.Claims.First().Value);

            var json = new
            {
                ma = userComment.MaNd,
                anh = userComment.getImageAvt(),
                hoten = userComment.getFullName(),
                postId = newBL.MaBai,
                postTime = newBL.ThoiGian.ToString(),
                postTimeCus = newBL.ThoiGian.ToString("yyyy-MM-dd'T'HH:mm:ss")
            };

            return Json(json);
        }

        //Xóa bình luận
        public IActionResult deleteComment(string maPost, DateTime thoigian)
        {
            var del = db.BinhLuans.Where(x => x.MaBai == maPost && x.MaNd == User.Claims.First().Value);
            foreach (var i in del)
            {
                if (i.ThoiGian.ToString() == thoigian.ToString())
                {
                    db.BinhLuans.Remove(i);
                }
            }
            db.SaveChanges();
            return Json(new { tt = true });
        }

        //Set ghim hoặc bỏ ghim bài đăng
        [HttpPost]
        public IActionResult setGhim(string maPost)
        {
            var ghim = db.Ghims.FirstOrDefault(x => x.MaBai == maPost);
            if (ghim == null)
            {
                Ghim newGhim = new Ghim();
                newGhim.MaBai = maPost;
                newGhim.ThoiGian = DateTime.Now;
                db.Ghims.Add(newGhim);
                db.SaveChanges();

                return Json(new { tt = true });
            }
            db.Ghims.Remove(ghim);
            db.SaveChanges();

            return Json(new { tt = false });
        }

        //Get file
        public FileResult getFile(string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot\\Content\\filePost\\");
            var name = fileName.Substring(16);
            byte[] bytes = System.IO.File.ReadAllBytes(path + fileName);
            return File(bytes, "application/octet-stream", name);
        }

        //Xem file pdf trên trình duyệt
        [Route("{fileName?}")]
        public IActionResult ViewPDF(string fileName)
        {
            if (fileName == null)
            {
                return NotFound();
            }
            ViewBag.FileName = fileName;
            return View();
        }
    }
}
