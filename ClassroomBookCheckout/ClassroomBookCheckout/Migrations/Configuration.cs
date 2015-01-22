namespace ClassroomBookCheckout.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;
    using ClassroomBookCheckout.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ClassroomBookCheckout.DAL.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ClassroomBookCheckout.DAL.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var students = new List<Student>()
            {
                new Student
                {
                    FirstName = "John",
                    LastName = "Smith"
                },
                new Student
                {
                    FirstName = "Jane",
                    LastName = "Doe"
                },
                new Student
                {
                    FirstName = "Matt",
                    LastName = "Door"
                },
                new Student
                {
                    FirstName = "Rachel",
                    LastName = "Ray"
                }
            };

            students.ForEach(s => context.Students.AddOrUpdate(p => new { p.FirstName, p.LastName }, s));

            var books = new List<Book>
            {
                new Book
                {
                    ISBN = "978-0140268867",
                    Title = "The Odyssey"
                },
                new Book
                {
                    ISBN = "978-0321716811",
                    Title = "College Algebra (9th Edition)"
                },
                new Book
                {
                    ISBN = "978-0201616224",
                    Title = "The Pragmatic Programmer: From Journeyman to Master"
                },
                new Book
                {
                    ISBN = "978-0321733177",
                    Title = "College Physics (9th Edition)"
                }
            };

            books.ForEach(b => context.Books.AddOrUpdate(q => q.ISBN, b));
        }
    }
}
