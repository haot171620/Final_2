using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhuTungXeMay2019.Models;
namespace PhuTungXeMay2019.Models
{
    public class Cart1
    {
        CsK23T2bEntities db = new CsK23T2bEntities();
        public string image { get; set; }
        public int Idsp { get; set; }
        public string tenSp { get; set; }
        public int dongia { get; set; }
        public int soluong { get; set; }
        public int tonkho { get; set; }
        public int Quantity { get; set; }
        public string loaiSp { get; set; }
        public int loaiSP { get; set; }
        public double thanhTien
        {
            get { return soluong * dongia; }

        }
                // Ham tạo cho giỏ hàng
        public Cart1(int Idsp)
        {
            // TODO: Complete member initialization
            this.Idsp = Idsp;
            SanPham sp = db.SanPhams.Single(n => n.Idsp == Idsp);
            tenSp = sp.Tensp;
            soluong = 1;
            dongia = Convert.ToInt32( sp.Giatien);
            loaiSp = sp.Loaisp;
        }
    }
}