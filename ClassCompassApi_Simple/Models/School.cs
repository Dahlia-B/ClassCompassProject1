using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassCompassApi_Simple.Models
{
    [Table("schools")]
    public class School
    {
        [Key]
        [Column("school_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SchoolId { get; set; }
        
        [Required]
        [MaxLength(200)]
        [Column("name")]
        public string Name { get; set; } = "";
        
        [Column("number_of_classes")]
        public int NumberOfClasses { get; set; }
        
        [Column("description")]
        [MaxLength(500)]
        public string? Description { get; set; }
        
        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
