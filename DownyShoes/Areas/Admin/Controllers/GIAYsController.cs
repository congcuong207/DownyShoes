using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DownyShoes.Models;
using PagedList;

namespace DownyShoes.Areas.Admin.Controllers
{
    public class GIAYsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Admin/GIAYs
        public ActionResult Index(string Timkiem, int? page)
        {
            // 1. Tham số int? dùng để thể hiện null và kiểu int
            // page có thể có giá trị là null và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo LinkID mới có thể phân trang.
            var links = (from l in db.GIAYs
                         select l).OrderBy(x => x.ID);

            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            int pageSize = 6;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);
            if (!string.IsNullOrEmpty(Timkiem))
            {
                var model = links.Where(x => x.NAME.Contains(Timkiem)).ToPagedList(pageNumber, pageSize);
                
                return View(model);
            }
            return View(links.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/GIAYs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIAY gIAY = db.GIAYs.Find(id);
            if (gIAY == null)
            {
                return HttpNotFound();
            }
            return View(gIAY);
        }

        // GET: Admin/GIAYs/Create
        public ActionResult Create()
        {
            ViewBag.IDLOAIGIAY = new SelectList(db.LoaiGiays, "ID", "NAME");
            return View();
        }

        // POST: Admin/GIAYs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NAME,COST,IMAGE,DETAILS,IDLOAIGIAY")] GIAY gIAY)
        {
            if (ModelState.IsValid)
            {
                db.GIAYs.Add(gIAY);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDLOAIGIAY = new SelectList(db.LoaiGiays, "ID", "NAME", gIAY.IDLOAIGIAY);
            return View(gIAY);
        }

        // GET: Admin/GIAYs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIAY gIAY = db.GIAYs.Find(id);
            if (gIAY == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDLOAIGIAY = new SelectList(db.LoaiGiays, "ID", "NAME", gIAY.IDLOAIGIAY);
            return View(gIAY);
        }

        // POST: Admin/GIAYs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NAME,COST,IMAGE,DETAILS,IDLOAIGIAY")] GIAY gIAY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gIAY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDLOAIGIAY = new SelectList(db.LoaiGiays, "ID", "NAME", gIAY.IDLOAIGIAY);
            return View(gIAY);
        }

        // GET: Admin/GIAYs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIAY gIAY = db.GIAYs.Find(id);
            if (gIAY == null)
            {
                return HttpNotFound();
            }
            return View(gIAY);
        }

        // POST: Admin/GIAYs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GIAY gIAY = db.GIAYs.Find(id);
            db.GIAYs.Remove(gIAY);
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
