﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using GummiBearKingdom.Models;
using GummiBearKingdom.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Moq;
using System.Linq;

namespace GummiBearTests
{
    [TestClass]
    public class ProductsControllerTests
    {
        Mock<IProductRepository> mock = new Mock<IProductRepository>();
        EFProductRepository db = new EFProductRepository(new TestDbContext());

        private void DbSetUp()
        {
            mock.Setup(m => m.Products).Returns(new Product[] 
            {
                new Product {ProductId = 1, Description = "Gummi Bears!", Name = "Gummi Bears", Price = "0.17 / oz", imageUrl = "test url"},
                new Product {ProductId = 1, Description = "Gummi Worms!", Name = "Gummi Worms", Price = "0.17 / oz", imageUrl = "test url"},
                new Product {ProductId = 1, Description = "Gummi Fruit!", Name = "Gummi Fruit", Price = "0.17 / oz", imageUrl = "test url"}
            }.AsQueryable());
        }

        [TestMethod]
        public void  Mock_GetViewResultIndex_ActionResult()
        {
            DbSetUp();
            ProductsController controller = new ProductsController(mock.Object);

            var result = controller.Index();

            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        [TestMethod]
        public void Mock_IndexContainsModelData_List()
        {
            DbSetUp();
            ViewResult indexView = new ProductsController(mock.Object).Index() as ViewResult;

            var result = indexView.ViewData.Model;

            Assert.IsInstanceOfType(result, typeof(List<Product>));
        }
        [TestMethod]
        public void Mock_IndexContainsItems_Collection()
        {
            DbSetUp();
            ProductsController controller = new ProductsController(mock.Object);
            Product product = new Product { ProductId = 1, Description = "Gummi Bears!", Name = "Gummi Bears", Price = "0.17 / oz", imageUrl = "test url" };

            ViewResult indexView = controller.Index() as ViewResult;
            List<Product> collection = indexView.ViewData.Model as List<Product>;

            CollectionAssert.Contains(collection, product);
        }
        [TestMethod]
        public void Mock_GetDetails_ReturnsView()
        {
            Product product = new Product { ProductId = 1, Description = "Gummi Bears!", Name = "Gummi Bears", Price = "0.17 / oz", imageUrl = "test url" };
            DbSetUp();
            ProductsController controller = new ProductsController(mock.Object);

            var resultView = controller.Details(product.ProductId) as ViewResult;
            var model = resultView.ViewData.Model as Product;

            Assert.IsInstanceOfType(resultView, typeof(ViewResult));
            Assert.IsInstanceOfType(model, typeof(Product));
        }
        [TestMethod]
        public void testDb_Create_AddsToDb()
        {
            ProductsController controller = new ProductsController(db);
            Product testProduct = new Product { ProductId = 1, Description = "Gummi Bears!", Name = "Gummi Bears", Price = "0.17 / oz", imageUrl = "test url" };

            controller.Create(testProduct);
            var collection = (controller.Index() as ViewResult).ViewData.Model as List<Product>;

            CollectionAssert.Contains(collection, testProduct);
            db.RemoveAll();
        }
        [TestMethod]
        public void testDb_Delete_RemovesToDb()
        {
            ProductsController controller = new ProductsController(db);
            Product testProduct = new Product { ProductId = 1, Description = "Gummi Bears!", Name = "Gummi Bears", Price = "0.17 / oz", imageUrl = "test url" };

            controller.Create(testProduct);
            controller.DeleteConfirmed(testProduct.ProductId);
            var collection = (controller.Index() as ViewResult).ViewData.Model as List<Product>;

            CollectionAssert.DoesNotContain(collection, testProduct);
            db.RemoveAll();
        }
        [TestMethod]
        public void testDb_Edit_UpdatesInDb()
        {
            ProductsController controller = new ProductsController(db);
            Product testProduct = new Product { ProductId = 1, Description = "Gummi Bears!", Name = "Gummi Bears", Price = "0.17 / oz", imageUrl = "test url" };
            Product updatedProduct = new Product { ProductId = 1, Description = "Gummi Childen!", Name = "Gummi Children", Price = "0.20 / oz", imageUrl = "a different test url" };

            controller.Create(testProduct);
            testProduct.Name = "Gummi Children";
            controller.Edit(testProduct);
            var returnedProduct = (controller.Details(1) as ViewResult).ViewData.Model as Product;

            Assert.AreEqual(returnedProduct.Name, "Gummi Children");
            db.RemoveAll();
        }
    }
}
