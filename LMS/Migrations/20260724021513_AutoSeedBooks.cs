using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LMS.Migrations
{
    /// <inheritdoc />
    public partial class AutoSeedBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Author", "ISBN", "IsAvailable", "PublishedDate", "Title" },
                values: new object[,]
                {
                    { 3, "Robert C. Martin", "978-0132350885", true, new DateTime(2008, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Clean Code" },
                    { 4, "Martin Fowler", "978-0201485677", true, new DateTime(1999, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Refactoring" },
                    { 5, "Jon Skeet", "978-1617294532", true, new DateTime(2019, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "C# in Depth" },
                    { 6, "Adam Freeman", "978-1484203989", true, new DateTime(2020, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pro ASP.NET Core" },
                    { 7, "Eric Evans", "978-0321125217", false, new DateTime(2003, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Domain-Driven Design" },
                    { 8, "Kyle Simpson", "978-1491904244", true, new DateTime(2015, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "You Don't Know JS" },
                    { 9, "Cormen et al.", "978-0262033848", true, new DateTime(2009, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Introduction to Algorithms" },
                    { 10, "Robert C. Martin", "978-0137081073", true, new DateTime(2011, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Clean Coder" },
                    { 11, "Martin Fowler", "978-0321127426", true, new DateTime(2002, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Patterns of Enterprise Application Architecture" },
                    { 12, "Bill Wagner", "978-0321245663", true, new DateTime(2017, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Effective C#" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 12);
        }
    }
}
