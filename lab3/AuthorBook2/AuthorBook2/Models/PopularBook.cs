using System;
using System.Collections.Generic;

namespace AuthorBook2.Models;

public partial class PopularBook
{
    public string? Title { get; set; }

    public long? NumberOfLoans { get; set; }
}
