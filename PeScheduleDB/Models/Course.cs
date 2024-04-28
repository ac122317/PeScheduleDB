using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PeScheduleDB.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required, MaxLength(5)]
        public string CourseName { get; set; }

        [ForeignKey("Teacher"), Required]
        public int TeacherId { get; set; }
        public Teacher Teachers { get; set; }
        public List<Location> Locations { get; set; }
        public List<Student>Students { get; set; } 
       
    }
}