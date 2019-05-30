using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhuTungXeMay2019.Models;

namespace PhuTungXeMay2019.Controllers
{
    public class ShoppingCartController : Controller
    {
        CsK23T2bEntities db = new CsK23T2bEntities();
        public List<Cart1> GetCart()
        {
            List<Cart1> lstCart = Session["GioHang"] as List<Cart1>;
            if (lstCart == null)
            {
                //neu gio hang chua ton tai thi tien hanh dat hang
                lstCart = new List<Cart1>();
                Session["GioHang"] = lstCart;
            }
            return lstCart;
        }
        public ActionResult addShoppingCart(int Idsp, int txtSoLuong)
        {
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.Idsp == Idsp);

            if (txtSoLuong <= 0 || txtSoLuong.ToString().Trim().Equals(null))
            {
                txtSoLuong = 1;
            }

            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            // Lấy ra session giỏ hàng
            List<Cart1> lstCart = GetCart();
            // Kiểm tra sản phẩm này đã tồn tại trong session[giohang] chưa
            Cart1 sp = lstCart.Find(n => n.Idsp == Idsp);
            ViewBag.TX = txtSoLuong;
            if (sp == null)
            {
                    sp = new Cart1(Idsp);
                    sp.soluong = txtSoLuong;
                    lstCart.Add(sp);
                    return RedirectToAction("chitietsanpham", "Chitietsanpham", new { sanpham.Idsp });
            }
            else
            {              
                    sp.soluong = sp.soluong + txtSoLuong;

                    return RedirectToAction("chitietsanpham", "Chitietsanpham", new { sanpham.Idsp });
            }
        }
        public ActionResult DeleteCart(int idSp)
        {
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.Idsp == idSp);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;

            }
            // lay gio hang ra tu session
            List<Cart1> lstCart = GetCart();
            // Kiem tra san pham co ton tai tron session
            Cart1 sp = lstCart.SingleOrDefault(n => n.Idsp == idSp);
            // Neu ton tai thi cho sua so luong
            if (sp != null)
            {
                lstCart.RemoveAll(n => n.Idsp == idSp);
            }
            if (lstCart.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("ShoppingCart");
        }
        public ActionResult Cart()
        {


            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            List<Cart1> lstCart = GetCart();
            return View(lstCart);
        }
        // Tinh tong so luong va tong tien
        private int SumAmount()
        {
            int iSumAmount = 0;
            List<Cart1> lstCart = Session["GioHang"] as List<Cart1>;
            if (lstCart != null)
            {
                iSumAmount = lstCart.Sum(n => n.soluong);

            }
            return iSumAmount;
        }
        private double SumPrice()
        {
            double dSumPrice = 0;
            List<Cart1> lstCart = Session["GioHang"] as List<Cart1>;
            if (lstCart != null)
            {
                dSumPrice += lstCart.Sum(n => n.thanhTien);
            }
            return dSumPrice;
        }
         // Ham tạo cho giỏ hàng
      
        //
        // GET: /ShoppingCart/
        public ActionResult Index()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        public ActionResult ShoppingCart()
        {


            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            List<Cart1> lstCart = GetCart();
            return View(lstCart);
        }
        public ActionResult SoluongPartial()
        {
            if (SumAmount() == 0)
            {
                ViewBag.SumAmount = 0;
                ViewBag.TongSoLuong = 0;
                ViewBag.SumPrice = 0;
                return PartialView();
            }
            ViewBag.TongSoLuong = SumAmount();
            ViewBag.TongTien = SumPrice();
            return PartialView();
        }
	}

}