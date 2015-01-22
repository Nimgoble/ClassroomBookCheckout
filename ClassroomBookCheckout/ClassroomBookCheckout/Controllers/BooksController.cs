using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ClassroomBookCheckout.ViewModels;
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

		#region Basic CRUD operations
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
		#endregion

		[HttpGet]
		public ActionResult CheckOut(Int32 studentId) 
		{
			return View(new CheckOutBookViewModel { StudentID = studentId });
		}

		[HttpPost]
		public ActionResult CheckOut(CheckOutBookViewModel model) 
		{
			if (!ModelState.IsValid)
				return View(model);

			Book book = dbContext.Books.SingleOrDefault(x => x.ISBN == model.ISBN);
			//If we didn't find the book, as them if they want to create a new one.
			if (book == null) 
			{
				return RedirectToAction("CreateForCheckOut", new { studentId = model.StudentID, ISBN = model.ISBN });
			}

			Student student = dbContext.Students.Find(model.StudentID);
			if (student == null) 
			{
				//Do error handling here
			}

			//Does this student already have this book checked out?
			if (student.Books.Contains(book)) 
			{
				ModelState.AddModelError(String.Empty, String.Format("Student has already checked out book with ISBN '{0}'", model.ISBN));
				return View(model);
			}

			//Check out the book
			student.Books.Add(book);
			dbContext.SaveChanges();

			return RedirectToAction("Details", "Students", new { Id = model.StudentID });
		}

		[HttpGet]
		public ActionResult CreateForCheckOut(Int32 studentId, String ISBN) 
		{
			return View(new CreateBookForCheckOutViewModel { ISBN = ISBN, StudentID = studentId });
		}

		[HttpPost]
		public ActionResult CreateForCheckOut(CreateBookForCheckOutViewModel model) 
		{
			if (!ModelState.IsValid)
				return View(model);

			//Find the student
			Student student = dbContext.Students.Find(model.StudentID);
			if (student == null) 
			{
				//TODO: error handling here	
				return RedirectToAction("Index");
			}

			//Create the book
			Book book = new Book() 
			{
				Title = model.Title,
				ISBN = model.ISBN
			};

			dbContext.Books.Add(book);
			dbContext.SaveChanges();

			//Add the book to the list of the student's checked out books
			student.Books.Add(book);
			dbContext.SaveChanges();

			//Return to the student's profile
			return RedirectToAction("Details", "Students", new { Id = model.StudentID });
		}
    }
}
