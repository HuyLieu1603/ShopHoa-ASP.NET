using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopHoa.Models;

namespace ShopHoa.Areas.Admin.Controllers.Home
{
    public class HomeController : Controller
    {
        private ShopHoaEntities db = new ShopHoaEntities();

        // GET: Admin/Home
        public ActionResult Index()
        {
            var staff = db.Staff.Include(s => s.Role);
            return View(staff.ToList());
        }

        // GET: Admin/Home/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staff.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // GET: Admin/Home/Create
        public ActionResult Create()
        {
            ViewBag.MaCV = new SelectList(db.Role, "MaCV", "TenCV");
            return View();
        }

        // POST: Admin/Home/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNV,HoTenNV,NgaySinh,DiaChi,Email,MatKhau,CCCD,MaCV")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                staff.MaNV = Guid.NewGuid();
                db.Staff.Add(staff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaCV = new SelectList(db.Role, "MaCV", "TenCV", staff.MaCV);
            return View(staff);
        }

        // GET: Admin/Home/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staff.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaCV = new SelectList(db.Role, "MaCV", "TenCV", staff.MaCV);
            return View(staff);
        }

        // POST: Admin/Home/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNV,HoTenNV,NgaySinh,DiaChi,Email,MatKhau,CCCD,MaCV")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaCV = new SelectList(db.Role, "MaCV", "TenCV", staff.MaCV);
            return View(staff);
        }

        // GET: Admin/Home/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staff.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // POST: Admin/Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Staff staff = db.Staff.Find(id);
            db.Staff.Remove(staff);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
