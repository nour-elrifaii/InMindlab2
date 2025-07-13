
using AuthorBook2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace AuthorBook2.Controllers;

public class LoansController: ODataController
{
    private readonly PostgresContext _context;

    public LoansController(PostgresContext context)
    {
        _context = context;
    }

    [EnableQuery]
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_context.Loans);
    }
    
    
}