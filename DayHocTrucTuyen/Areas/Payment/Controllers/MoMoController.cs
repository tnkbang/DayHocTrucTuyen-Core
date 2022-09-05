using DayHocTrucTuyen.Areas.Payment.Models;
using DayHocTrucTuyen.Models.Entities;
using DayHocTrucTuyen.Models.Payment.Momo;
using DayHocTrucTuyen.Models.Payment.MoMo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace DayHocTrucTuyen.Areas.Payment.Controllers
{
    [Area(nameof(Payment))]
    [Route("Payment/[controller]/[action]")]
    [Authorize]

    public class MoMoController : Controller
    {
        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();
        static List<UserPayment> userPayments = new List<UserPayment>();

        public IActionResult Pay(string pak, string money)
        {
            //request params need to request to MoMo system
            string endpoint = "https://test-payment.momo.vn/v2/gateway/api/create";
            string partnerCode = "MOMOBW8K20220903";
            string accessKey = "jjuT3W0pPODOW7Cg";
            string serectkey = "dJTcUi9ROvZGCakIJuFzQfBK69QaHJKh";
            string orderInfo = pak;

            string redirectUrl = "https://localhost:44354/Profile/Info?id=" + User.Claims.First().Value;
            string ipnUrl = "https://cead-2402-800-63b5-a749-b0f8-561e-a288-b0e8.ap.ngrok.io/Payment/MoMo/SavePay";
            string requestType = "captureWallet";

            string amount = money;
            string orderId = Guid.NewGuid().ToString();
            string requestId = Guid.NewGuid().ToString();
            string extraData = "";

            //Before sign HMAC SHA256 signature
            string rawHash = "accessKey=" + accessKey +
                "&amount=" + amount +
                "&extraData=" + extraData +
                "&ipnUrl=" + ipnUrl +
                "&orderId=" + orderId +
                "&orderInfo=" + orderInfo +
                "&partnerCode=" + partnerCode +
                "&redirectUrl=" + redirectUrl +
                "&requestId=" + requestId +
                "&requestType=" + requestType
                ;

            MoMoSecurity crypto = new MoMoSecurity();
            //sign signature SHA256
            string signature = crypto.signSHA256(rawHash, serectkey);

            //build body json request
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "partnerName", "Test" },
                { "storeId", "MomoTestStore" },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderId },
                { "orderInfo", orderInfo },
                { "redirectUrl", redirectUrl },
                { "ipnUrl", ipnUrl },
                { "lang", "en" },
                { "extraData", extraData },
                { "requestType", requestType },
                { "signature", signature }

            };

            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());
            JObject jmessage = JObject.Parse(responseFromMomo);

            //Thêm người dùng đang thực hiện giao dịch vào danh sách để tiến hành lưu csdl khi thành công
            userPayments.Add(new UserPayment
            {
                id = orderId,
                maNd = User.Claims.First().Value,
                money = double.Parse(money)
            });

            return Redirect(jmessage.GetValue("payUrl").ToString());
        }

        [HttpPost]
        [AllowAnonymous]
        public void SavePay([FromBody] object requestData)
        {
            JObject jmessage = JsonConvert.DeserializeObject<dynamic>(requestData.ToString());

            //Lấy người dùng đang thực hiện giao dịch
            var user = userPayments.FirstOrDefault(x => x.id.Equals(jmessage.GetValue("orderId").ToString()));

            //Nếu giao dịch thành công
            if (jmessage.GetValue("resultCode").ToString().Equals("0"))
            {
                var pak = db.GoiNangCaps.FirstOrDefault(x => x.GiaTien == user.money);

                if (pak != null)
                {
                    var upgrade = db.TrangThaiNangCaps.FirstOrDefault(x => x.MaNd == user.maNd);

                    if (upgrade == null)
                    {
                        TrangThaiNangCap newUG = new TrangThaiNangCap();
                        newUG.MaNd = user.maNd;
                        newUG.NgayDangKy = DateTime.Now;
                        newUG.MaGoi = pak.MaGoi;

                        db.TrangThaiNangCaps.Add(newUG);
                    }
                    else
                    {
                        upgrade.MaGoi = pak.MaGoi;
                        upgrade.NgayDangKy = DateTime.Now;
                    }

                    db.SaveChanges();
                }
            }

            userPayments.Remove(user);

            base.Response.StatusCode = (int)HttpStatusCode.NoContent;
        }
    }
}
