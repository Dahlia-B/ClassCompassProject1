using ClassCompass.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace ClassCompass.Shared.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<BehaviorRemark> BehaviorRemarks { get; set; }
        public DbSet<ClassPeriod> ClassPeriods { get; set; }
        public DbSet<ClassRoom> ClassRooms { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassRoster> ClassRosters { get; set; }
        public DbSet<DailySchedule> DailySchedules { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<HomeworkSubmission> HomeworkSubmissions { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<ScheduleEntry> ScheduleEntries { get; set; }
        public DbSet<ScheduleTemplate> ScheduleTemplates { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Add any Fluent API configuration here if needed
        }
    }
}
