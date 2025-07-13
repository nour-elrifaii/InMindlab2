using AuthorBook2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace AuthorBook2.Controllers;

public class BooksController : ODataController
{
    private readonly PostgresContext _context;

    public BooksController(PostgresContext context)
    {
        _context = context;
    }

    [EnableQuery]
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_context.Books);
    }
    
    
    
}    
