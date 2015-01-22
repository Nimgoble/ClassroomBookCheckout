using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ClassroomBookCheckout.Models;
using ClassroomBookCheckout.DAL;

namespace ClassroomBookCheckout.Controllers
{
    public class BooksController : Controller
    {
		private ApplicationDbContext dbContext = new ApplicationDbContext();

	    protected override void Dispose(bool disposing) 
		{
		    if (disposing) 
			{
				dbContext.Dispose();
		    }

		    base.Dispose(disposing);
	    }

	    // GET: Books
        public ActionResult Index()
        {
            return View(dbContext.Books.OrderBy(x => x.Title).ToList());
        }

        // GET: Books/Details/5
        public ActionResult Details(int id) 
		{
	        Book book = dbContext.Books.Find(id);
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            return View(new Book());
        }

        // POST: Books/Create
        [HttpPost]
        public ActionResult Create(Book model)
        {
			if (!ModelState.IsValid)
				return View(model);

            try 
			{
	            dbContext.Books.Add(model);
	            dbContext.SaveChanges();

                return RedirectToAction("Details", new {Id = model.ID_Book});
            }
			catch (Exception ex) 
			{
				ModelState.AddModelError(String.Empty, ex);
				return View(model);
			}
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int id)
        {
			Book book = dbContext.Books.Find(id);
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        public ActionResult Edit(Book model)
        {
			if (!ModelState.IsValid)
				return View(model);

            try
            {
                dbContext.Books.AddOrUpdate(model);
	            dbContext.SaveChanges();
                return RedirectToAction("Details", new {Id = model.ID_Book});
            }
			catch (Exception ex) 
			{
				ModelState.AddModelError(String.Empty, ex);
				return View(model);
			}
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int id) 
		{
	        Book book = dbContext.Books.Find(id);
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost]
        public ActionResult Delete(Book model) 
		{
	        if (!ModelState.IsValid)
		        return View(model);

            try 
			{
	            dbContext.Books.Remove(model);
	            dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
			catch (Exception ex) 
			{
				ModelState.AddModelError(String.Empty, ex);
				return View(model);
			}
        }
    }
}
