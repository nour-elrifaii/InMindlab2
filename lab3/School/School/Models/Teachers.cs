
namespace School.Models;

public class Teachers
{
    public int TeacherId { get; set; }
    public string TeacherName { get; set; }
    public List<Courses> Courses { get; set; } = new List<Courses>();
}

