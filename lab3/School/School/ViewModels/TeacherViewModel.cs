using System.Text.Json.Serialization;

namespace School.ViewModels;


public class TeacherViewModel
{
    public int TeacherId { get; set; }
    public string TeacherName { get; set; } = string.Empty;
    [JsonIgnore]public List<CourseViewModel> Courses { get; set; } = new();
}