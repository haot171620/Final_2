using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhuTungXeMay2019.Models;
using PhuTungXeMay2019.Controllers;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Transactions;

namespace BanHangXachTay.Tests.Controllers
{
    [TestClass]
    public class Admin_tableCUSTOMERsControllerTest
    {
        private string searchString;

        [TestMethod]
        public void TestIndex()
        {
            var db = new CsK23T2bEntities();
            var controller = new ContactController();
            var result = controller.Index(searchString);
            var view = result as ViewResult;

            Assert.IsNotNull(view);

            var model = view.Model as List<CONTACT>;

            var movies = from m in db.CONTACTs
                         select m;

            Assert.IsNotNull(movies);
            Assert.AreEqual(model, searchString);
        }

        [TestMethod]
        public void TestCreateGet()
        {
            var controller = new ContactController();
            var result = controller.Create();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void TestCreatePost()
        {
            var db = new CsK23T2bEntities();
            var model = new CONTACT
            {
                tenNguoidung = "Nguyen",
                tenContact = "123",
                sdt = 123456,
                noidungContact = "khong co"

            };
            var controller = new ContactController();

            using (var scope = new TransactionScope())
            {

                var result = controller.Create(model);


                var redirect = result as RedirectToRouteResult;

                Assert.IsNotNull(redirect);
                Assert.AreEqual("Index", redirect.RouteValues["action"]);

                var item = db.CONTACTs.Find(model.idContact);

                Assert.IsNotNull(item);
                Assert.AreEqual(model.tenNguoidung, item.tenNguoidung);
                Assert.AreEqual(model.tenContact, item.tenContact);
                Assert.AreEqual(model.sdt, item.sdt);
                Assert.AreEqual(model.noidungContact, item.noidungContact);



            }
        }


        [TestMethod]
        public void TestEditGet()
        {
            var db = new CsK23T2bEntities();
            var item = db.CONTACTs.First();
            var controller = new ContactController();
            var result = controller.Edit(0);

            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));

            result = controller.Edit(item.idContact);
            var view = result as ViewResult;

            Assert.IsNotNull(view);

            var model = view.Model as CONTACT;

            Assert.IsNotNull(model);

        }

        [TestMethod]
        public void TestEditPost()
        {
            var db = new CsK23T2bEntities();
            var model = new CONTACT
            {
                idContact = db.CONTACTs.AsNoTracking().First().idContact,
                tenContact = "789"
            };
            var controller = new ContactController();

            using (var scope = new TransactionScope())
            {
                var result = controller.Create(model);
                var view = result as ViewResult;


                controller = new ContactController();
                result = controller.Edit(model);

                var redirect = result as RedirectToRouteResult;

                Assert.IsNotNull(redirect);
                Assert.AreEqual("Index", redirect.RouteValues["action"]);

                var item = db.CONTACTs.Find(model.idContact);

                Assert.IsNotNull(item);
                Assert.AreEqual(model.tenContact, item.tenContact);

            }
        }

        [TestMethod]
        public void TestDelete()
        {
            var controller = new ContactController();
            var db = new CsK23T2bEntities();

            using (var scope = new TransactionScope())
            {
                var result = controller.Delete(0);

                Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
            }
        }



    }
}
