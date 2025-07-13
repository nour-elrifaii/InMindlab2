
using Microsoft.EntityFrameworkCore;
using School.Models;
using Microsoft.AspNetCore.Mvc;


namespace School.Data;

public class PortalContext : DbContext
{
    public PortalContext(DbContextOptions<PortalContext> options) : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Class> Classes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Student>()
            .HasMany(s => s.Classes)
            .WithMany(c => c.Students)
            .UsingEntity<Dictionary<string, object>>(
                "ClassStudent",
                j => j.HasOne<Class>()
                    .WithMany()
                    .HasForeignKey("ClassesClassId")
                    .HasConstraintName("FK_ClassStudent_Classes"),
                j => j.HasOne<Student>()
                    .WithMany()
                    .HasForeignKey("StudentsStudentId")
                    .HasConstraintName("FK_ClassStudent_Students"),
                j => j.HasKey("ClassesClassId", "StudentsStudentId")
            );
    }
}
    

