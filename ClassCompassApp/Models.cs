#nullable enable
using System;
using System.Collections.Generic;

namespace ClassCompass
{
    public class Student
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
        public int ClassId { get; set; }
        public bool IsActive { get; set; }
        public bool NotificationsEnabled { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }

    public class Teacher
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public int SchoolId { get; set; }
        public bool IsActive { get; set; }
        public List<Student>? Students { get; set; }
    }

    public class SchoolRegistration
    {
        public string Name { get; set; } = string.Empty;
        public int NumberOfClasses { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

