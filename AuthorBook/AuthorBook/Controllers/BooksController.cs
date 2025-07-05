using AuthorBook.Data;
using AuthorBook.models;
using AuthorBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthorBook.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly LibraryContext _context;

    public BooksController(LibraryContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
        return await _context.Books.Include(b => b.Author).ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Book>> AddBook(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBooks), new { id = book.BookId }, book);
    }

    [HttpPost("AddAuthor")]
    public async Task<ActionResult<Author>> AddAuthor(Author author)
    {
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(AddAuthor), new { id = author.AuthorId }, author);
    }

    [HttpGet("GetBookByYear")]
    public async Task<ActionResult<IEnumerable<Book>>> GetBookByYear([FromQuery] int year, [FromQuery] string order = "asc")
    {
        var booksQuery = _context.Books
            .Where(b => b.Published_Year.StartsWith(year.ToString())) 
            .AsQueryable();
        
        if (order.ToLower() == "desc")
        {
            booksQuery = booksQuery.OrderByDescending(b => b.Published_Year); 
        }
        else
        {
            booksQuery = booksQuery.OrderBy(b => b.Published_Year); 
        }
        var books = await booksQuery.ToListAsync();
        return Ok(books);
    }
    
    [HttpGet("AuthorsByBirthYear")]
    public async Task<ActionResult> GetAuthorsByBirthYear()
    {
        var groupedAuthors = await _context.Authors
            .GroupBy(a => a.Birth_Date.Year)
            .Select(g => new
            {
                BirthYear = g.Key,
                Authors = g.Select(a => new
                {
                    a.AuthorId, a.Name, a.Country
                }).ToList()
            })
            .ToListAsync();

        return Ok(groupedAuthors);
    }
    
    [HttpGet("AuthorsYearCountry")]
    public async Task<ActionResult> GetAuthorsGroupYearCountry()
    {
        var groupedAuthors = await _context.Authors
            .GroupBy(a => new { Year = a.Birth_Date.Year, a.Country })
            .Select(g => new
            {
                Year = g.Key.Year,
                Country = g.Key.Country,
                Authors = g.Select(a => new
                {
                    a.AuthorId, a.Name, a.Country
                }).ToList()
            })
            .ToListAsync();

        return Ok(groupedAuthors);
    }
    
    [HttpGet("BookCount")]
    public async Task<ActionResult> GetBookCount()
    {
        var totalBooks = await _context.Books.CountAsync();

        return Ok(totalBooks);
    }
    
    [HttpGet("GetPages")]
    public async Task<ActionResult> GetsPaged(int pageNumber, int pageSize)
    {
        var skip = (pageNumber - 1) * pageSize;
        
        var books = await _context.Books
            .Skip(skip) 
            .Take(pageSize) 
            .ToListAsync(); 
        
        return Ok(books);
    }
    
}    

    














