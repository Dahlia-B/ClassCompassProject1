using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassCompassApi_Simple.Models
{
    [Table("students")]
    public class Student
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Column("student_id")]
        public int StudentId { get; set; }
        
        [Required]
        [MaxLength(100)]
        [Column("name")]
        public string Name { get; set; } = "";
        
        [MaxLength(50)]
        [Column("class_name")]
        public string? ClassName { get; set; }
        
        [Column("teacher_id")]
        public int? TeacherId { get; set; }
        
        [Column("class_id")]
        public int? ClassId { get; set; }
        
        [Column("school_id")]
        public int? SchoolId { get; set; }
        
        [MaxLength(255)]
        [Column("password_hash")]
        public string? PasswordHash { get; set; }
        
        [Column("is_active")]
        public bool IsActive { get; set; } = true;
        
        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        [ForeignKey("TeacherId")]
        public virtual Teacher? Teacher { get; set; }
        
        [ForeignKey("SchoolId")]
        public virtual School? School { get; set; }
    }
}
