using AuthorBook2.Models;
using Microsoft.AspNetCore.Analyz;
using Microsoft.AspNetCore.Mvc;

namespace AuthorBook2.Controllers;

public class LibrariesController : ODataController
{
    public static Random random = new Random();

    public static List<Book> Books = new List<Book>;
