using System;
using System.Collections.Generic;

namespace AuthorBook2.Models;

public partial class Author
{
    public int AuthorId { get; set; }

    public string? Name { get; set; }

    public DateOnly? BirthDate { get; set; }

    public string? Country { get; set; }
}
