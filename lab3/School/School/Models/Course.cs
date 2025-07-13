
namespace School.Models;

public class Course
{
    public int CourseId { get; set; }
    public string CourseName { get; set; } = string.Empty;
    public int TeacherId { get; set; }
    public Teacher Teacher { get; set; } = null!;
    public List<Class> Classes { get; set; } = new();
}

