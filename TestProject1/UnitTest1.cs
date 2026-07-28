using System.Linq;
using FluentAssertions;
using LMS.Controllers;
using LMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace TestProject1
{
    public class BooksControllerTests
    {
        private LibraryContext CreateInMemoryContext(string name)
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(name)
                .Options;
            return new LibraryContext(options);
        }

        private void Seed(LibraryContext ctx)
        {
            ctx.Books.AddRange(
                new Book { BookId = 1, Title = "A", Author = "Author1", ISBN = "111-1111111111", PublishedDate = System.DateTime.UtcNow.AddYears(-5), IsAvailable = true },
                new Book { BookId = 2, Title = "B", Author = "Author2", ISBN = "222-2222222222", PublishedDate = System.DateTime.UtcNow.AddYears(-4), IsAvailable = true },
                new Book { BookId = 3, Title = "Clean Code", Author = "Robert C. Martin", ISBN = "978-0132350885", PublishedDate = System.DateTime.UtcNow.AddYears(-3), IsAvailable = true },
                new Book { BookId = 4, Title = "D", Author = "Author4", ISBN = "444-4444444444", PublishedDate = System.DateTime.UtcNow.AddYears(-2), IsAvailable = true },
                new Book { BookId = 5, Title = "E", Author = "Author5", ISBN = "555-5555555555", PublishedDate = System.DateTime.UtcNow.AddYears(-1), IsAvailable = true },
                new Book { BookId = 6, Title = "F", Author = "Author6", ISBN = "666-6666666666", PublishedDate = System.DateTime.UtcNow, IsAvailable = true }
            );
            ctx.SaveChanges();
        }

        [Fact]
        public async Task Index_Returns_Paged_Results()
        {
            var ctx = CreateInMemoryContext("test_index_paged");
            Seed(ctx);

            var controller = new BooksController(ctx);
            var result = await controller.Index(null, 1);

            result.Should().BeOfType<ViewResult>();
            var view = result as ViewResult;
            view!.Model.Should().BeAssignableTo<System.Collections.Generic.IEnumerable<Book>>();
            var model = ((System.Collections.Generic.IEnumerable<Book>)view.Model).ToList();
            model.Count.Should().Be(5);
        }

        [Fact]
        public async Task Index_Search_Filters_Results()
        {
            var ctx = CreateInMemoryContext("test_index_search");
            Seed(ctx);

            var controller = new BooksController(ctx);
            var result = await controller.Index("Clean Code", 1);

            result.Should().BeOfType<ViewResult>();
            var view = result as ViewResult;
            var model = ((System.Collections.Generic.IEnumerable<Book>)view!.Model).ToList();
            model.Should().ContainSingle(b => b.Title == "Clean Code");
        }

        [Fact]
        public async Task Details_Returns_NotFound_For_Invalid_Id()
        {
            var ctx = CreateInMemoryContext("test_details_notfound");
            Seed(ctx);

            var controller = new BooksController(ctx);
            var result = await controller.Details(999);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Details_Returns_View_For_Valid_Id()
        {
            var ctx = CreateInMemoryContext("test_details_valid");
            Seed(ctx);

            var controller = new BooksController(ctx);
            var result = await controller.Details(3);

            result.Should().BeOfType<ViewResult>();
            var view = result as ViewResult;
            var model = view!.Model as Book;
            model.Should().NotBeNull();
            model!.Title.Should().Be("Clean Code");
        }
    }
}
