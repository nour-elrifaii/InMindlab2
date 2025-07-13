using AutoMapper;
using School.Data;
using School.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.ViewModels;

namespace School.Controller;

[ApiController]
[Route("api/[controller]")]
public class UniversityController : ControllerBase
{
    private readonly PortalContext _context;
    private readonly IMapper _mapper;

    public UniversityController(PortalContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    
    [HttpPost("AddTeacher")]
    public async Task<IActionResult> AddTeacher([FromBody] TeacherCreateDto dto)
    {
        
        var teacher = new Teacher
        {
            TeacherName = dto.TeacherName
        };

        await _context.Teachers.AddAsync(teacher);
        await _context.SaveChangesAsync();
        return Ok(_mapper.Map<TeacherViewModel>(teacher));
    }
    
    [HttpPost("AddStudent")]
    public async Task<IActionResult> AddStudent([FromBody] StudentDto dto)
    {
        var student = new Student { StudentName = dto.StudentName };
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        
        return Ok(_mapper.Map<StudentViewModel>(student));
    }
    
    [HttpPost("AddCourse")]
    public async Task<IActionResult> AddCourse([FromBody] CourseDto dto)
    {
        var course = new Course { CourseName = dto.CourseName, TeacherId = dto.TeacherId };
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
        var fullCourse = await _context.Courses
            .Include(c => c.Teacher)
            .FirstAsync(c => c.CourseId == course.CourseId);
        
        return Ok(_mapper.Map<CourseViewModel>(fullCourse));
    }
    
    [HttpPost("AddClass")]
    public async Task<IActionResult> AddClass([FromBody] ClassDto dto)
    {
        var course = await _context.Courses.FindAsync(dto.CourseId);
        if (course == null) return NotFound("Course not found");

        var classEntity = new Class { CourseId = dto.CourseId };   
        _context.Classes.Add(classEntity);
        await _context.SaveChangesAsync();

        //var vm = _mapper.Map<ClassViewModel>(classEntity);
        var full = await _context.Classes
            .Include(c => c.Course)
                    .ThenInclude(cr => cr.Teacher)
            .FirstAsync(c => c.ClassId == classEntity.ClassId);
        
        return Ok(_mapper.Map<ClassViewModel>(full));
    }
    
    [HttpPost("enroll")]
    public async Task<IActionResult> EnrollStudent([FromBody] EnrollDto dto)
    {
        var student = await _context.Students.Include(s => s.Classes)
                .ThenInclude(cl => cl.Course)
                    .ThenInclude(cr => cr.Teacher)
            .FirstOrDefaultAsync(s => s.StudentId == dto.StudentId);
        var classEntity = await _context.Classes
                .Include(c => c.Course)
                    .ThenInclude(cr => cr.Teacher)
                .FirstOrDefaultAsync(c => c.ClassId == dto.ClassId);

        if (student == null || classEntity == null)
            return NotFound("Student or Class not found");

        student.Classes.Add(classEntity);
        await _context.SaveChangesAsync();
        student = await _context.Students
            .Include(s => s.Classes)
                .ThenInclude(cl => cl.Course)
                    .ThenInclude(cr => cr.Teacher)
            .FirstAsync(s => s.StudentId == dto.StudentId);
        
        return Ok(_mapper.Map<StudentViewModel>(student));
    }
    
    [HttpDelete("remove-class/{classId}")]
    public async Task<IActionResult> RemoveClass(int classId)
    {
        var classEntity = await _context.Classes.FindAsync(classId);
        if (classEntity == null) return NotFound("Class not found");

        _context.Classes.Remove(classEntity);
        await _context.SaveChangesAsync();
        return Ok("Class removed");
    }


}

