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
    public class StudentsController : Controller
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

        // GET: Students
        public ActionResult Index()
        {
            return View(dbContext.Students.OrderBy(x => x.LastName).ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int id) 
		{
	        Student student = dbContext.Students.Find(id);
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View(new Student());
        }

        // POST: Students/Create
        [HttpPost]
        public ActionResult Create(Student model) 
		{
	        if (!ModelState.IsValid)
		        return View(model);

            try
            {
	            dbContext.Students.Add(model);
	            dbContext.SaveChanges();

                return RedirectToAction("Details", new {Id = model.ID_Student});
            }
            catch(Exception ex)
            {
				ModelState.AddModelError(String.Empty, ex);
                return View(model);
            }
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int id)
        {
			Student student = dbContext.Students.Find(id);
            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        public ActionResult Edit(Student model)
        {
            try
            {
                // TODO: Add update logic here
				dbContext.Students.AddOrUpdate(model);
	            dbContext.SaveChanges();
                return RedirectToAction("Details", new {Id = model.ID_Student});
            }
            catch(Exception ex)
            {
				ModelState.AddModelError(String.Empty, ex);
				return View(model);
            }
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int id)
        {
			Student student = dbContext.Students.Find(id);
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost]
        public ActionResult Delete(Student model)
        {
            try
            {
                // TODO: Add delete logic here
	            dbContext.Students.Remove(model);
	            dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
				ModelState.AddModelError(String.Empty, ex);
				return View(model);
            }
        }
    }
}
