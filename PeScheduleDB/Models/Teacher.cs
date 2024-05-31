using System.ComponentModel.DataAnnotations;
namespace PeScheduleDB.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }

        [Required, MinLength(2), MaxLength(20), RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last name must only contain letters, no special characters or spaces.")]
        public string LastName { get; set; }

        [Required, MinLength(2), MaxLength(20), RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name must only contain letters, no special characters or spaces.")]
        public string FirstName { get; set; }

        [Required, MaxLength(35), EmailAddress]
        public string Email { get; set; }

        [Required, MaxLength(4)]
        public string TeacherCode { get; set; }
        public List<Course>Courses { get; set; }
    }
}
