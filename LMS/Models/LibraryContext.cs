using Microsoft.EntityFrameworkCore;

namespace LMS.Models
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    BookId = 1,
                    Title = "The Pragmatic Programmer",
                    Author = "Andrew Hunt and David Thomas",
                    ISBN = "978-0201616224",
                    PublishedDate = new DateTime(2021, 10, 30),
                    IsAvailable = true
                },
                new Book
                {
                    BookId = 2,
                    Title = "Design Pattern using C#",
                    Author = "Robert C. Martin",
                    ISBN = "978-0132350884",
                    PublishedDate = new DateTime(2023, 8, 1),
                    IsAvailable = true
                }
                ,
                new Book { BookId = 3, Title = "Clean Code", Author = "Robert C. Martin", ISBN = "978-0132350885", PublishedDate = new DateTime(2008,8,1), IsAvailable = true },
                new Book { BookId = 4, Title = "Refactoring", Author = "Martin Fowler", ISBN = "978-0201485677", PublishedDate = new DateTime(1999,7,8), IsAvailable = true },
                new Book { BookId = 5, Title = "C# in Depth", Author = "Jon Skeet", ISBN = "978-1617294532", PublishedDate = new DateTime(2019,3,23), IsAvailable = true },
                new Book { BookId = 6, Title = "Pro ASP.NET Core", Author = "Adam Freeman", ISBN = "978-1484203989", PublishedDate = new DateTime(2020,5,12), IsAvailable = true },
                new Book { BookId = 7, Title = "Domain-Driven Design", Author = "Eric Evans", ISBN = "978-0321125217", PublishedDate = new DateTime(2003,8,30), IsAvailable = false },
                new Book { BookId = 8, Title = "You Don't Know JS", Author = "Kyle Simpson", ISBN = "978-1491904244", PublishedDate = new DateTime(2015,2,10), IsAvailable = true },
                new Book { BookId = 9, Title = "Introduction to Algorithms", Author = "Cormen et al.", ISBN = "978-0262033848", PublishedDate = new DateTime(2009,7,31), IsAvailable = true },
                new Book { BookId = 10, Title = "The Clean Coder", Author = "Robert C. Martin", ISBN = "978-0137081073", PublishedDate = new DateTime(2011,5,13), IsAvailable = true },
                new Book { BookId = 11, Title = "Patterns of Enterprise Application Architecture", Author = "Martin Fowler", ISBN = "978-0321127426", PublishedDate = new DateTime(2002,11,5), IsAvailable = true },
                new Book { BookId = 12, Title = "Effective C#", Author = "Bill Wagner", ISBN = "978-0321245663", PublishedDate = new DateTime(2017,9,1), IsAvailable = true }
            );

            modelBuilder.Entity<StudentModel>().HasData(
                new StudentModel { StudentId = 1, StudentName = "Alice Johnson", Email = "alice.j@email.com", Phone = "555-0101" },
                new StudentModel { StudentId = 2, StudentName = "Bob Smith", Email = "bob.smith@email.com", Phone = "555-0102" }
            );

            modelBuilder.Entity<LibrarianModel>().HasData(
                new LibrarianModel { LibrarianId = 1, Name = "Sarah Connor", Age = 34, Phone = "555-0201" },
                new LibrarianModel { LibrarianId = 2, Name = "John Doe", Age = 28, Phone = "555-0202" }
            );

            modelBuilder.Entity<LoginModel>().HasData(
                new LoginModel { Id = 1, Username = "admin", Password = "12345" },
                new LoginModel { Id = 2, Username = "mycodingproject", Password = "myc546" }
            );
        }

        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<BorrowRecord> BorrowRecords { get; set; } = null!;
        public DbSet<StudentModel>? Students { get; set; }
        public DbSet<LibrarianModel>? Librarians { get; set; }
        public DbSet<LoginModel>? LoginTab { get; set; }
    }
}
