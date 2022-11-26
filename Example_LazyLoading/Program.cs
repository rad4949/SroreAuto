using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Example_LazyLoading
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            CreateEmptyDB();
            AddData();
            LazyLoading();
        }

        public static void CreateEmptyDB()
        {
            using var dbContext = new ApplicationDbContext();

            dbContext.Database.EnsureDeleted();

            dbContext.Database.EnsureCreated();
        }

        public static void AddData()
        {
            using var dbContext = new ApplicationDbContext();

            var csharpCourse = new Course
            {
                Name = "C# Advanced",
                LessonsQuantity = 7
            };

            var efCoreCourse = new Course
            {
                Name = "Entity Framework Core Basic",
                LessonsQuantity = 10,
            };

            var johnSmithAvatar = new AuthorAvatar
            {
                AvatarUri = "http//john-smith/avatar"
            };
            var johnSmith = new Author
            {
                FirstName = "John",
                LastName = "Smith",
                Courses = new List<Course>(),
                Avatar = johnSmithAvatar
            };

            var arthurMorganAvatar = new AuthorAvatar
            {
                AvatarUri = "http//arthur-morgan/avatar"
            };
            var arthurMorgan = new Author
            {
                FirstName = "Arthur",
                LastName = "Morgan",
                Avatar = arthurMorganAvatar
            };

            johnSmith.Courses.Add(csharpCourse);
            johnSmith.Courses.Add(efCoreCourse);

            dbContext.Add(csharpCourse);
            dbContext.Add(efCoreCourse);
            dbContext.Add(johnSmith);
            dbContext.Add(arthurMorgan);

            dbContext.SaveChanges();
        }

        public static void LazyLoading()
        {
            using var dbContext = new ApplicationDbContext();

            var authors = dbContext.Authors.ToList();

            Console.WriteLine(new string('-', 80));

            foreach (var author in authors)
            {
                Console.WriteLine(new string('-', 80));
                Console.WriteLine("Author information:");
                Console.WriteLine(
                    $"Author: {author.FirstName + " " + author.LastName}. " +
                    $"Author avatar: {author.Avatar.AvatarUri}.");

                foreach (var course in author.Courses)
                {
                    Console.WriteLine($"Course Name: {course.Name}.");
                }

                Console.WriteLine(new string('-', 80));
            }
        }
    }

    public class ApplicationDbContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }

        public DbSet<Author> Authors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\MSSQLSERVER01;Database=Lazy;Trusted_Connection=True;")
                .UseLazyLoadingProxies();
        }
    }

    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int LessonsQuantity { get; set; }
        public virtual Author Author { get; set; }
    }

    public class Author
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        public virtual AuthorAvatar Avatar { get; set; }
    }

    public class AuthorAvatar
    {
        public int Id { get; set; }

        public string AvatarUri { get; set; }

        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
    }
}