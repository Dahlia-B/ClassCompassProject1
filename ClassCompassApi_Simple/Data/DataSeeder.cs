using ClassCompassApi_Simple.Data;
using ClassCompassApi_Simple.Models;

namespace ClassCompassApi_Simple.Data
{
    public static class DataSeeder
    {
        public static void SeedData(AppDbContext context)
        {
            if (context.Students.Any()) return; // Already seeded

            // Add sample school
            var school = new School
            {
                SchoolId = 1,
                Name = "Demo High School",
                NumberOfClasses = 5,
                Description = "A demo school for ClassCompass testing",
                CreatedDate = DateTime.Now
            };
            context.Schools.Add(school);

            // Add sample teachers
            var teachers = new[]
            {
                new Teacher
                {
                    Id = 1,
                    TeacherId = 1001,
                    Name = "Mr. Michael Wilson",
                    Subject = "Mathematics",
                    IsActive = true
                },
                new Teacher
                {
                    Id = 2,
                    TeacherId = 1002,
                    Name = "Ms. Sarah Davis",
                    Subject = "Science",
                    IsActive = true
                }
            };
            context.Teachers.AddRange(teachers);

            // Add sample students
            var students = new[]
            {
                new Student
                {
                    Id = 1,
                    StudentId = 2001,
                    Name = "John Doe",
                    ClassName = "Math Class A",
                    TeacherId = 1,
                    EnrollmentDate = DateTime.Now.AddMonths(-6),
                    IsActive = true,
                    NotificationsEnabled = true
                },
                new Student
                {
                    Id = 2,
                    StudentId = 2002,
                    Name = "Jane Smith",
                    ClassName = "Science Class B",
                    TeacherId = 2,
                    EnrollmentDate = DateTime.Now.AddMonths(-6),
                    IsActive = true,
                    NotificationsEnabled = true
                },
                new Student
                {
                    Id = 3,
                    StudentId = 2003,
                    Name = "Bob Johnson",
                    ClassName = "Math Class A",
                    TeacherId = 1,
                    EnrollmentDate = DateTime.Now.AddMonths(-3),
                    IsActive = true,
                    NotificationsEnabled = false
                }
            };
            context.Students.AddRange(students);

            context.SaveChanges();
        }
    }
}
