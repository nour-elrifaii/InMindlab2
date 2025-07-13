using AuthorBook2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace AuthorBook2.Controllers;

public class AuthorsController: ODataController
{
    private readonly PostgresContext _context;

    public AuthorsController(PostgresContext context)
    {
        _context = context;
    }

    [EnableQuery]
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_context.Authors);
    }
    
    
}