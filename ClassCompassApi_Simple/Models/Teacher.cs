using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassCompassApi_Simple.Models
{
    [Table("teachers")]
    public class Teacher
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Column("teacher_id")]
        public int TeacherId { get; set; }
        
        [Required]
        [MaxLength(100)]
        [Column("name")]
        public string Name { get; set; } = "";
        
        [MaxLength(100)]
        [Column("subject")]
        public string? Subject { get; set; }
        
        [Column("school_id")]
        public int SchoolId { get; set; }
        
        [MaxLength(255)]
        [Column("password_hash")]
        public string? PasswordHash { get; set; }
        
        [Column("is_active")]
        public bool IsActive { get; set; } = true;
        
        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        [ForeignKey("SchoolId")]
        public virtual School School { get; set; } = null!;
        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
