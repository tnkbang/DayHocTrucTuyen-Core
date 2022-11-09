using DayHocTrucTuyen.Areas.User.Controllers;
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
        public IActionResult Login(string? ReturnUrl)
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

        //Trang quên mật khẩu
        [AllowAnonymous]
        public IActionResult ForgotPassword(string? ReturnUrl)
        {
            LoginModel loginModel = new LoginModel();
            loginModel.ReturnUrl = string.IsNullOrEmpty(ReturnUrl) ? "/" : ReturnUrl;
            ViewBag.members = db.NguoiDungs.Count();
            ViewBag.classroom = db.LopHocs.Count();
            return View(loginModel);
        }

        //Xác thực đăng nhập
        public async Task<int> setLogin(string email, string pass, bool re, bool useLink)
        {
            //Trạng thái của return:
            // 0 - Email hoặc mật khẩu không chính xác
            // 1 - Tài khoản bị khóa
            // 2 - Đăng nhập thành công

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
                if (!user.TrangThai)
                {
                    return 1;
                }
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

                return 2;
            }

            return 0;
        }

        //Gọi đăng nhập
        [AllowAnonymous]
        public async Task<IActionResult> getLogin(string email, string pass, bool re)
        {
            var login = await setLogin(email, pass, re, false);
            if (login == 2)
            {
                return Json(new { tt = true });
            }
            if(login == 1)
            {
                return Json(new { tt = false, mess = "Tài khoản của bạn bị khóa !" });
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
                    var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot\\Content\\Img\\userAvt\\");
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
            var login = await setLogin(email, "", false, true);
            if (login == 1)
            {
                return Json(new { tt = false, mess = "Tài khoản của bạn bị khóa !" });
            }
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
            if(user != null && user.isUpgrade()) return Redirect("/");

            var model = db.GoiNangCaps.ToList();
            return View(model);
        }

        struct userChangePass
        {
            public string email { get; set; }
            public string token { get; set; }
            public DateTime thoiGian { get; set; }
        }
        static List<userChangePass> lstChangePass = new List<userChangePass>();

        //Lấy thông tin khi quên mật khẩu
        [AllowAnonymous]
        [HttpPost]
        public IActionResult getForgotPassord(string email)
        {
            var user = db.NguoiDungs.FirstOrDefault(x => x.Email == email);
            if (user == null) return Json(new { tt = false, mess = "Email không tồn tại trên hệ thống !" });
            else
            {
                var currentURL = Request.Scheme + "://" + Request.Host.Value;
                userChangePass userChange = lstChangePass.FirstOrDefault(x => x.email == user.Email);
                
                //Token phải là duy nhất
                var token = "";
                do
                {
                    token = StringGenerator.Alphabet(30);
                } while (lstChangePass.FirstOrDefault(x => x.token == token).token != null);

                if (userChange.email != null)
                {
                    var timer = userChange.thoiGian.AddMinutes(30);
                    if(timer > DateTime.Now) return Json(new { tt = false, mess = "Không thể gửi yêu cầu liên tiếp trong 30 phút !" });

                    EmailController emailController = new EmailController();
                    var body = emailController.getRePass(user.Ten, currentURL + "/account/resetpasswourd/" + token, currentURL + "/support");

                    userChange.thoiGian = DateTime.Now;
                    userChange.token = token;

                    emailController.Send(userChange.email, "Thông báo bảo mật", body);
                    return Json(new { tt = true, mess = "Đã gửi mail cho bạn !" });
                }
                else
                {
                    EmailController emailController = new EmailController();
                    var body = emailController.getRePass(user.Ten, currentURL + "/account/resetpassword/" + token, currentURL + "/support");

                    lstChangePass.Add(new userChangePass { email = user.Email, thoiGian = DateTime.Now, token = token });

                    emailController.Send(user.Email, "Thông báo bảo mật", body);
                    return Json(new { tt = true, mess = "Đã gửi mail cho bạn !" });
                }
            }
        }

        //Trang làm mới mật khẩu
        [AllowAnonymous]
        [HttpGet]
        [Route("/account/resetpassword/{token?}")]
        public IActionResult ResetPassword(string token)
        {
            var user = lstChangePass.FirstOrDefault(x => x.token == token);
            if (string.IsNullOrEmpty(token) || user.token == null) return NotFound();

            var temptoken = user.thoiGian.AddMinutes(30);
            if (temptoken < DateTime.Now) return NotFound();

            return View(new { email = user.email });
        }

        //Thực hiện đổi mật khẩu
        [AllowAnonymous]
        [HttpPost]
        public IActionResult setResetPassword(string email, string pass)
        {
            var user = db.NguoiDungs.FirstOrDefault(x => x.Email == email);
            if(user != null)
            {
                user.MatKhau = user.mahoaMatKhau(pass);

                var uchange = lstChangePass.FirstOrDefault(x => x.email == email);
                lstChangePass.Remove(uchange);

                db.SaveChanges();

                return Json(new { tt = true });
            }
            return Json(new { tt = false });
        }

        //Thực hiện đổi mật khẩu với popup
        [Authorize]
        [HttpPost]
        public IActionResult changePass(string pass, string new_pass)
        {
            var tempPass = new NguoiDung().mahoaMatKhau(pass);
            var user = db.NguoiDungs.FirstOrDefault(x => x.MaNd == User.Claims.First().Value && x.MatKhau == tempPass);
            if (user != null)
            {
                user.MatKhau = user.mahoaMatKhau(new_pass);
                db.SaveChanges();

                return Json(new { tt = true });
            }
            return Json(new { tt = false, mess = "Mật khẩu không chính xác !" });
        }
    }
}
