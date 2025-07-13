using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using School.Models;

namespace School.Models;


public class Class
{
    public int ClassId { get; set; }
    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;
    public List<Student> Students { get; set; } = new();
}

