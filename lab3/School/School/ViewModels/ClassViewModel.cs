namespace School.ViewModels;

public class ClassViewModel
{
    public int ClassId { get; set; }
    public CourseViewModel Course { get; set; } = null!;
    //public List<StudentViewModel> Students { get; set; } = new();
}
