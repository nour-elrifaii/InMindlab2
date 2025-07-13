
using AutoMapper;

using AutoMapper;
using School.Models;
using School.ViewModels;

public class UniversityMappingProfile : Profile
{
    public UniversityMappingProfile()
    {
        CreateMap<Student, StudentViewModel>()
            .ForMember(d => d.StudentId,   e => e.MapFrom(src => src.StudentId))
            .ForMember(d => d.StudentName, e => e.MapFrom(src => src.StudentName))
            .ForMember(d => d.Classes,     e => e.MapFrom(s => s.Classes));
        
        CreateMap<Class, ClassViewModel>()
            .ForMember(d => d.ClassId, e => e.MapFrom(src => src.ClassId))
            .ForMember(d => d.Course, e => e.MapFrom(src => src.Course));
            //.ForMember(d => d.Students, e => e.MapFrom(src => src.Students));

            CreateMap<Course, CourseViewModel>()
                .ForMember(d => d.CourseId, e => e.MapFrom(src => src.CourseId))
                .ForMember(d => d.CourseName, e => e.MapFrom(src => src.CourseName))
                .ForMember(d => d.Teacher, e => e.MapFrom(src => src.Teacher));
            //.ForMember(d => d.Classes,    e => e.MapFrom(src => src.Classes));
        
        CreateMap<Teacher, TeacherViewModel>()
            .ForMember(d => d.TeacherId,   e => e.MapFrom(src => src.TeacherId))
            .ForMember(d => d.TeacherName, e => e.MapFrom(src => src.TeacherName))
            .ForMember(d => d.Courses,     e => e.MapFrom(src => src.Courses));
    }
}
