using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using AuthorBook.Models;
namespace AuthorBook.models;

public class Book
{
    public int BookId { get; set; }
    public string Title { get; set; }
    [ForeignKey("Author")]
    public int AuthorId { get; set; }
    public string ISBN { get; set; }
    public string Published_Year { get; set; }
    [JsonIgnore]
    public Author? Author { get; set; }
}

