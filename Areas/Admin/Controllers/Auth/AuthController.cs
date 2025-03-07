using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopHoa.Models;
using ShopHoa.Helpers;
using ShopHoa.Models.Auth.Customer;
using Newtonsoft.Json;

namespace ShopHoa.Areas.Admin.Controllers.Auth
{
    public class AuthController : Controller
    {
        private readonly ShopHoaEntities db = new ShopHoaEntities();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel user)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Error = "Dữ liệu không hợp lệ";
                return View(user);
            }
            var checkLogin= db.Staff.Where(cus=>cus.Email == user.Email&&cus.MatKhau==user.MatKhau).FirstOrDefault();
            if (checkLogin==null)
            {
                ViewBag.Error = "Sai tài khoản hoặc mật khẩu!";
                return View(user);
            }
            var token = JwtHelper.GenerateToken(user.Email);
            //Lưu thông tin vào session
            var userFull= db.Staff.Where(u=>u.Email == user.Email).FirstOrDefault();
            var userDataJson = JsonConvert.SerializeObject(userFull, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            Session["User"] = userDataJson;
            //Lưu token vào Cookie
            HttpCookie authCookie = new HttpCookie("AuthToken", token)
            {
                   HttpOnly = true,
                   Secure = true,
                   Expires= DateTime.Now.AddHours(1)
            };
            Response.Cookies.Add(authCookie);

            return RedirectToAction("Index", "Home");
        }

        //LOG OUT
        public ActionResult Logout()
        {
            if (Request.Cookies["AuthToken"]!=null)
            {
                HttpCookie authCookie = new HttpCookie("AuthToken")
                {
                    Expires = DateTime.Now.AddHours(-1)
                };
                Response.Cookies.Add(authCookie);
            }
            Session["User"]= null;
            return RedirectToAction("Login");
        }
    }
}