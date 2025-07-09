using System;
using System.Collections.Generic;

namespace AuthorBook2.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string? Title { get; set; }

    public int? AuthorId { get; set; }

    public int? Isbn { get; set; }

    public int? PublishedYear { get; set; }

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
