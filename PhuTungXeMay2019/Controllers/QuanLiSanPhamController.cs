using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhuTungXeMay2019.Models;
using System.Transactions;

namespace PhuTungXeMay2019.Controllers
{
    public class QuanLiSanPhamController : Controller
    {
        CsK23T2bEntities db = new CsK23T2bEntities();

        // GET: /QuanLiSanPham/
        public ActionResult Index(String searchString)
        {
            var model = db.SanPhams;
            var links = from l in db.SanPhams
                        select l;
            if (!String.IsNullOrEmpty(searchString))
            {
                links = links.Where(s => s.Tensp.Contains(searchString));
            }
 
 
            return View(model.ToList());
        }

        // GET: /QuanLiSanPham/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham model = db.SanPhams.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: /QuanLiSanPham/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /QuanLiSanPham/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SanPham model)
        {
            ValidateSanpham(model);
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {
                    db.SanPhams.Add(model);
                    db.SaveChanges();
                    var path = Server.MapPath("~/App_Data");
                    path = System.IO.Path.Combine(path, model.Idsp.ToString());
                    Request.Files["Image"].SaveAs(path);
                   
                    // accept all and persistence
                    scope.Complete();
                    return RedirectToAction("Index");
                }
               
            }

            return View("Create",model);
        }
        public ActionResult Image(string id)
        {
            var path = Server.MapPath("~/App_Data");
            path = System.IO.Path.Combine(path, id);
            return File(path, "image/*");
        }
        private void ValidateSanpham(SanPham model)
        {
            if (model.Giatien <= 0)
                ModelState.AddModelError("Gia", SanPhamError.PRICE_LESS_0);
           
        }
        

        // GET: /QuanLiSanPham/Edit/5
        public ActionResult Edit(int id)
        {
            var model = db.SanPhams.Find(id);
            if (model == null)
                return HttpNotFound();
            return View(model);
        }

        // POST: /QuanLiSanPham/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SanPham model)
        {
            ValidateSanpham(model);
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: /QuanLiSanPham/Delete/5
        public ActionResult Delete(int id)
        {
            var model = db.SanPhams.Find(id);
            if (model == null)
                return HttpNotFound();
            db.SanPhams.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: /QuanLiSanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanpham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanpham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Search(string Search)
        {
            var model = db.SanPhams.ToList().Where(x => x.Tensp.Contains(Search));
            return View(model);
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
