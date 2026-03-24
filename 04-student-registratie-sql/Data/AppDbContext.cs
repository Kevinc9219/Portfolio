using Microsoft.EntityFrameworkCore;
using StudentRegistratie.Models;

namespace StudentRegistratie.Data;

public class AppDbContext : DbContext
{
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=studentregistratie.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasIndex(s => s.Email).IsUnique();
            entity.Property(s => s.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(s => s.LastName).IsRequired().HasMaxLength(100);
            entity.Property(s => s.Email).IsRequired().HasMaxLength(200);
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.Property(c => c.Name).IsRequired().HasMaxLength(200);
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasOne(e => e.Student)
                  .WithMany(s => s.Enrollments)
                  .HasForeignKey(e => e.StudentId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Course)
                  .WithMany(c => c.Enrollments)
                  .HasForeignKey(e => e.CourseId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Seed data
        modelBuilder.Entity<Student>().HasData(
            new Student { Id = 1, FirstName = "Emma",  LastName = "de Vries",   Email = "emma.devries@student.nl",   EnrollmentDate = new DateTime(2023, 9, 1) },
            new Student { Id = 2, FirstName = "Liam",  LastName = "Janssen",    Email = "liam.janssen@student.nl",   EnrollmentDate = new DateTime(2023, 9, 1) },
            new Student { Id = 3, FirstName = "Sofie", LastName = "Bakker",     Email = "sofie.bakker@student.nl",   EnrollmentDate = new DateTime(2024, 2, 1) },
            new Student { Id = 4, FirstName = "Noah",  LastName = "van den Berg",Email = "noah.vdberg@student.nl",   EnrollmentDate = new DateTime(2024, 2, 1) },
            new Student { Id = 5, FirstName = "Mila",  LastName = "Visser",     Email = "mila.visser@student.nl",    EnrollmentDate = new DateTime(2024, 9, 1) }
        );

        modelBuilder.Entity<Course>().HasData(
            new Course { Id = 1, Name = "C# Programmeren",    Description = "Introductie tot C# en .NET",          Credits = 4 },
            new Course { Id = 2, Name = "Databases & SQL",    Description = "Relationele databases en SQL-queries", Credits = 3 },
            new Course { Id = 3, Name = "Web Development",    Description = "HTML, CSS en JavaScript basis",        Credits = 3 }
        );

        modelBuilder.Entity<Enrollment>().HasData(
            new Enrollment { Id = 1, StudentId = 1, CourseId = 1, Grade = 8.5 },
            new Enrollment { Id = 2, StudentId = 1, CourseId = 2, Grade = 7.0 },
            new Enrollment { Id = 3, StudentId = 2, CourseId = 1, Grade = 6.5 },
            new Enrollment { Id = 4, StudentId = 3, CourseId = 2, Grade = 9.0 },
            new Enrollment { Id = 5, StudentId = 3, CourseId = 3, Grade = null },
            new Enrollment { Id = 6, StudentId = 4, CourseId = 3, Grade = 7.5 },
            new Enrollment { Id = 7, StudentId = 5, CourseId = 1, Grade = null }
        );
    }
}
