
namespace School.Models;

public class Student
{
    public int StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public List<Class> Classes { get; set; } = new();
}
