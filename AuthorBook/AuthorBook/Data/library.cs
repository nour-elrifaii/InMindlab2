using AuthorBook.models;
using Microsoft.EntityFrameworkCore;
using AuthorBook.Models;
using Microsoft.AspNetCore.Mvc;
namespace AuthorBook.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

    }
}