
namespace School.Models;

public class Teacher
{
    public int TeacherId { get; set; }
    public string TeacherName { get; set; } = string.Empty;
    public List<Course> Courses { get; set; } = new();
}

