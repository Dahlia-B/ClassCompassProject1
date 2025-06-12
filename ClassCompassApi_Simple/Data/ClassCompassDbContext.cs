using Microsoft.EntityFrameworkCore;
using ClassCompassApi_Simple.Models;

namespace ClassCompassApi_Simple.Data
{
    public class ClassCompassDbContext : DbContext
    {
        public ClassCompassDbContext(DbContextOptions<ClassCompassDbContext> options) : base(options)
        {
        }

        public DbSet<School> Schools { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure MySQL-specific settings
            modelBuilder.Entity<School>(entity =>
            {
                entity.HasKey(e => e.SchoolId);
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP()");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP()");
                entity.HasOne(d => d.School)
                    .WithMany(p => p.Teachers)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP()");
                entity.HasOne(d => d.School)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP()");
                entity.HasOne(d => d.Teacher)
                    .WithMany()
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(d => d.Student)
                    .WithMany()
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Seed initial data
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Username = "admin", Role = "Admin", CreatedDate = DateTime.UtcNow },
                new User { UserId = 2, Username = "teacher1", Role = "Teacher", CreatedDate = DateTime.UtcNow },
                new User { UserId = 3, Username = "student1", Role = "Student", CreatedDate = DateTime.UtcNow }
            );
        }
    }
}
