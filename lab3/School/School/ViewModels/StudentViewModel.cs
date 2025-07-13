

namespace School.ViewModels;


public class StudentViewModel
{
    public int StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public List<ClassViewModel> Classes { get; set; } = new();
}