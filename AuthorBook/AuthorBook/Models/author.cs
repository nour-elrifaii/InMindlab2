using AuthorBook.models;
using System.ComponentModel.DataAnnotations;

namespace AuthorBook.Models;

public class Author
{
    public int AuthorId { get; set; }             
    public string Name { get; set; }
    public DateTime Birth_Date { get; set; }
    public string Country { get; set; }
}