using Raven.Client;
using RavenBookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RavenBookstore.Controllers
{
    public class BooksController : RavenController
    {
        private const int PageSize = 10;

        public ActionResult ListAll(int page)
        {
            RavenQueryStatistics stats;

            var books = RavenSession.Query<Book>()
                .Statistics(out stats)
                .OrderByDescending(o => o.YearPublished)
                .Skip(PageSize * page)
                .Take(PageSize)
                .ToList();

            ViewBag.Title = stats.TotalResults + " books found";

            return View("List", books);
        }

        public ActionResult ListAll()
        {
            var books = RavenSession.Query<Book>().ToList();

            return View(books);
        }

        public ActionResult ListByYear(int year)
        {
            var books = RavenSession.Query<Book>()
                .Where(x => x.YearPublished == year)
                .ToList();

            return View("List", books);
        }

        public ActionResult ListByDepartment(string department)
        {
            var books = RavenSession.Query<Book>()
                .Where(x => x.Departments.Any(y => y.Equals(department)))
                .ToList();

            return View("List", books);
        }

        public ActionResult ListByPriceLimit(double price)
        {
            var books = RavenSession.Query<Book>()
                .Where(x => x.Price <= price)
                .ToList();

            return View("List", books);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new Book());
        }

        [HttpPost]
        public ActionResult Create(Book model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            RavenSession.Store(model);

            return Json("Book was successfully added and assigned the ID " + model.Id, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Show(int id)
        {
            var book = RavenSession.Load<Book>(id);

            if (book == null)
                return HttpNotFound("The requested book wasn't found in the system");

            return Json(book, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var book = RavenSession.Load<Book>(id);
            if (book == null)
                return HttpNotFound("The requested book wasn't found in the system");

            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(int id, Book model)
        {
            var book = RavenSession.Load<Book>(id);
            if (book == null)
                return HttpNotFound("The requested book wasn't found in the system");

            book.Title = model.Title;
            book.Author = model.Author;
            book.Description = model.Description;
            book.Price = model.Price;
            book.YearPublished = model.YearPublished;

            return Json("Book was edited successfully", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            var book = RavenSession.Load<Book>(id);
            if (book == null)
                return HttpNotFound("The requested book wasn't found in the system");

            RavenSession.Delete(book);

            return Json("Book was deleted successfully", JsonRequestBehavior.AllowGet);
        }
	}
}