using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace DayHocTrucTuyen.Areas.User.Controllers
{
    public class EmailController : Controller
    {
        struct Email
        {
            public string Address { get; set; }  
            public string Password { get; set; }
        }

        //Lấy email đã được cấu hình
        private Email getEmail()
        {
            var email = new Email();
            var builder = WebApplication.CreateBuilder();
            email.Address = builder.Configuration.GetValue<string>("Email:Address");
            email.Password = builder.Configuration.GetValue<string>("Email:Password");

            return email;
        }

        //Hàm gửi mail không chứa file
        public bool Send(string mailTo, string tieude, string noidung)
        {
            try
            {
                var email = getEmail();
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(email.Address);
                    mail.To.Add(mailTo);
                    mail.Subject = tieude;
                    mail.Body = noidung;
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential(email.Address, email.Password);
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Hàm gửi mail có chứa file
        public bool SendWithFile(string mailTo, string tieude, string noidung, string fileName)
        {
            try
            {
                var email = getEmail();

                //Sử dụng mailMessage để gửi
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(email.Address);
                    mail.To.Add(mailTo);
                    mail.Subject = tieude;
                    mail.Body = noidung;
                    mail.IsBodyHtml = true;

                    var path = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot\\Content\\filePost\\");
                    mail.Attachments.Add(new Attachment(path + fileName));

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential(email.Address, email.Password);
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
