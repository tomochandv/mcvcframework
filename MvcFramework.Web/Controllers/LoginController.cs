using MvcFramework.Logic;
using MvcFramework.Logic.Base;
using MvcFramework.Model;
using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace MvcFramework.Web.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect("/Home/Index");
            }
            FormsAuthentication.SignOut();
            return View();
        }

        public ActionResult Test()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);
            ViewBag.E = new Security().Encription("1111");
            ViewBag.D = new Security().Description(ViewBag.E);
            return View();
        }

        [HttpPost]
        public JsonResult Login(string id = "", string pwd = "")
        {
            Model_User_Info info = new Loc_UserInfo().GetUserInfo(DesMpSecurity(id), DesMpSecurity(pwd));
            bool result = info.EMP_REF_ID == 0 ? false : true;
            if(result)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string data = serializer.Serialize(info);
                FormsAuthenticationTicket newticket = new FormsAuthenticationTicket(1,
                                                                  "MpMvc",
                                                                  DateTime.Now,
                                                                  DateTime.Now.AddDays(1),
                                                                  false, // always persistent
                                                                  data,
                                                                  FormsAuthentication.FormsCookiePath);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(newticket));
                cookie.Expires = newticket.Expiration;
                Response.Cookies.Add(cookie);
                FormsAuthentication.GetAuthCookie("MpMvc", true);
            }
            return Json(result);
        }

        public RedirectResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Login");
        }

        private string DesMpSecurity(string encText)
        {
            string returnText = string.Empty;
            string firstDecText = System.Text.Encoding.GetEncoding("UTF-8").GetString(Convert.FromBase64String(encText));
            string secDexText = firstDecText.Replace("/", "+").Replace("=", "+");
            secDexText = Regex.Replace(secDexText, @"[a-z]", "+");
            string[] onebyoneText = secDexText.Split('+');
            foreach (string text in onebyoneText)
            {
                returnText += Convert.ToChar(int.Parse(text));
            }
            return returnText;
        }
    }
}
