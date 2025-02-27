using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopHoa.Models;
using ShopHoa.Helpers;
using ShopHoa.Models.Auth.Customer;

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
            //Lưu token vào Cookie
            HttpCookie authCookie = new HttpCookie("JwtToken", token);
            authCookie.Expires = DateTime.Now.AddMinutes(60);
            Response.Cookies.Add(authCookie);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            return View();
        }
    }
}