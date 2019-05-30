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
    public class DonHangController : Controller
    {
        CsK23T2bEntities db = new CsK23T2bEntities();

        // GET: /DonHang/
        public List<Cart1> GetCart()
        {
            List<Cart1> lstcart = Session["GioHang"] as List<Cart1>;
            if (lstcart == null)
            {
                // Neu gio hang chua ton tai thi minh tien hang tao gio hang
                lstcart = new List<Cart1>();
                Session["GioHang"] = lstcart;
            }
            return lstcart;
        }
        public ActionResult Index()
        {
            var model = db.Donhangs;
            return View(model.ToList());
        }

        // GET: /DonHang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donhang model = db.Donhangs.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: /DonHang/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /DonHang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create(Dathang Dathang)
        {
            if (ModelState.IsValid)
            {
                List<Cart1> cart = GetCart();
                if (cart == null)
                {
                    ModelState.AddModelError("Order_ID", Resource1.nullCart);

                }
                else
                {
                    Dathang.Ngaymua = DateTime.Now;
                    db.Dathangs.Add(Dathang);


                    foreach (var item in cart)
                    {
                        Donhang Donhang = new Donhang();
                        Donhang.IdDonhang = Dathang.Iddonhang;
                        Donhang.Gia = Convert.ToInt32(item.thanhTien);
                        db.Donhangs.Add(Donhang);
                        Donhang.Tongtien += Convert.ToInt32(item.thanhTien);
                        db.Donhangs.Add(Donhang);
                        SanPham product = db.SanPhams.Find(item.Idsp);
                        db.Entry(product).State = EntityState.Modified;
                    }
                    db.SaveChanges();
                    Session["GioHang"] = null;

                    return RedirectToAction("Index", "Home");
                }

            }


            return View("~/Views/CheckOut/Index.cshtml");
        }
        

        // GET: /DonHang/Edit/5
        public ActionResult Edit(int id)
        {
            var model = db.Donhangs.Find(id);
            if (model == null)
                return HttpNotFound();
            return View(model);
        }
        private void ValidateDonhang(Donhang model)
        {
            if (model.Gia <= 0)
                ModelState.AddModelError("Price", SanPhamError.PRICE_LESS_0);
        }


        // POST: /DonHang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Donhang model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: /DonHang/Delete/5
        public ActionResult Delete(int id)
        {
            var model = db.Donhangs.Find(id);
            if (model == null)
                return HttpNotFound();
            db.Donhangs.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // POST: /DonHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Donhang donhang = db.Donhangs.Find(id);
            db.Donhangs.Remove(donhang);
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
