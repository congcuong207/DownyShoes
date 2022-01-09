using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DownyShoes.Models;

namespace DownyShoes.Areas.Admin.Controllers
{
    public class LoaiGiaysController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Admin/LoaiGiays
        public ActionResult Index(string Timkiem)
        {
            
            if (!string.IsNullOrEmpty(Timkiem))
            {
                 var model=db.LoaiGiays.ToList().Where(x => x.NAME.ToUpper().Contains(Timkiem.ToUpper()));
                 return View(model);
            }
            return View(db.LoaiGiays.ToList());
        }

        // GET: Admin/LoaiGiays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiGiay loaiGiay = db.LoaiGiays.Find(id);
            if (loaiGiay == null)
            {
                return HttpNotFound();
            }
            return View(loaiGiay);
        }

        // GET: Admin/LoaiGiays/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LoaiGiays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NAME")] LoaiGiay loaiGiay)
        {
            if (ModelState.IsValid)
            {
                db.LoaiGiays.Add(loaiGiay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiGiay);
        }

        // GET: Admin/LoaiGiays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiGiay loaiGiay = db.LoaiGiays.Find(id);
            if (loaiGiay == null)
            {
                return HttpNotFound();
            }
            return View(loaiGiay);
        }

        // POST: Admin/LoaiGiays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NAME")] LoaiGiay loaiGiay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiGiay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiGiay);
        }

        // GET: Admin/LoaiGiays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiGiay loaiGiay = db.LoaiGiays.Find(id);
            if (loaiGiay == null)
            {
                return HttpNotFound();
            }
            return View(loaiGiay);
        }

        // POST: Admin/LoaiGiays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoaiGiay loaiGiay = db.LoaiGiays.Find(id);
            db.LoaiGiays.Remove(loaiGiay);
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
