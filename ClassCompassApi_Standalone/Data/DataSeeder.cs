using ClassCompassApi.Shared.Data;
using ClassCompassApi.Shared.Models;

namespace ClassCompassApi.Data
{
    public static class DataSeeder
    {
        public static void SeedData(AppDbContext context)
        {
            if (context.Students.Any()) return; // Already seeded

            // Add sample schools
            var school = new School
            {
                SchoolId = 1,
                Name = "Demo High School",
                NumberOfClasses = 5,
                Description = "A demo school for testing",
                CreatedDate = DateTime.Now
            };
            context.Schools.Add(school);

            // Add sample teachers first
            var teachers = new[]
            {
                new Teacher
                {
                    Id = 1,
                    TeacherId = 1001,
                    Name = "Mr. Michael Wilson",
                    PasswordHash = "hashedpassword123",
                    Subject = "Mathematics",
                    SchoolId = 1,
                    IsActive = true
                },
                new Teacher
                {
                    Id = 2,
                    TeacherId = 1002,
                    Name = "Ms. Sarah Davis",
                    PasswordHash = "hashedpassword456",
                    Subject = "Science",
                    SchoolId = 1,
                    IsActive = true
                }
            };
            context.Teachers.AddRange(teachers);

            // Add sample classrooms
            var classRooms = new[]
            {
                new ClassRoom
                {
                    Id = 1,
                    Capacity = 30,
                    TeacherId = 1,
                    SchoolId = 1
                },
                new ClassRoom
                {
                    Id = 2,
                    Capacity = 25,
                    TeacherId = 2,
                    SchoolId = 1
                }
            };
            context.ClassRooms.AddRange(classRooms);

            // Add sample students
            var students = new[]
            {
                new Student
                {
                    Id = 1,
                    StudentId = 2001,
                    Name = "John Doe",
                    PasswordHash = "studentpass123",
                    ClassName = "Math Class A",
                    TeacherId = 1,
                    ClassId = 1,
                    EnrollmentDate = DateTime.Now.AddMonths(-6),
                    IsActive = true,
                    NotificationsEnabled = true
                },
                new Student
                {
                    Id = 2,
                    StudentId = 2002,
                    Name = "Jane Smith",
                    PasswordHash = "studentpass456",
                    ClassName = "Science Class B",
                    TeacherId = 2,
                    ClassId = 2,
                    EnrollmentDate = DateTime.Now.AddMonths(-6),
                    IsActive = true,
                    NotificationsEnabled = true
                },
                new Student
                {
                    Id = 3,
                    StudentId = 2003,
                    Name = "Bob Johnson",
                    PasswordHash = "studentpass789",
                    ClassName = "Math Class A",
                    TeacherId = 1,
                    ClassId = 1,
                    EnrollmentDate = DateTime.Now.AddMonths(-3),
                    IsActive = true,
                    NotificationsEnabled = false
                }
            };
            context.Students.AddRange(students);

            // Add basic attendance records using minimal properties that exist
            var attendanceRecords = new[]
            {
                new Attendance
                {
                    // Using Guid.NewGuid() for AttendanceId since it expects Guid, not int
                    AttendanceId = Guid.NewGuid(),
                    StudentId = 1,
                    Date = DateTime.Today
                    // Note: Only using properties that definitely exist
                },
                new Attendance
                {
                    AttendanceId = Guid.NewGuid(),
                    StudentId = 2,
                    Date = DateTime.Today
                },
                new Attendance
                {
                    AttendanceId = Guid.NewGuid(),
                    StudentId = 3,
                    Date = DateTime.Today
                }
            };
            context.Attendances.AddRange(attendanceRecords);

            // Skip other models for now until we know their exact structure
            // We can add them later once we verify the property names

            context.SaveChanges();
        }
    }
}

