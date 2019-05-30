//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using PhuTungXeMay2019.Tests.Controllers;
//using PhuTungXeMay2019.Controllers;
//using PhuTungXeMay2019.Models;
//using System.Transactions;
//using System.Linq;
//using System.Web.Mvc;
//using System.Collections.Generic;

//namespace PhuTungXeMay2019.Tests.Controllers
//{
//    [TestClass]
//    public class QuanLiSanPham
//    {
//        [TestMethod]
//        public void TestIndex()
//        {
//            private string searchString;
//            var db = new CsK23T2bEntities();
//            var controller = new QuanLiSanPhamController();

//            var result = controller.Index(searchString);
//            var view = result as ViewResult;
//            Assert.IsNotNull(view);
//            var model = view.Model as List<SanPham>;
//            Assert.IsNotNull(model);
//            Assert.AreEqual(db.SanPhams.Count(), model.Count);
//        }
//    }
//}
