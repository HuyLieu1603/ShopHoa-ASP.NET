using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopHoa.Models;

namespace ShopHoa.Controllers.Auth
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
        public ActionResult Login(string email,string password)
        {
            var checkLogin= db.Customer.Where(cus=>cus.Email == email&&cus.MatKhau==password).FirstOrDefault();
            if (checkLogin==null)
            {
                ViewBag.Error = "Sai tài khoản hoặc mật khẩu!";
                return View();
            }
            return View();
        }

        public ActionResult Logout()
        {
            return View();
        }
    }
}