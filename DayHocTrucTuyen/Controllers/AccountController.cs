using DayHocTrucTuyen.Models;
using DayHocTrucTuyen.Models.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace DayHocTrucTuyen.Controllers
{
    public class AccountController : Controller
    {
        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();

        //Trang đăng nhập
        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl)
        {
            LoginModel loginModel = new LoginModel();
            loginModel.ReturnUrl = string.IsNullOrEmpty(ReturnUrl) ? "/" : ReturnUrl;
            ViewBag.members = db.NguoiDungs.Count();
            ViewBag.classroom = db.LopHocs.Count();
            return View(loginModel);
        }

        //Trang đăng ký
        [AllowAnonymous]
        public IActionResult Register()
        {
            ViewBag.members = db.NguoiDungs.Count();
            ViewBag.classroom = db.LopHocs.Count();
            return View();
        }

        //Xác thực đăng nhập
        public async Task<bool> setLogin(string email, string pass, bool re, bool useLink)
        {
            NguoiDung temp = new NguoiDung();
            NguoiDung user = new NguoiDung();

            if (useLink)
            {
                user = db.NguoiDungs.Where(x => x.Email == email).FirstOrDefault();
            }
            else
            {
                user = db.NguoiDungs.Where(x => x.Email == email && x.MatKhau == temp.mahoaMatKhau(pass)).FirstOrDefault();
            }
            
            if (user != null)
            {
                //Tạo list lưu chủ thể đăng nhập
                var claims = new List<Claim>() {
                        new Claim("MaNd", user.MaNd),
                        new Claim("LoaiNd", user.MaLoai),
                        new Claim("Ten", user.Ten)
                    };

                //Tạo Identity để xác thực và xác nhận
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                //Gọi hàm đăng nhập bất đồng bộ của HttpContext
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                {
                    IsPersistent = re
                });

                return true;
            }

            return false;
        }

        //Gọi đăng nhập
        [AllowAnonymous]
        public async Task<IActionResult> getLogin(string email, string pass, bool re)
        {
            var login = await setLogin(email, pass, re, false);
            if (login)
            {
                return Json(new { tt = true });
            }
            return Json(new { tt = false, mess = "Email hoặc mật khẩu không chính xác !" });
        }

        //Hàm đăng xuất
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            //Gọi hàm đăng xuất của HttpContext
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Json(new { tt = true, mess = "Đăng xuất thành công !" });
        }

        //Kiểm tra email có tồn tại trên hệ thống chưa
        [AllowAnonymous]
        public async Task<IActionResult> ktEmail(string email)
        {
            var temp = await db.NguoiDungs.FirstOrDefaultAsync(x => x.Email == email);

            return temp != null ? Json(new { email = false }) : Json(new { email = true });
        }

        //Đăng nhập với tài khoản google
        [AllowAnonymous]
        public async Task<IActionResult> loginWithGoogle(string hoten, string email, string img_avt)
        {
            var user = await db.NguoiDungs.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
            {
                await createAccount(hoten.Substring(0, hoten.LastIndexOf(' ')), hoten.Substring(hoten.LastIndexOf(' ') + 1), email, "userloginwithgoogle" + email);

                var userLogin = await db.NguoiDungs.FirstOrDefaultAsync(x => x.Email == email);
                if (userLogin != null && img_avt != null)
                {
                    //Khai báo đường dẫn lưu file
                    var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot\\Content\\Img\\UserAvt\\");
                    bool basePathExists = Directory.Exists(basePath);

                    //Nếu thư mục không có thì tạo mới
                    if (!basePathExists) Directory.CreateDirectory(basePath);

                    var fileName = "avt-" + userLogin.MaNd + "-" + DateTime.Now.Millisecond + ".jpg";
                    var filePath = Path.Combine(basePath, fileName);

                    //Nếu file không tồn tại thì thêm file vào server và cập nhật vào csdl
                    if (!System.IO.File.Exists(filePath))
                    {
                        using (WebClient webClient = new WebClient())
                        {
                            byte[] dataArr = webClient.DownloadData(img_avt);
                            System.IO.File.WriteAllBytes(filePath, dataArr);
                        }

                        userLogin.ImgAvt = fileName;
                    }

                    db.SaveChanges();
                }

                return Json(new { tt = true, mess = "Đăng nhập thành công !" });
            }
            await setLogin(email, "", false, true);
            return Json(new { tt = true, mess = "Đăng nhập thành công !" });
        }

        //Tạo mới tài khoản
        [AllowAnonymous]
        public async Task<IActionResult> createAccount(string holot, string ten, string email, string matkhau)
        {
            if (ten == "" || email == "" || matkhau == "")
            {
                return Json(new { tt = false, erro = "form", mess = "Chưa nhập đủ thông tin !<br>Tên, email và mật khẩu là bắt buộc." });
            }
            var emailCheck = await db.NguoiDungs.FirstOrDefaultAsync(x => x.Email == email);
            if (emailCheck != null)
            {
                return Json(new { tt = false, erro = "email", mess = "Email đã tồn tại trên hệ thống !" });
            }

            NguoiDung newUser = new NguoiDung();
            newUser.MaNd = newUser.setMaUser();
            newUser.MaLoai = "03";
            newUser.HoLot = holot;
            newUser.Ten = ten;
            newUser.Email = email;
            newUser.MatKhau = newUser.mahoaMatKhau(matkhau);
            newUser.BiDanh = newUser.MaNd;
            newUser.TrangThai = true;
            newUser.NgayTao = newUser.setNgayTao();

            db.NguoiDungs.Add(newUser);
            db.SaveChanges();

            await setLogin(newUser.Email, matkhau, false, false);
            return Json(new { tt = true, mess = "Đăng ký tài khoản thành công !" });
        }

        //Nâng cấp tài khoản
        [Authorize]
        public IActionResult Upgrade()
        {
            var user = db.NguoiDungs.FirstOrDefault(x => x.MaNd == User.Claims.First().Value);
            if (user.isUpgrade()) return Redirect("/");

            var model = db.GoiNangCaps.ToList();
            return View(model);
        }
    }
}
