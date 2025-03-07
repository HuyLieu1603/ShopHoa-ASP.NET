using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopHoa.Models;

namespace ShopHoa.Areas.Admin.Controllers.ManageAccount
{
    public class AccountController : Controller
    {
        private ShopHoaEntities db = new ShopHoaEntities();
        // GET: Admin/Account
        public ActionResult Index(Guid? id)
        {
            var user = db.Staff.Where(u => u.MaNV == id).FirstOrDefault();
            if (user == null)
            {
                ViewBag.Error = "Người dùng không tồn tại";
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }
        [HttpGet]
        public ActionResult EditInformation(Guid id)
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditInformation([Bind(Include = "MaNV,HoTenNV,NgaySinh,DiaChi,Email,MatKhau,CCCD,MaCV")] Staff staff)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Có lỗi xảy ra! Mời nhập lại";
                return View(staff);
            }
            ViewBag.Success = "Chỉnh sửa thông tin thành công!";
            return RedirectToAction("Index","Home");
        }
        public ActionResult ChangePassword(Guid id)
        {
            var user = db.Staff.Where(u=>u.MaNV == id).FirstOrDefault();
            return View(user);
        }
        [HttpPost]
        public ActionResult ChangePassword(string oldPassword,string newPassword)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Vui lòng thử lại";
                return View();
            }
            return View();
        }

        //get user by email
        public Staff GetUserByEmail(string email)
        {
            var user = db.Staff.Where(e => e.Email == email).FirstOrDefault();
            return user;
        }
    }
}