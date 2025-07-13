namespace School.ViewModels;


public class CourseViewModel
{
    public int CourseId { get; set; }
    public string CourseName { get; set; } = string.Empty;
    public TeacherViewModel Teacher { get; set; } = null!;
    //public List<ClassViewModel> Classes { get; set; } = new();
}