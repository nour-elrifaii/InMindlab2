
namespace School.Models;

public class Students
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Courses> Courses { get; set; } = new List<Courses>();
}
