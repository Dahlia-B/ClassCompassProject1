using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassCompassApi_Simple.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("user_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        
        [Required]
        [MaxLength(100)]
        [Column("username")]
        public string Username { get; set; } = "";
        
        [Required]
        [MaxLength(50)]
        [Column("role")]
        public string Role { get; set; } = "";
        
        [Column("teacher_id")]
        public int? TeacherId { get; set; }
        
        [Column("student_id")]
        public int? StudentId { get; set; }
        
        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        [ForeignKey("TeacherId")]
        public virtual Teacher? Teacher { get; set; }
        
        [ForeignKey("StudentId")]
        public virtual Student? Student { get; set; }
    }
}
