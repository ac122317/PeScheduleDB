using System.ComponentModel.DataAnnotations;
namespace PeScheduleDB.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(60), EmailAddress]
        public string Email { get; set; }

        [Required, MaxLength(4)]
        public string TeacherCode { get; set; }
        public List<Course>Courses { get; set; }
    }
}
