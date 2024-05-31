using System.ComponentModel.DataAnnotations;
namespace PeScheduleDB.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required, MinLength(2), MaxLength(20), RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name must only contain letters, no special characters or spaces.")]
        public string LastName { get; set; }

        [Required, MinLength(2), MaxLength(20), RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name must only contain letters, no special characters or spaces.")]
        public string FirstName { get; set; }

        [Required, MaxLength(35), EmailAddress]
        public string Email { get; set; }

        [Required, Range(9,13)]
        public int YearLevel { get; set; }

        [Required, RegularExpression(@"^\+?\d{1,3}[- ]?\(?\d{3}\)?[- ]?\d{3}[- ]?\d{4}$", ErrorMessage = "Invalid phone number format (please include +64)")]
        public string Emergency_Contact { get; set; }
        public List<Course> Courses { get; set; }
    }
}
