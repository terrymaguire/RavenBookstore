using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raven.Client.Embedded;
using RavenBookstore.Models;
using RavenBookstore.Controllers;
using System.Web.Mvc;
using System.Collections.Generic;

namespace RavenBookstore.Tests.Controllers
{
    [TestClass]
    public class BooksControllerTests
    {
        [TestMethod]
        public void ReturnBooksByPriceLimit()
        {
            using (var docStore = new EmbeddableDocumentStore { RunInMemory = true}
                .Initialize())
            {
                using (var session = docStore.OpenSession())
                {
                    session.Store(new Book
                    {
                        Title = "Test book",
                        YearPublished = 2013,
                        Price = 12.99
                    });

                    session.SaveChanges();
                }

                var controller = new BooksController { RavenSession = docStore.OpenSession() };

                var viewResult = (ViewResult)controller.ListByPriceLimit(15);
                var result = viewResult.ViewData.Model as List<Book>;
                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.Count);

                viewResult = (ViewResult)controller.ListByPriceLimit(10);
                result = viewResult.ViewData.Model as List<Book>;
                Assert.IsNotNull(result);
                Assert.AreEqual(0, result.Count);

                controller.RavenSession.Dispose();
            }
        }
    }
}
