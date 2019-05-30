using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhuTungXeMay2019.Models;
namespace PhuTungXeMay2019.Controllers
{
    public class ChitietsanphamController : Controller
    {
        CsK23T2bEntities db = new CsK23T2bEntities();
        //
        // GET: /Chitietsanpham/
        public ActionResult Index()
        {
            return View(db.SanPhams.ToList());
        }
        public ViewResult chitietsanpham(int Idsp = 0)
        {
            // Tra về đôi tượng với điều kiện
            SanPham product = db.SanPhams.SingleOrDefault(n => n.Idsp == Idsp);
            if (product == null)
            {
                // Trả về trang báo lỗi
                Response.StatusCode = 404;
                return null;
            }
            return View(product);
        }
        
	}
}